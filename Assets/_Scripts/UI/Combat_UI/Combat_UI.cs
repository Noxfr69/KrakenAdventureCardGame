using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using TMPro;

public class Combat_UI : MonoBehaviour
{
    public static event Action clickDraw;
    public static event Action clickShuffle;
    public Button drawButton;
    public Button ShuffleButton;
    public TMP_Text DeckCount;
    public TMP_Text PlayedCount;
    public TMP_Text Boat_Attack;
    public TMP_Text Boat_Health;
    public TMP_Text Boat_Defense;
    public TMP_Text Ennemy_Boat_Attack;
    public TMP_Text Ennemy_Boat_Health;
    public TMP_Text Ennemy_Boat_Defense;
    public TMP_Text Ennemy_Boat_Name;
    public Image BoatLifeBar;
    public Image EnnemyBoatLifeBar;
    public int EnnemyBoatLife;

    public bool canfight = false;

    public GameObject TutoCombatUI;
    public GameObject CombatIGUI;
    GameObject DeckManager;
    DeckManager DM;
    GameObject PlayerBoatStats;
    Player_Boat_Stats Boat_Stats;
    Input inputController;
    int count = 0;
    public AudioSource Combat_SFX;
    public AudioClip card_take;
    public AudioClip card_Put;
    public AudioClip fight;

    public Ennemy_SO WorldBoss;
    
    private void OnEnable() { inputController.Enable(); }
    private void OnDisable() { inputController.Disable(); }


    private void Awake() 
    { 
        inputController = new Input();
        drawButton.gameObject.SetActive(false);
        ShuffleButton.gameObject.SetActive(false);
        DeckManager = GameObject.Find("DeckManager");
        DM = DeckManager.GetComponent<DeckManager>();
        PlayerBoatStats = GameObject.Find("Player_Stats");
        Boat_Stats = PlayerBoatStats.GetComponent<Player_Boat_Stats>();

        drawButton.image.sprite = DM.ChosenCardColor;
        ShuffleButton.image.sprite = DM.ChosenCardColor;

        //refresh the boat stats 
        OnCardPlay oCP = DM.GetComponent<OnCardPlay>();
        oCP.AddBoatStats(0,0,0);

        //SetUP an Ennemy
        EnnemySetUp();
        PlayerBoatStatRefresh();
        Debug.Log("refreshed");

        //FillUpLife bars
        BoatLifeBar.rectTransform.sizeDelta = new Vector2(450, 39.9f);
        EnnemyBoatLifeBar.rectTransform.sizeDelta = new Vector2(450, 39.9f);

        //Set game bool 
        DM.IsGameOver = false;
        canfight = false;

        //cache Combat ui in the deckmanager
        DM.CacheCombatUI(this, this.gameObject);
    }


    public void OnclickDraw()
    {
        clickDraw?.Invoke();
        Debug.Log("draw invoked");
    }

    public void OnclickShuffle(){
        clickShuffle?.Invoke();
    }

    private void Update() {
         
        if(inputController.Normal.Click.IsPressed() && count ==0){
        // take the tuto screen away and show the draw button
        if(TutoCombatUI.gameObject.activeSelf && count ==0){
            count =1;
            TutoCombatUI.gameObject.SetActive(false);
            drawButton.gameObject.SetActive(true);
            DeckCount.text = DM.PlayerDeck.Count.ToString();
            PlayedCount.text = DM.PlayerPlayed.Count.ToString();

            return;
        }
        }

        if(inputController.Normal.Click.ReadValue<float>() != 0 && count ==1){

            CombatIGUI.gameObject.SetActive(true);
        }
        if(inputController.Normal.Click.ReadValue<float>() == 0 && count ==1){
            CombatIGUI.gameObject.SetActive(false);
        }
    }


    public void EnnemySetUp(){

        if(WorldBoss != null){
        Ennemy_Boat_Attack.text = WorldBoss.Attack.ToString();
        Ennemy_Boat_Defense.text = WorldBoss.Armor.ToString();
        Ennemy_Boat_Health.text = WorldBoss.Health.ToString();
        EnnemyBoatLife = WorldBoss.Health;
        Ennemy_Boat_Name.text = WorldBoss.Name;

        }else{

        Ennemy_Boat_Attack.text = DM.CurrentEnemy.Attack.ToString();
        Ennemy_Boat_Defense.text = DM.CurrentEnemy.Armor.ToString();
        Ennemy_Boat_Health.text = DM.CurrentEnemy.Health.ToString();
        EnnemyBoatLife = DM.CurrentEnemy.Health;
        Ennemy_Boat_Name.text = DM.CurrentEnemy.Name;
        }

    }

    public void PlayerBoatStatRefresh(){
        Boat_Stats.CurrentBoatArmor = Boat_Stats.BaseArmorStat;
        Boat_Stats.CurrentBoatAttack = Boat_Stats.BaseAttackStat;
        Boat_Stats.CurrentBoatHealth = Boat_Stats.BaseHealthStat;

        Boat_Attack.text = Boat_Stats.CurrentBoatAttack.ToString();
        Boat_Health.text = Boat_Stats.CurrentBoatHealth.ToString();
        Boat_Defense.text = Boat_Stats.CurrentBoatArmor.ToString();
    }
    
}
