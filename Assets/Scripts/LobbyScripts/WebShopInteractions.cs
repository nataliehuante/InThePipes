using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class WebShopInteractions : MonoBehaviour
{
    // list of all items in the shop 
    public List<GameObject> shopItems = new List<GameObject>();
    public List<int> shopPrices = new List<int>();
    public LineRenderer playerLineRenderer;
    public SpriteRenderer playerWebAnchorSR;
    public GameObject currentItemEquipped;
    public int currentItemEquippedIndex = 0;
    // text of each buy button 
    public TextMeshProUGUI yourCoinsText;
    public List<Material> webColors = new List<Material>(); 
    public List<Sprite> webAnchors = new List<Sprite>();
    public LevelsSounds levelsSounds;

    [Header("Button Colors")]
    public Sprite greenButton;
    public Sprite blueButton; 
    public Sprite redButton;

    void Awake() {
        onLoadMenu();
    }

    void Update() {
        CheckHotKeys();
    }

    private void CheckHotKeys() {
        // if (Input.GetKeyDown(KeyCode.Alpha1) ) {
        //     GlobalVariables.totalCoins += 500;
        //     onLoadMenu();
        // }
    }

    // loads menu based on menu's status
    public void onLoadMenu() {
        Debug.Log("Loading Web Menu....");
        // will update coins to match the current player's total coin count 
        yourCoinsText.text = "" + GlobalVariables.totalCoins;

        int i = 0;
        // will update items' views to match status of purchase
        foreach (GameObject item in shopItems) {
            // check global variables for item status 
            if (GlobalVariables.webShopStatus[i] == "equipped") {
                setAsEquipped(i, false);
                // Debug.Log("set item " + i + " as equipped");
            } else if (GlobalVariables.webShopStatus[i] == "canEquip") {
                setAsCanEquip(i);
                // Debug.Log("set item " + i + " as can equip");
            } else if (GlobalVariables.webShopStatus[i] == "canBuy") {
                setAsCanBuy(i);
                // Debug.Log("set item " + i + " can buy");
            } else if (GlobalVariables.webShopStatus[i] == "locked") {
                setAsLocked(i);
                // Debug.Log("set item " + i + " as locked");
            } else {
                Debug.Log ("Error loading item status!");
                setAsCanBuy(i);
            }
            i++;
        }

    }

    public void setAsEquipped(int itemIndex, bool fromButtonClick) { // user has equipped item 
        Debug.Log("set item " + itemIndex + " as equipped");

        // get shop game object
        GameObject item = shopItems[itemIndex];

        // get the "PriceBackground" child object
        GameObject priceBackground = item.transform.GetChild(3).gameObject;

        // set the button sprite to green 
        priceBackground.GetComponent<Image>().sprite = greenButton;

        // set the correct dimensions of the button 
        priceBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 70);

        // set coin logo to inactive 
        priceBackground.transform.GetChild(0).gameObject.SetActive(false);

        // set button text 
        GameObject priceText = priceBackground.transform.GetChild(1).gameObject;
        priceText.GetComponent<TextMeshProUGUI>().text = "Equipped";
        priceText.GetComponent<RectTransform>().anchoredPosition = new Vector3(20f, -4f, 0f);

        // reset current equipped item to can equip 
        // if (currentItemEquippedIndex != -1) {
        
        // }
        
        if (fromButtonClick) {
            setAsCanEquip(currentItemEquippedIndex);
            GlobalVariables.webShopStatus[currentItemEquippedIndex] = "canEquip";
        }

        // set as current equipped item 
        currentItemEquipped = item; 
        currentItemEquippedIndex = itemIndex;

        // foreach (GameObject listItem in itemsCanEquip) {
        //     setAsCanEquip(listItem);
        // }
        GlobalVariables.webShopStatus[itemIndex] = "equipped";
        GlobalVariables.currentWebColorIndex = itemIndex;
        if (itemIndex == 0) {
            GlobalVariables.currentWebColor = "white";
        } else if (itemIndex == 1) {
            GlobalVariables.currentWebColor = "blue";
        } else if (itemIndex == 2) {
            GlobalVariables.currentWebColor = "pink";
        } else if (itemIndex == 3) {
            GlobalVariables.currentWebColor = "gold";
        }
       

        // set line renderer & web anchor to new color 
        playerLineRenderer.material = webColors[itemIndex];
        // GlobalVariables.currentWebMaterial = webColors[itemIndex];
        // Debug.Log(GlobalVariables.currentWebMaterial);

        playerWebAnchorSR.sprite = webAnchors[itemIndex];
        // GlobalVariables.currentWebAnchorSprite = webAnchors[itemIndex];

        // set lock and unlock text as disabled
        priceBackground.transform.GetChild(2).gameObject.SetActive(false);
        priceBackground.transform.GetChild(3).gameObject.SetActive(false);

    }

    public void setAsCanEquip(int itemIndex) { // player has bought the item, avail to equip now 
        Debug.Log("set item " + itemIndex + " as can equip");

        // get shop game object
        GameObject item = shopItems[itemIndex];

        // get the "PriceBackground" child object
        GameObject priceBackground = item.transform.GetChild(3).gameObject;

        // set the button sprite to green 
        priceBackground.GetComponent<Image>().sprite = blueButton;

        // set the correct dimensions of the button 
        priceBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(215, 70);

        // set coin logo to inactive 
        priceBackground.transform.GetChild(0).gameObject.SetActive(false);

        // set button text 
        GameObject priceText = priceBackground.transform.GetChild(1).gameObject;
        priceText.GetComponent<TextMeshProUGUI>().text = "Equip Item";
        priceText.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, -4f, 0f);

        // update item state 
        GlobalVariables.webShopStatus[itemIndex] = "canEquip";

        // set lock and unlock text as disabled
        priceBackground.transform.GetChild(2).gameObject.SetActive(false);
        priceBackground.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void setAsCanBuy(int itemIndex) { // player has unclocked item, avail to buy 
        Debug.Log("set item " + itemIndex + " as can buy");

        // get shopp game object
        GameObject item = shopItems[itemIndex];

        // get the "PriceBackground" child object
        GameObject priceBackground = item.transform.GetChild(3).gameObject;

        // set the button sprite to blue 
        priceBackground.GetComponent<Image>().sprite = blueButton;

        // set the correct dimensions of the button 
        priceBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(175, 70);

        // set coin logo to active 
        priceBackground.transform.GetChild(0).gameObject.SetActive(true);

        // set button text 
        GameObject priceText = priceBackground.transform.GetChild(1).gameObject;
        priceText.GetComponent<TextMeshProUGUI>().text = "" + shopPrices[itemIndex];
        priceText.GetComponent<RectTransform>().anchoredPosition = new Vector3(80.4f, -4f, 0f);

        // set lock and unlock text as disabled
        priceBackground.transform.GetChild(2).gameObject.SetActive(false);
        priceBackground.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void setAsLocked(int itemIndex) {
        Debug.Log("set item " + itemIndex + " as locked");

        // get shopp game object
        GameObject item = shopItems[itemIndex];

        // set lock and unlock text as enabled
        GameObject priceBackground = item.transform.GetChild(3).gameObject;
        priceBackground.transform.GetChild(2).gameObject.SetActive(true);
        priceBackground.transform.GetChild(3).gameObject.SetActive(true);

    }

    public void onButtonClick(int itemIndex) {
        // check the current state of the item 
        string buttonState = GlobalVariables.webShopStatus[itemIndex];

        if (buttonState == "equipped") { // item is equipped, do nothing on click 

        } else if (buttonState == "canEquip") { // item has been bought, equip item on click
            setAsEquipped(itemIndex, true);
            // Debug.Log("set item " + itemIndex + " as equipped");
        } else if (buttonState == "canBuy") { // item is unlocked & can be bought, on click set item as canEquip
            if (GlobalVariables.totalCoins >= shopPrices[itemIndex]) {
                GlobalVariables.totalCoins -= shopPrices[itemIndex];
                yourCoinsText.text = "" + GlobalVariables.totalCoins;
                setAsCanEquip(itemIndex);
                levelsSounds.PlayItemPurchased();
                
            } else {
                Debug.Log("not enough coins to buy");
            }
        } else if (buttonState == "locked") { // item is locked, on click won't do anything

        } 

        
    }
}
