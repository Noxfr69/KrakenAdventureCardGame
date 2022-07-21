using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class LostManager : MonoBehaviour
{


    DeckManager DM;
    GameObject deckManager;

    public List<CardInfoDisplay> deckDisplay = new List<CardInfoDisplay>();
    public CardInfoDisplay confirmationCard;

    public GameObject CardComfiramtionUI;
    public int CurrentOffset = 0;
    Input inputController;
    public static event Action GameContinue;

    private void OnEnable() { inputController.Enable(); }
    private void OnDisable() { inputController.Disable(); }

    private void Awake() {

        deckManager = GameObject.Find("DeckManager");
        DM = deckManager.GetComponent<DeckManager>();
        CardComfiramtionUI.SetActive(false);
        
        inputController = new Input();

        //reset all data in case of a continue runned
        DM.VisitedEventIDs.Clear();
        SavedDATA.BoatPosition = new Vector2(0,0);
        GameObject bar = GameObject.Find("FinalBoss_Bar");
        var finalbar = bar.GetComponentInChildren<BossBar>();
        finalbar.ResetBar();
        bar.gameObject.SetActive(false);
        

        //check if we already have saved card and remove them from the deck
        if(PlayerPrefs.GetInt("NumberOfSavedCard",0) != 0){
            int numberOfSavedCard = PlayerPrefs.GetInt("NumberOfSavedCard",0);
        if(numberOfSavedCard != 0){
            //add saved card
            for (int i = 1; i < numberOfSavedCard+1; i++)
            {
                DM.PlayerDeck.Remove(DM.CardFullList[PlayerPrefs.GetInt("SavedCard"+i.ToString())]);
            }
        }
        }

        SetupCardsDisplay(0);

    }

    public void SetupCardsDisplay(int Offset){
        for (int i = 0; i < deckDisplay.Count; i++)
        {   
            if(DM.PlayerDeck.Count <= i + Offset){
                deckDisplay[i].gameObject.SetActive(false);
                
            }else{
                deckDisplay[i].gameObject.SetActive(true);
                deckDisplay[i].SetNewCardID(DM.PlayerDeck[i+Offset].ID);
            }
            

        }
    }


    public void OnClickYes(){

        DM.PlayerDeck.Add(DM.CardFullList[confirmationCard.CardID]);
        int numberofSavedCard = PlayerPrefs.GetInt("NumberOfSavedCard",0);
        if(numberofSavedCard == 0){
            PlayerPrefs.SetInt("NumberOfSavedCard", 1);
            PlayerPrefs.SetInt("SavedCard1", DM.CardFullList[confirmationCard.CardID].ID);
        }else{
            numberofSavedCard++;
            PlayerPrefs.SetInt("NumberOfSavedCard", numberofSavedCard);
            PlayerPrefs.SetInt("SavedCard"+numberofSavedCard.ToString(), DM.CardFullList[confirmationCard.CardID].ID);
        }
        
       GameContinue?.Invoke();
    
    }

    public void OnClickNo(){
        CardComfiramtionUI.SetActive(false);
    }

    public void OnclickRight(){
        SetupCardsDisplay(CurrentOffset+5);
        CurrentOffset = CurrentOffset+5;
    }

    public void OnClickLeft(){
        var x = CurrentOffset - 5;
        if(x<0){return;}
        CurrentOffset = CurrentOffset-5;
        SetupCardsDisplay(CurrentOffset);

    }


    private void Update() {
        if(inputController.Normal.Mouse_Left_Hold.inProgress){

            if(EventSystem.current.IsPointerOverGameObject())return;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if(hit2D.collider != null){
                var clickedCard = hit2D.collider.gameObject.GetComponent<CardInfoDisplay>();
                confirmationCard.SetNewCardID(clickedCard.CardID);
                CardComfiramtionUI.SetActive(true);


            }

    }
    }

}
