using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class costumeShopInteraction : MonoBehaviour
{
    // list of all items in the shop 
    public List<GameObject> shopItems = new List<GameObject>();
    public List<int> shopPrices = new List<int>();
    // equipped items
    public GameObject currentItemEquipped;
    public int currentItemEquippedIndex = -1;
    // item on page
    public int currentItemOnPageIndex = -1;
    // text of each buy button 
    public TextMeshProUGUI yourCoinsText;
    // arrow buttons
    public GameObject leftArrow;
    public GameObject rightArrow;

    [Header("Button Colors")]
    public Sprite greenButton;
    public Sprite blueButton; 
    public Sprite redButton;
    // animations
    public Animator Anim;
    public List<string> animationBoolNames = new List<string>();
    public List<GameObject> costumeGameObjects = new List<GameObject>();
    public LevelsSounds levelsSounds;
    


    public void Start() {
        currentItemOnPageIndex = 0;
        onLoadMenu();
    }

    void Update() {
        CheckHotKeys();
    }

    private void CheckHotKeys() {
        if (Input.GetKeyDown(KeyCode.Alpha1) ) {
            onLoadMenu();
        }
    }


    // loads menu based on menu's status
    public void onLoadMenu() {
        // will update coins to match the current player's total coin count 
        yourCoinsText.text = "" + GlobalVariables.totalCoins;

        int i = 0;
        // will update items' views to match status of purchase
        foreach (GameObject item in shopItems) {
            // check global variables for item status 
            if (GlobalVariables.costumeShopStatus[i] == "equipped") {
                setAsEquipped(i, false);
            } else if (GlobalVariables.costumeShopStatus[i] == "canEquip") {
                setAsCanEquip(i);
            } else if (GlobalVariables.costumeShopStatus[i] == "canBuy") {
                setAsCanBuy(i);
            } else if (GlobalVariables.costumeShopStatus[i] == "locked") {
                setAsLocked(i);
            } else {
                Debug.Log ("Error loading item status!");
                setAsCanBuy(i);
            }
            i++;
        }

        // set the current item on page as the first item 
        // currentItemOnPageIndex = 0;
        shopItems[currentItemOnPageIndex].SetActive(true);

    }

    public void setAsEquipped(int itemIndex, bool fromButtonClick) { // user has equipped item 
        // get shop game object
        GameObject item = shopItems[itemIndex];

        // get the "PriceBackground" child object
        GameObject priceBackground = item.transform.GetChild(3).gameObject;

        // set the button sprite to green 
        priceBackground.GetComponent<Image>().sprite = greenButton;

        // set the correct dimensions of the button 
        priceBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100);

        // set coin logo to inactive 
        priceBackground.transform.GetChild(0).gameObject.SetActive(false);

        // set button text 
        GameObject priceText = priceBackground.transform.GetChild(1).gameObject;
        priceText.GetComponent<TextMeshProUGUI>().text = "Equipped";
        priceText.GetComponent<RectTransform>().anchoredPosition = new Vector3(20f, -4f, 0f);

        // reset current equipped item to equippable
        // if (currentItemEquippedIndex != -1) {
        //     GlobalVariables.costumeShopStatus[currentItemEquippedIndex] = "canEquip";
        // }
        
        if (fromButtonClick && (currentItemEquipped != null)) {
            GlobalVariables.costumeShopStatus[currentItemEquippedIndex] = "canEquip";
            setAsCanEquip(currentItemEquippedIndex);
        }

        // set as current equipped item 
        currentItemEquipped = item; 
        currentItemEquippedIndex = itemIndex;

        // foreach (GameObject listItem in itemsCanEquip) {
        //     setAsCanEquip(listItem);
        // }
        GlobalVariables.costumeShopStatus[itemIndex] = "equipped";

        Anim.SetBool(animationBoolNames[itemIndex], true);
        costumeGameObjects[itemIndex].SetActive(true);
        GlobalVariables.equippedCostumeAnimName = animationBoolNames[itemIndex];

        priceBackground.transform.GetChild(2).gameObject.SetActive(false);
        priceBackground.transform.GetChild(3).gameObject.SetActive(false);

    }

    public void setAsUnequipped(int itemIndex) {
        // get shopp game object
        GameObject item = shopItems[itemIndex];

        // get the "PriceBackground" child object
        GameObject priceBackground = item.transform.GetChild(3).gameObject;
        
        // set the correct dimensions of the button 
        priceBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(215, 100);     
        priceBackground.GetComponent<Image>().sprite = blueButton;

        GameObject priceText = priceBackground.transform.GetChild(1).gameObject;
        priceText.GetComponent<TextMeshProUGUI>().text = "Equip Item"; 
        priceText.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, -4f, 0f);

        // update item state 
        GlobalVariables.costumeShopStatus[itemIndex] = "canEquip";

        currentItemEquipped = null;
        currentItemEquippedIndex = -1;

        Anim.SetBool(animationBoolNames[itemIndex], false);
        GlobalVariables.equippedCostumeAnimName = "";
        costumeGameObjects[itemIndex].SetActive(false);

        priceBackground.transform.GetChild(2).gameObject.SetActive(false);
        priceBackground.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void setAsCanEquip(int itemIndex) { // player has bought the item, avail to equip now 
        // get shopp game object
        GameObject item = shopItems[itemIndex];

        // get the "PriceBackground" child object
        GameObject priceBackground = item.transform.GetChild(3).gameObject;

        // set the button sprite to green 
        priceBackground.GetComponent<Image>().sprite = blueButton;

        // set the correct dimensions of the button 
        priceBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(215, 100);

        // set coin logo to inactive 
        priceBackground.transform.GetChild(0).gameObject.SetActive(false);

        // set button text 
        GameObject priceText = priceBackground.transform.GetChild(1).gameObject;
        priceText.GetComponent<TextMeshProUGUI>().text = "Equip Item";
        priceText.GetComponent<RectTransform>().anchoredPosition = new Vector3(4f, -4f, 0f);

        // update item state 
        GlobalVariables.costumeShopStatus[itemIndex] = "canEquip";

        Anim.SetBool(animationBoolNames[itemIndex], false);
        costumeGameObjects[itemIndex].SetActive(false);
        
        priceBackground.transform.GetChild(2).gameObject.SetActive(false);
        priceBackground.transform.GetChild(3).gameObject.SetActive(false);

    }

    public void setAsCanBuy(int itemIndex) { // player has unclocked item, avail to buy 
        // get shopp game object
        GameObject item = shopItems[itemIndex];

        // get the "PriceBackground" child object
        GameObject priceBackground = item.transform.GetChild(3).gameObject;

        // set the button sprite to blue 
        priceBackground.GetComponent<Image>().sprite = blueButton;

        // set the correct dimensions of the button 
        priceBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(215, 100);

        // set coin logo to active 
        priceBackground.transform.GetChild(0).gameObject.SetActive(true);

        // set button text 
        GameObject priceText = priceBackground.transform.GetChild(1).gameObject;
        priceText.GetComponent<TextMeshProUGUI>().text = "" + shopPrices[itemIndex];
        priceText.GetComponent<RectTransform>().anchoredPosition = new Vector3(80.4f, -4f, 0f);

        priceBackground.transform.GetChild(2).gameObject.SetActive(false);
        priceBackground.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void setAsLocked(int itemIndex) {
        // get shopp game object
        GameObject item = shopItems[itemIndex];
        
        GameObject priceBackground = item.transform.GetChild(3).gameObject;
        priceBackground.transform.GetChild(2).gameObject.SetActive(true);
        priceBackground.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void onButtonClick(int itemIndex) {
        // check the current state of the item 
        string buttonState = GlobalVariables.costumeShopStatus[itemIndex];

        if (buttonState == "equipped") { // item is equipped, do nothing on click 
            setAsUnequipped(itemIndex);
        } else if (buttonState == "canEquip") { // item has been bought, equip item on click
            setAsEquipped(itemIndex, true);
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

    public void onRightArrow() {
        // if on the first item, enable left arrow 
        if (currentItemOnPageIndex == 0) {
            leftArrow.SetActive(true);
        }

        // if going to the last item, disable right arrow
        if (currentItemOnPageIndex == 2) {
            rightArrow.SetActive(false);
        }

        // disable current item 
        shopItems[currentItemOnPageIndex].SetActive(false);
    
        // get next item & update reference & set active
        currentItemOnPageIndex += 1;
        shopItems[currentItemOnPageIndex].SetActive(true);
    }

    public void onLeftArrow() {
        // if going left from the second item, disable left arrow 
        if (currentItemOnPageIndex == 1) {
            leftArrow.SetActive(false);
        }

        // if going left from the last item, enable right arrow
        if (currentItemOnPageIndex == 3) {
            rightArrow.SetActive(true);
        }

        // disable current item 
        shopItems[currentItemOnPageIndex].SetActive(false);
    
        // get next item & update reference & set active
        currentItemOnPageIndex -= 1;
        shopItems[currentItemOnPageIndex].SetActive(true);
    }
}
