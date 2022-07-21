using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCardPlay : MonoBehaviour
{

    DeckManager DM;
    CardInfoDisplay cinfo;
    OnCardCraft craftsysteme;
    public GameObject PlayerStats;

    private void OnEnable() {
        DM = GetComponent<DeckManager>();
        craftsysteme = DM.GetComponent<OnCardCraft>();
    }


    public void CardPlayed(GameObject card){
        //Check if it's not the same card that was in crafting zone the reset the crafting zone data
        if(card != null){
            if(card == craftsysteme.Card1){
                craftsysteme.numberOfCardInCraft = 0;
                craftsysteme.Card1 = null;
        }


        cinfo = card.GetComponent<CardInfoDisplay>();
        var x = DM.PlayerHand.FindIndex(x => x.ID == cinfo.CardID);
        DM.PlayerPlayed.Add(DM.PlayerHand[x]);
        DM.PlayerHand.Remove(DM.PlayerHand[x]);
        DM.DrawnCard--;
        StartCoroutine(DM.HandleCombatUI());
        AddBoatStats(DM.CardFullList[cinfo.CardID].Damage, DM.CardFullList[cinfo.CardID].Armor, DM.CardFullList[cinfo.CardID].HealthRegen);
        Destroy(card,0.06f);
        }
        
        
    }

    public void AddBoatStats(int Damage, int Armor, int Health){
        GameObject combat_UI = GameObject.Find("Combat_UI");
        Combat_UI c_UI = combat_UI.GetComponent<Combat_UI>();
        Player_Boat_Stats boat_Stats = PlayerStats.GetComponent<Player_Boat_Stats>();

        c_UI.Boat_Attack.text = (boat_Stats.CurrentBoatAttack + Damage).ToString();
        c_UI.Boat_Defense.text = (boat_Stats.CurrentBoatArmor + Armor).ToString();
        if(boat_Stats.CurrentBoatHealth + Health > boat_Stats.BaseHealthStat){
            c_UI.Boat_Health.text = boat_Stats.BaseHealthStat.ToString();
        }else{
            c_UI.Boat_Health.text = (boat_Stats.CurrentBoatHealth + Health).ToString();
        } 
        


        //now thats it's displayed lets save the new value 

        boat_Stats.CurrentBoatAttack = boat_Stats.CurrentBoatAttack + Damage;
        boat_Stats.CurrentBoatArmor = boat_Stats.CurrentBoatArmor + Armor;
        if(boat_Stats.CurrentBoatHealth + Health < boat_Stats.BaseHealthStat){
            boat_Stats.CurrentBoatHealth = boat_Stats.CurrentBoatHealth + Health;
            //display the bar moving back up thanks to heal
            c_UI.BoatLifeBar.rectTransform.sizeDelta = new Vector2((450*boat_Stats.CurrentBoatHealth)/boat_Stats.BaseHealthStat, 39.9f);
            
        }else{
            boat_Stats.CurrentBoatHealth = boat_Stats.BaseHealthStat;
            c_UI.BoatLifeBar.rectTransform.sizeDelta = new Vector2(450, 39.9f);
        
        }
               
        
    }
}
