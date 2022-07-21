using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PortManager : MonoBehaviour
{
    public GameObject ShopOpenButton;
    
    public List<CardInfoDisplay> PortCard = new List<CardInfoDisplay>();
    public List<int> CardOfthisShopIDs = new List<int>();
    public GameObject Shop;
    public GameObject Shop_Buttons;
    public GameObject Buy_Screen;
    public GameObject Buy_Screen_Buttons;
    public TextMeshPro Buy_Text;
    public CardInfoDisplay Buy_Card;
    public GameObject Sell_Screen;
    public GameObject Sell_Screen_Buttons;
    public CardInfoDisplay Sell_Card;
    public TextMeshPro Sell_Text;
    public TextMeshProUGUI MoneyDisplay;
    public Image moneybackground;

    Input inputController;
    GameObject DeckManager;
    DeckManager DM;
    LootTables PortLootTable;

    int ThisShopNumberOfcard;
    int CurrentOffset = 0;
    bool IsBuyingCard = true;
    bool Isdeciding = false;
    Color32 moneybackgroundColor = new Color32(231,171,104,255);


    private void OnEnable() { inputController.Enable(); }
    private void OnDisable() { inputController.Disable(); }






    private void Awake() {
        //Get relevent data 
        DeckManager = GameObject.Find("DeckManager");
        DM = DeckManager.GetComponent<DeckManager>();
        inputController = new Input();
        PortLootTable = DM.CurrentLootTable;
        
        // make a random sell card set for the shop from the lootTable
        CardOfthisShopIDs.Clear();
        ThisShopNumberOfcard = Random.Range(6,16);
        Debug.Log(ThisShopNumberOfcard);

        for (int i = 0; i < ThisShopNumberOfcard; i++)
        {
            int x = Random.Range(0, PortLootTable.table.Count);
            CardOfthisShopIDs.Add(PortLootTable.table[x]);
        }     

        //Set the shop to inactive
        ShopOpenButton.SetActive(true);
        Shop.SetActive(false);
        Shop_Buttons.SetActive(false);
        Buy_Screen.SetActive(false);
        Buy_Screen_Buttons.SetActive(false);
        Sell_Screen.SetActive(false);
        Sell_Screen_Buttons.SetActive(false);
    }

    public void SetupCardDisplay(int Offset){
  
          if(IsBuyingCard){
            
            for (int i = 0; i < PortCard.Count; i++)
            {
                if(CardOfthisShopIDs.Count <= i + Offset){
                    PortCard[i].gameObject.SetActive(false);
                }else{
                    PortCard[i].gameObject.SetActive(true);
                    PortCard[i].SetNewCardID(CardOfthisShopIDs[i+Offset]);
                    PortCard[i].SellTag.gameObject.SetActive(false);
                    PortCard[i].BuyTag.gameObject.SetActive(true);                    
                }


            }

        }else{
            for (int i = 0; i < PortCard.Count; i++)
            {
                if(DM.PlayerDeck.Count <= i + Offset){
                    Debug.Log("this card doenst exist");
                    PortCard[i].gameObject.SetActive(false);
                }else{
                    PortCard[i].gameObject.SetActive(true);
                    PortCard[i].SetNewCardID(DM.PlayerDeck[i+Offset].ID);
                    PortCard[i].SellTag.gameObject.SetActive(true);
                    PortCard[i].BuyTag.gameObject.SetActive(false);
                }
            }
        }
    }


    public void OnShopOpen(){
        Shop.SetActive(true);
        Shop_Buttons.SetActive(true);
        CurrentOffset = 0;
        SetupCardDisplay(CurrentOffset);
        MoneyDisplay.text = SavedDATA.PlayerMoney.ToString();
        ShopOpenButton.SetActive(false);
    }
    public void OnCloseShop (){
        Shop.SetActive(false);
        Shop_Buttons.SetActive(false);
        CurrentOffset = 0;    
        ShopOpenButton.SetActive(true);   
    }


    public void OnArrowRight(){
        CurrentOffset = CurrentOffset+8;
        SetupCardDisplay(CurrentOffset);
    }
    public void OnArrowLeft(){
        var x = CurrentOffset - 8;
        if(x<0){ CurrentOffset = 0; return;}
        CurrentOffset = CurrentOffset-8;
        SetupCardDisplay(CurrentOffset);
    }

    public void OnClickBuyShopButton(){
        IsBuyingCard = true;
        CurrentOffset = 0;
        SetupCardDisplay(CurrentOffset);
    }
    public void OnClickSellShopButton(){
        IsBuyingCard = false;
        CurrentOffset = 0;
        SetupCardDisplay(CurrentOffset);
    }

    public void OnClickSell(){
        // remove the card from the deck
        DM.PlayerDeck.Remove(DM.CardFullList[Sell_Card.CardID]);
        //Add money to the general count and display it
        StartCoroutine(moneyChange(DM.CardFullList[Sell_Card.CardID].Sell_Price));
        //display the deck again 
        SetupCardDisplay(CurrentOffset);
        //clsoe everything 
        Isdeciding = false;
        Sell_Screen.SetActive(false);
        Sell_Screen_Buttons.SetActive(false);

    }
    public void OnclickNoSellNo(){
        Isdeciding = false;
        Sell_Screen.SetActive(false);
        Sell_Screen_Buttons.SetActive(false);
    }
    public void OnclickBuy(){
        if(SavedDATA.PlayerMoney >= DM.CardFullList[Buy_Card.CardID].Buy_Price){
            //add the card to the deck 
            DM.PlayerDeck.Add(DM.CardFullList[Buy_Card.CardID]);
            //remove the card from the list of possible card of the shop
            CardOfthisShopIDs.Remove(Buy_Card.CardID);
            //set up the display of card again 
            SetupCardDisplay(CurrentOffset);
            //change the money
            StartCoroutine(moneyChange(-DM.CardFullList[Buy_Card.CardID].Buy_Price));
            Debug.Log(-DM.CardFullList[Sell_Card.CardID].Buy_Price);
            //close buy window
            Isdeciding = false;
            Buy_Screen.SetActive(false);
            Buy_Screen_Buttons.SetActive(false); 
        }else{
            StartCoroutine(TooPoor());
        }
    }
    public void OnclickBuyNo(){
        Isdeciding = false;
        Buy_Screen.SetActive(false);
        Buy_Screen_Buttons.SetActive(false);        
    }

    public IEnumerator TooPoor(){
        moneybackground.color = Color.red;
        yield return new WaitForSecondsRealtime(0.05f);
        moneybackground.color = moneybackgroundColor;
        yield return new WaitForSecondsRealtime(0.05f);
        moneybackground.color = Color.red;
        yield return new WaitForSecondsRealtime(0.05f);
        moneybackground.color = moneybackgroundColor;

    }

    public IEnumerator moneyChange(int change){

        SavedDATA.PlayerMoney = SavedDATA.PlayerMoney + change;
        MoneyDisplay.text = SavedDATA.PlayerMoney.ToString();

        moneybackground.color = Color.grey;
        yield return new WaitForSecondsRealtime(0.05f);
        moneybackground.color = moneybackgroundColor;
        yield return new WaitForSecondsRealtime(0.05f);
        moneybackground.color = Color.grey;
        yield return new WaitForSecondsRealtime(0.05f);
        moneybackground.color = moneybackgroundColor;       
    }


    private void Update() {


         if(inputController.Normal.Mouse_Left_Hold.inProgress && !Isdeciding){

            if(EventSystem.current.IsPointerOverGameObject())return;
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if(hit2D.collider != null){
                var clickedCard = hit2D.collider.gameObject.GetComponent<CardInfoDisplay>();
                if(IsBuyingCard){
                    Isdeciding = true;
                    Buy_Screen.SetActive(true);
                    Buy_Screen_Buttons.SetActive(true);
                    Buy_Card.SetNewCardID(clickedCard.CardID);
                    Buy_Card.SellPrice.gameObject.SetActive(false);
                    Buy_Text.text = "Are you sure you want to buy this card for : "+ DM.CardFullList[Buy_Card.CardID].Buy_Price.ToString() + " ?";

                }else{
                    Isdeciding = true;
                    Sell_Screen.SetActive(true);
                    Sell_Screen_Buttons.SetActive(true);
                    Sell_Card.SetNewCardID(clickedCard.CardID);
                    Sell_Card.BuyPrice.gameObject.SetActive(false);
                    Sell_Text.text = "Are you sure you want to sell this card for : "+DM.CardFullList[Sell_Card.CardID].Sell_Price.ToString() + " ?";
                }
            }
        }
    }

}
