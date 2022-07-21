using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardInfoDisplay : MonoBehaviour
{
   public TextMeshPro Name;
   public TextMeshPro Description;
   public TextMeshPro Attack;
   public TextMeshPro Armor;
   public TextMeshPro Health;
   public SpriteRenderer Image;
   public SpriteRenderer Rarity;
   public TextMeshPro SellPrice;
   public GameObject SellTag;
   public TextMeshPro BuyPrice;
   public GameObject BuyTag;
   public int CardID;
   GameObject DM;
   DeckManager deckManager;


    void Awake() {
        
    }


    public void SetNewCardID(int ID){
        DM = GameObject.Find("DeckManager");
        deckManager = DM.gameObject.GetComponent<DeckManager>();
        ////////////////////////set up the card ////////////////
        Name.text = deckManager.CardFullList[ID].name;
        Description.text = deckManager.CardFullList[ID].Description;
        Attack.text = deckManager.CardFullList[ID].Damage.ToString();
        Armor.text = deckManager.CardFullList[ID].Armor.ToString();
        Health.text = deckManager.CardFullList[ID].HealthRegen.ToString();
        Image.sprite = deckManager.CardFullList[ID].Image;
        CardID = deckManager.CardFullList[ID].ID;
        #region buy and sell info
        if(SellTag != null){
            SellPrice.text = deckManager.CardFullList[ID].Sell_Price.ToString();
            BuyPrice.text = deckManager.CardFullList[ID].Buy_Price.ToString();

        }
        #endregion
        #region Rarity
        if(deckManager.CardFullList[ID].Rarity == 1){
            Rarity.color = new Color32(151,151,151,255);
            return;
        }
        if(deckManager.CardFullList[ID].Rarity == 2){
            Rarity.color = new Color32(29,65,212,255);
            return;
        }
        if(deckManager.CardFullList[ID].Rarity == 3){
            Rarity.color = new Color32(212,29,185,255);
            return;
        }
        if(deckManager.CardFullList[ID].Rarity == 4){
            Rarity.color = new Color32(255,207,0,255);
            return;
        }
        if(deckManager.CardFullList[ID].Rarity == 5){
            Rarity.color = new Color32(91,243,231,255);
            return;
        }
        #endregion
    }
}
