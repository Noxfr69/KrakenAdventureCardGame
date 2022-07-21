using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using System;



public class DeckManager : MonoBehaviour
{
  #region Variable
    public List<Card_SO> CardFullList = new List<Card_SO>();

    public List<Card_SO> PlayerDeck = new List<Card_SO>();
    public List<Card_SO> PlayerPlayed = new List<Card_SO>();
    public List<Card_SO> PlayerHand = new List<Card_SO>();
    public GameObject cardPrefab;
    GameObject Slots;
    public List<Transform> slotsTransform = new List<Transform>();
    public int DrawnCard = 0;
    GameObject win;
    GameObject winIG;
    public GameObject PlayerStats;

    public Sprite CardGreen;
    public Sprite CardBlue;
    public Sprite CardYellow;
    public Sprite ChosenCardColor;

    public static event Action PlayerLost;
    public bool IsGameOver = false;
    public bool IsGamePaused = false;

    //////events :
    public List<int> VisitedEventIDs = new List<int>();
    public int CurrentEventID;
    public LootTables CurrentLootTable;
    public Sprite CurrentEventSprite;
    public Ennemy_SO CurrentEnemy;

    Combat_UI  cUI;
    GameObject Combat_UI_GO;


#endregion
    
    
    
    private void Awake() {
        Combat_UI.clickDraw += DrawCards;
        Combat_UI.clickShuffle += ShuffleCard;
        Debug.Log(PlayerDeck.Count);
    }

    public void DrawCards(){
        if(PlayerDeck.Count == 0){
            return;}
        OnCardCraft cardcraft = GetComponent<OnCardCraft>();
        if(PlayerHand.Count ==1 && cardcraft.numberOfCardInCraft == 1){ // draw 4 card
            for (int i = 0; i < 4; i++)
            {
                if(PlayerDeck.Count == 0) return;
                var newcard = Instantiate(cardPrefab, slotsTransform[i].position, Quaternion.identity);
                CardSlot CS = slotsTransform[i].gameObject.GetComponent<CardSlot>();
                CS.IsSlotActive = true;
                CardInfoDisplay display = newcard.gameObject.GetComponent<CardInfoDisplay>();
                var x = UnityEngine.Random.Range(0,PlayerDeck.Count);
                var y = PlayerDeck[x].ID;
                display.SetNewCardID(y);
                PlayerHand.Add(PlayerDeck[x]);
                PlayerDeck.Remove(PlayerDeck[x]);
                DrawnCard++;
                StartCoroutine(HandleCombatUI());


            }
        }
        if(PlayerHand.Count == 0){ // draw 5 card 
            for (int i = 0; i < 5; i++)
            {
                if(PlayerDeck.Count == 0) return;
                var newcard = Instantiate(cardPrefab, slotsTransform[i].position, Quaternion.identity);
                CardSlot CS = slotsTransform[i].gameObject.GetComponent<CardSlot>();
                CS.IsSlotActive = true;
                CardInfoDisplay display = newcard.gameObject.GetComponent<CardInfoDisplay>();
                var x =  UnityEngine.Random.Range(0,PlayerDeck.Count);
                var y = PlayerDeck[x].ID;
                display.SetNewCardID(y);
                PlayerHand.Add(PlayerDeck[x]);
                PlayerDeck.Remove(PlayerDeck[x]);
                DrawnCard++;
                StartCoroutine(HandleCombatUI());


            }
        }

        
    }

    public IEnumerator getSlots(){
        slotsTransform.Clear();
        yield return new WaitForSecondsRealtime(0.05f);
        win = GameObject.Find("WinScreen");
        win.SetActive(false);
        winIG = GameObject.Find("WinScreen_IG");
        winIG.SetActive(false);
        Slots = GameObject.Find("Slots");
        var Slot1 = Slots.transform.Find("Slot1");
        var Slot2 = Slots.transform.Find("Slot2");
        var Slot3 = Slots.transform.Find("Slot3");
        var Slot4 = Slots.transform.Find("Slot4");
        var Slot5 = Slots.transform.Find("Slot5");
        slotsTransform.Add(Slot1);
        slotsTransform.Add(Slot2);
        slotsTransform.Add(Slot3);
        slotsTransform.Add(Slot4);
        slotsTransform.Add(Slot5);
    }


