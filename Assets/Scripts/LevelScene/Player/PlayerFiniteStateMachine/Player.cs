using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables 
    // declare state machine
    public PlayerStateMachine StateMachine { get; private set; }

    // declare all player states
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState;
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerGrappleState GrappleState { get; private set; }
    public PlayerShootingState ShootingState { get; private set; }
    public PlayerDeathState DeathState { get; private set;}


    [SerializeField]
    public PlayerData playerData;

    #endregion


    #region Components
    // animator
    public Animator Anim; 
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public WebSystem webSystem { get; private set; }
    [SerializeField]
    private GameObject bulletPrefab;
    #endregion

    #region Level Variables
    public LevelController levelController { get; private set; }
    public LevelsSounds sounds { get; private set; }
    public InPlayGameView inPlayGameView { get; private set; }

    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform wallCheck;
    #endregion

    #region Effects
    public GameObject dirtPoof1;
    public GameObject dirtPoof2;
    public GameObject dirtPoof1_Lobby;
    public GameObject dirtPoof2_Lobby;
    #endregion

    #region Other Variables 
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public bool enemyNearbySoundLock;
    public bool isSwinging;
    private Vector2 workspace;
    public bool disableAllMovement = false;

    private GameObject[] multiplierCoins;
    public bool inLobby = false;
    public bool isDeathAnimationFinished = false;
    private float lastHurtTime;
    #endregion


    #region Unity Callback Functions
    private void Awake() {
        // initialize state machine
        StateMachine = new PlayerStateMachine();

        // initialize all player states
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        GrappleState = new PlayerGrappleState(this, StateMachine, playerData, "grapple");
        ShootingState = new PlayerShootingState(this, StateMachine, playerData, "shoot");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death");
        

        // initialize variables
        enemyNearbySoundLock = false;
        isSwinging = false;


         
    }

    private void Start() {
        // set references
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        inPlayGameView = FindObjectOfType<InPlayGameView>();
        sounds = FindObjectOfType<LevelsSounds>();
        levelController = FindObjectOfType<LevelController>();
        webSystem = GetComponent<WebSystem>();

        // initialize variables 
        FacingDirection = -1;

        // initialize state machine to the default state 
        StateMachine.Initialize(IdleState);

        if (!inLobby) {
            // reset player lives
            ResetLives();

            deactivateAllCoins();
        }

        updateWebColor();
        
    }

    private void Update() {
        if (disableAllMovement)
            return;
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void fixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion


    #region Set Functions
    public void SetVelocityX(float velocity) {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity) {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction) {
        // normalize the angle 
        angle.Normalize();

        // set up workspace 
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);

        // set velocity
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void setAnimatorForSwing() {
        // set to idle 
        Anim.SetBool("idle", true);
        Anim.speed = 0;
    }

    public void resetAnimatorSpeed() {
        Anim.speed = 1;
        Anim.SetBool("idle", false);
    }

    #endregion


    #region Check Functions
    public void CheckIfShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) {
            Flip();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
    }
  
    public bool CheckIfGrounded() {
        // will return true if the overlap circcle detects anything in whatIsGround within the radius of the circle 
        bool isOnGround = Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
        return isOnGround;
    }

    public bool CheckIfTouchingWall() {
        bool isTouchingWall;
        if (inLobby) {
            isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGroundLobby);
        } else {
            isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        }
        return isTouchingWall;
    }

     public bool CheckIfTouchingWallBack() {
        bool isTouchingWall;
        if (inLobby) {
            isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGroundLobby);
        } else {
            isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        }
        return isTouchingWall;
    }


    #endregion


    #region Other Functions
    private void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void InstantiateShot()
    {
        sounds.PlayShootSound();
        // get positions needed
        Vector3 mousePosition = new Vector3(InputHandler.MousePosition.x, InputHandler.MousePosition.y, -10);
        Vector3 playerPosition = transform.position + new Vector3(0, 0.25f, 0);
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(playerPosition);

        // Calculate direction
        Vector3 direction = (mousePosition - playerScreenPosition).normalized;
        
        // Calculate rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Calculate point of instantiation (optional offset)
        float spawnDistance = 1f; // Adjust this value as needed
        Vector3 spawnPoint = playerPosition + direction * spawnDistance;

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint, rotation);

        // If your bullet has a Rigidbody2D component and you want to apply force
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * 5f, ForceMode2D.Impulse); // Adjust the force as needed
    }

    #endregion

    #region Collider + Trigger Checks

    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag) {
            case "Rat":
                if (Time.time > lastHurtTime + 1f) {
                    getBitten("rat");
                    lastHurtTime = Time.time;
                }
                break;
            case "Bat":
                if (Time.time > lastHurtTime + 1f) {
                    getBitten("bat");
                    lastHurtTime = Time.time;
                }
                break; 
            case "Snake":
                if (Time.time > lastHurtTime + 1f) {
                    getBitten("snake");
                    lastHurtTime = Time.time;
                }
                break;
        }
    
    }

    // handles player entering a trigger box
    void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.gameObject.tag) {
            case "CompleteLevelTrigger":
                levelController.CompleteLevelTrigger();
                break;
            case "Water":
                loseALife("water", "noFlash");
                break;
            case "ShortWater":
                loseALife("water", "flash");
                break;
            case "Coin":
            case "MultiplierCoin_Batch1":
            case "MultiplierCoin_Batch2":
            case "MultiplierCoin_Batch3":
            case "MultiplierCoin_Batch4":
            case "MultiplierCoin_Batch5":
                collectCoin();
                break;
            case "Heart":
                collectHeart();
                break;
            case "CoinMultiplier_Batch1":
                StartCoroutine(activateCoinMultiplier(collision.gameObject.GetComponent<Collectible>().multiplerCoinPickup_TimeToActivate, "MultiplierCoin_Batch1"));
                break;
            case "CoinMultiplier_Batch2":
                StartCoroutine(activateCoinMultiplier(collision.gameObject.GetComponent<Collectible>().multiplerCoinPickup_TimeToActivate, "MultiplierCoin_Batch2"));
                break;
            case "CoinMultiplier_Batch3":
                StartCoroutine(activateCoinMultiplier(collision.gameObject.GetComponent<Collectible>().multiplerCoinPickup_TimeToActivate, "MultiplierCoin_Batch3"));
                break;
            case "CoinMultiplier_Batch4":
                StartCoroutine(activateCoinMultiplier(collision.gameObject.GetComponent<Collectible>().multiplerCoinPickup_TimeToActivate, "MultiplierCoin_Batch4"));
                break;
            case "CoinMultiplier_Batch5":
                StartCoroutine(activateCoinMultiplier(collision.gameObject.GetComponent<Collectible>().multiplerCoinPickup_TimeToActivate, "MultiplierCoin_Batch5"));
                break;
            case "ShortenGrappleStart":
                webSystem.ropeMaxCastDistance = playerData.shortenedRopeCastDistance;
                break;
            case "ShortenGrappleEnd":
                webSystem.ropeMaxCastDistance = playerData.maxRopeCastDistance;
                break;
        }
    }

    #endregion

     
    #region Life Count Functions
    public void getBitten(string enemyType) {
        switch (enemyType) {
            case "rat":
                loseALife("rat", "flash");
                sounds.PlayRatBatAttack();
                break;
            case "bat":
                loseALife("bat", "flash");
                sounds.PlayRatBatAttack();
                break;
            case "snake":
                loseALife("snake", "flash");
                sounds.PlaySnakeAttack();
                break;
        }
    }


    // handles the player losing a life based on how they received damage
    private void loseALife(string methodOfLoss, string showHurtFlash) {
        // play the hurt sound
        sounds.PlayHurtSound(0.5f);

        // show red damage flash if indicated
        if (showHurtFlash == "flash") {
            inPlayGameView.ShowPlayerHurtCanvas();
        }

        // decrement the life count
        playerData.lives--;

        // if no lives left, pass on lose level sequence to levelController 
        if (playerData.lives <= 0) {
            // levelController.OnPlayerLoseLevel(methodOfLoss);
            DeathState.methodOfDeath = methodOfLoss;
            StateMachine.ChangeState(DeathState);
        }
        // if still lives left, update game hud to display correct number of lives
        else { 
            inPlayGameView.updateLives(playerData.lives);
            // if we got hurt by water, send to last checkpoint
            if (methodOfLoss == "water")
                gameObject.transform.position = GlobalVariables.latestCheckpointPosition;
        }

    }

    private void gainALife() {
        // if not maxed out, increment life count 
        if (playerData.lives < 4) {
            playerData.lives++;
        }

        // update game view
        inPlayGameView.updateLives(playerData.lives);

        // play sound
    }

    // reset lives for the player    
    public void ResetLives() {
        playerData.lives = 3;
        inPlayGameView.updateLives(playerData.lives);
    }    
    
    #endregion
    

    #region Collectibles Functions

    private void deactivateAllCoins() {
        deactivateMultiplierCoins("MultiplierCoin_Batch1");
        deactivateMultiplierCoins("MultiplierCoin_Batch2");
        deactivateMultiplierCoins("MultiplierCoin_Batch3");
        deactivateMultiplierCoins("MultiplierCoin_Batch4");
        deactivateMultiplierCoins("MultiplierCoin_Batch5");
    }
    // player collects a coin
    private void collectCoin() {
        playerData.score = playerData.score + 10;
        inPlayGameView.setScoreText(playerData.score);
        sounds.PlayPickupCoinSound();
    }

    private void collectHeart() {
        gainALife();
        sounds.PlayCoinMultiplier();
    }

    private IEnumerator activateCoinMultiplier(float timeToWait, string coinsToLookFor) {
        // play multiplier sound 
        sounds.PlayCoinMultiplier();

        // multiplierCoins = GameObject.FindGameObjectsWithTag("MultiplierCoin");
        // activate coins
        activateMultiplierCoins(coinsToLookFor);
    
        // wait 5 seconds 
        yield return new WaitForSeconds(timeToWait);

        // start to flash the multiplier coins 
        flashMultiplierCoins(coinsToLookFor);

        // wait 5 seconds 
        yield return new WaitForSeconds(5);

        // inactivate all multiplier coins
        deactivateMultiplierCoins(coinsToLookFor);

    }

    private void activateMultiplierCoins(string coinsToLookFor) {
        multiplierCoins = GameObject.FindGameObjectsWithTag(coinsToLookFor);
        int coinCoint = 0;

        // activate multiplier coins 
        foreach(GameObject coin in multiplierCoins) {
            // coin.SetActive(true);
            coin.GetComponent<SpriteRenderer>().enabled = true;
            coin.GetComponent<CircleCollider2D>().enabled = true;
            coin.GetComponent<Collectible>().shouldFlash = false;
            coinCoint += 1;
        }
        // Debug.Log("activated " + coinCoint + " " + coinsToLookFor + " coins");
    }

    private void deactivateMultiplierCoins(string coinsToLookFor) {
        multiplierCoins = GameObject.FindGameObjectsWithTag(coinsToLookFor);

        // deactivate multiplier coins 
        foreach(GameObject coin in multiplierCoins) {
            coin.GetComponent<SpriteRenderer>().enabled = false;
            coin.GetComponent<CircleCollider2D>().enabled = false;
            coin.GetComponent<Collectible>().shouldFlash = false;
            // coin.SetActive(false);
        }
        // Debug.Log("deactivated " + coinsToLookFor + " coins");
    }

    private void flashMultiplierCoins(string coinsToLookFor) {
        multiplierCoins = GameObject.FindGameObjectsWithTag(coinsToLookFor);
        // deactivate multiplier coins 
        foreach(GameObject coin in multiplierCoins) {
            coin.GetComponent<Collectible>().shouldFlash = true;
        }
        // Debug.Log("flashing " + coinsToLookFor + " coins");
    }

    public void updateWebColor() {
        // Debug.Log("updating web color");
        webSystem.updateWebColor(GlobalVariables.currentWebColorIndex);
        // webSystem.ropeRenderer.material = GlobalVariables.currentWebMaterial;
        // webSystem.ropeHingeAnchorSprite.sprite = GlobalVariables.currentWebAnchorSprite;
    }
    #endregion
   
}
