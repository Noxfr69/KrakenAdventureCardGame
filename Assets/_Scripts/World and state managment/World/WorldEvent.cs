using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldEvent : MonoBehaviour
{
    
    public static event Action OncolliderAttack;
    public static event Action OncolliderPort;
    public static event Action OncolliderIsland;


    public Events_SO _event;

    GameObject deckmanager;
    DeckManager DM;




    void Awake() {
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
        SR.sprite = _event.Event_mapstate;
        deckmanager = GameObject.Find("DeckManager");
        DM = deckmanager.GetComponent<DeckManager>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(DM.VisitedEventIDs.Exists(x => x == _event.ID)){
            return;
        }else{
        GiveEventFollowData();

        Debug.Log("collided");
        if(_event.TypeID == 3){
            OncolliderAttack?.Invoke();
        }
        if(_event.TypeID == 2){
            OncolliderIsland?.Invoke();
        }
        if(_event.TypeID == 1){
            OncolliderPort?.Invoke();
        }
        }




    }

    public void GiveEventFollowData(){
        DM.VisitedEventIDs.Add(_event.ID);
        DM.CurrentEventID = _event.ID;
        DM.CurrentEventSprite = _event.Event_BackGround;
        DM.CurrentEnemy = _event.Event_ennemy;
        DM.CurrentLootTable = _event.Event_lootTable;
    }
}