    public IEnumerator HandleCombatUI(){

        OnCardCraft cardcraft = GetComponent<OnCardCraft>();
        cUI.DeckCount.text = PlayerDeck.Count.ToString();
        cUI.PlayedCount.text = PlayerPlayed.Count.ToString();
        if(PlayerHand.Count == 0 || (PlayerHand.Count ==1 && cardcraft.numberOfCardInCraft == 1 || cUI.canfight == false)){
            if(cUI.canfight != true){
                yield return new WaitForSecondsRealtime(0.05f);
                cUI.canfight = true;
                
            }else{//start coroutine for the fight and then set ui active
            StartCoroutine(FightLogic());}
            
        }else cUI.drawButton.gameObject.SetActive(false);
        




    }

    public void ShuffleCard(){

        for (int i = 0; i < PlayerPlayed.Count; i++)
        {
            PlayerDeck.Add(PlayerPlayed[i]);
        }
        PlayerPlayed.Clear();
        cUI.ShuffleButton.gameObject.SetActive(false);
        cUI.DeckCount.text = PlayerDeck.Count.ToString();
        cUI.PlayedCount.text = PlayerPlayed.Count.ToString();
        
        
    }

    private void OnDisable() {
        Combat_UI.clickDraw -= DrawCards;
        Combat_UI.clickShuffle -= ShuffleCard;
    }



    public IEnumerator FightLogic(){
        IsGamePaused = true;
        yield return new WaitForSeconds(1);
        //play fight sound TODO


        //Collect all the data we need
        Player_Boat_Stats Player = PlayerStats.GetComponent<Player_Boat_Stats>();


        //Let deal damage to each other 
        int DamageTaken = CurrentEnemy.Attack - Player.CurrentBoatArmor;
        int DamageGiven = Player.CurrentBoatAttack - CurrentEnemy.Armor;

        if(DamageTaken >= 0){Player.CurrentBoatHealth = Player.CurrentBoatHealth - DamageTaken;}
        if(DamageGiven >= 0){cUI.EnnemyBoatLife = cUI.EnnemyBoatLife - DamageGiven;}

        //restart the player stat to the original one
        Player.CurrentBoatArmor = Player.BaseArmorStat;
        Player.CurrentBoatAttack = Player.BaseAttackStat;
        cUI.Boat_Attack.text = Player.CurrentBoatAttack.ToString();
        cUI.Boat_Defense.text = Player.CurrentBoatArmor.ToString();
        

        //Display the damage 
        cUI.Combat_SFX.PlayOneShot(cUI.fight);
        cUI.Ennemy_Boat_Health.text = cUI.EnnemyBoatLife.ToString();
        cUI.EnnemyBoatLifeBar.rectTransform.sizeDelta = new Vector2((450*cUI.EnnemyBoatLife)/CurrentEnemy.Health, 39.9f);
        yield return new WaitForSeconds(1);

        cUI.Boat_Health.text = Player.CurrentBoatHealth.ToString();
        cUI.BoatLifeBar.rectTransform.sizeDelta = new Vector2((450*Player.CurrentBoatHealth)/Player.BaseHealthStat, 39.9f);
        yield return new WaitForSeconds(1);
        //Check if we have a winner
        if(Player.CurrentBoatHealth <= 0){
            Debug.Log("YOU Lost...");
            LostBattle();
            //Player Loose screen
        }

        if(cUI.EnnemyBoatLife <= 0){
            Debug.Log("YOU WIN !!!");
            WinnedBattle();
        }


        //Make screen shake on damage





        //when everything is done Set the draw and shuffle button back to active;
        cUI.drawButton.gameObject.SetActive(true);
        if(PlayerDeck.Count < 5 ){
            cUI.ShuffleButton.gameObject.SetActive(true);
        }
        IsGamePaused = false;
    }



    public void LostBattle(){
        //Put All the card back in the deck 
        PutAllCardInDeck();
        
        PlayerLost?.Invoke();

    }


    public void WinnedBattle(){

        win.SetActive(true);
        winIG.SetActive(true);
        IsGameOver = true;


    }

    public void PutAllCardInDeck(){
        if(PlayerHand.Count != 0){
            PlayerDeck.AddRange(PlayerHand);
            PlayerHand.RemoveAll(x=>x);
        }
        
        if(PlayerPlayed.Count != 0){
            PlayerDeck.AddRange(PlayerPlayed);
            PlayerPlayed.RemoveAll(x => x);
        }
    }

    public void CacheCombatUI(Combat_UI c_UI, GameObject CombatUI_GO){
        cUI = c_UI;
        Combat_UI_GO = CombatUI_GO;
    }


}

