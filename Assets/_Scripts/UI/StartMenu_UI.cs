using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class StartMenu_UI : MonoBehaviour
{
    public event Action CharacterSelected;
    public Canvas ComfirmationCanvas;
    public TMP_Text CharachterChoice;
    public Canvas tutoCanvas;
    public Image tutoImage;
    public Button LetsGOButton;
    public List<Sprite> tutoImagelist = new List<Sprite>();
    GameObject WM;
    GameObject DM;
    GameObject Bossbar;
    WorldManager wdManager;
    DeckManager deckManager;
    int currenttutoImage = 0;
    


private void Awake() {
    ComfirmationCanvas.gameObject.SetActive(false);
    tutoCanvas.gameObject.SetActive(false);
    WM = GameObject.Find("WorldManager");
    wdManager = WM.GetComponent<WorldManager>();
    DM = GameObject.Find("DeckManager");
    deckManager = DM.GetComponent<DeckManager>();
    Bossbar = GameObject.Find("FinalBoss_Bar_Fill");
    var bb = Bossbar.GetComponent<BossBar>();
    bb.ResetBar();


}

    public void OnCharacterSelect1(){
        CharacterSelected?.Invoke();
        ComfirmationCanvas.gameObject.SetActive(true);
        CharachterChoice.text = "Coco Le Grand";
        deckManager.ChosenCardColor = deckManager.CardBlue;
        //set current character to 1
        //Update starting deck

    }
        public void OnCharacterSelect2(){
        CharacterSelected?.Invoke();
        ComfirmationCanvas.gameObject.SetActive(true);
        CharachterChoice.text = "Kara Le Vaillant";
        deckManager.ChosenCardColor = deckManager.CardYellow;
        //set current character to 2
        //Update starting deck

    }
        public void OnCharacterSelect3(){
        CharacterSelected?.Invoke();
        ComfirmationCanvas.gameObject.SetActive(true);
        CharachterChoice.text = "Julien Le Juste";
        deckManager.ChosenCardColor = deckManager.CardGreen;
        //set current character to 3
        //Update starting deck

    }

    public void OnClickYes(){

        if(PlayerPrefs.GetInt("TutoDone",0) == 1){
            SetUpPlayerDeck();
        }else{
            tuto();
        }
        
        
        
    }

    public void OnclickNo(){
        ComfirmationCanvas.gameObject.SetActive(false);
    }

    public void SetUpPlayerDeck(){
        deckManager.PlayerDeck.Clear();
        deckManager.PlayerDeck.Add(deckManager.CardFullList[1]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[1]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[1]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[1]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[1]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[1]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[5]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[5]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[9]);
        deckManager.PlayerDeck.Add(deckManager.CardFullList[9]);
        int numberOfSavedCard = PlayerPrefs.GetInt("NumberOfSavedCard",0);
        Debug.Log(numberOfSavedCard);
        if(numberOfSavedCard != 0){
            //add saved card
            for (int i = 1; i < numberOfSavedCard+1; i++)
            {
                deckManager.PlayerDeck.Add(deckManager.CardFullList[PlayerPrefs.GetInt("SavedCard"+i.ToString())]);
            }
        }
        wdManager.LeaveStartMenu = true;
    }



    public void tuto(){
        PlayerPrefs.SetInt("TutoDone",1);
        tutoCanvas.gameObject.SetActive(true);
        LetsGOButton.gameObject.SetActive(false);
        tutoImage.sprite = tutoImagelist[currenttutoImage];
    }

    public void OnclickArrowRight(){
        currenttutoImage++;
        if(currenttutoImage > 4){currenttutoImage=4;}
        tutoImage.sprite = tutoImagelist[currenttutoImage];
        if(currenttutoImage==4){LetsGOButton.gameObject.SetActive(true);}

    }
    public void OnclickArrowLeft(){
        currenttutoImage--;
        if(currenttutoImage < 0){currenttutoImage = 0;}
        LetsGOButton.gameObject.SetActive(false);
        tutoImage.sprite = tutoImagelist[currenttutoImage];        

    }
}
