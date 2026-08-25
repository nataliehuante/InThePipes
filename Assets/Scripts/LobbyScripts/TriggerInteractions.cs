using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerInteractions : MonoBehaviour
{
    [Header("Menu Panels")]
    public TextMeshPro PopupBoxLevelSelect;
    public TextMeshPro PopupBoxCostumeShop;
    public TextMeshPro PopupBoxWebShop;
    public TextMeshPro PopupBoxBed;

    [Header("Script References")]
    public LobbySpecificInputs LSI;
    private Player player;
    public WebShopInteractions webShopInteractions;
    public costumeShopInteraction costumeShopInteraction;

    public void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
            return;

        // PopupBox.enabled = true;
        switch (this.gameObject.tag)
        {
            case "CostumeShopTrigger":
                // PopupBoxCostumeShop.text = "Press 'X' to open the costume shop.";
                PopupBoxCostumeShop.enabled = true;
                LSI.currentTrigger = "CostumeShop";
                costumeShopInteraction.onLoadMenu();
                // player.SetVelocityX(0f);
                break;
            case "WebShopTrigger":
                // PopupBoxWebShop.text = "Press 'X' to open the costume shop.";
                PopupBoxWebShop.enabled = true;
                LSI.currentTrigger = "WebShop";
                // webShopInteractions.onLoadMenu();
                // player.SetVelocityX(0f);
                break;
            case "LevelSelectTrigger":
                // PopupBoxLevelSelect.text = "Press 'X' to view level select.";
                PopupBoxLevelSelect.enabled = true;
                LSI.currentTrigger = "LevelSelect";
                // player.SetVelocityX(0f);
                break;
            case "BedTrigger":
                // PopupBoxBed.text = "Press 'X' to return to main menu.";
                PopupBoxBed.enabled = true;
                LSI.currentTrigger = "Bed";
                // player.SetVelocityX(0f);
                break;
            default: //Should never be reached
                Debug.Log("Something went wrong and an unreachable state was reached.");
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        if (!LSI.loadedOutOfLobby) {
            LSI.currentTrigger = "";
            if (PopupBoxLevelSelect != null)
                PopupBoxLevelSelect.enabled = false;
            if (PopupBoxBed != null)
                PopupBoxBed.enabled = false;
            if (PopupBoxCostumeShop != null)
                PopupBoxCostumeShop.enabled = false;
            if (PopupBoxWebShop != null)
                PopupBoxWebShop.enabled = false;
        }
       
    }
}
