using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatWinScreen : MonoBehaviour
{
    public CardInfoDisplay card1;
    public CardInfoDisplay card2;
    public CardInfoDisplay card3;

    public int Reward1ID;
    public int Reward2ID;
    public int Reward3ID;

    DeckManager DM;
    LootTables lootTables;

    public static event Action FightIsOver;
    




    private void Awake() {

        //get ref to the deck
        GameObject deckm = GameObject.Find("DeckManager");
        DM = deckm.GetComponent<DeckManager>();
        lootTables = DM.CurrentLootTable;

        // pick 3 random reward

        int x = UnityEngine.Random.Range(0, lootTables.table.Count);
        Reward1ID = lootTables.table[x];
        int y = UnityEngine.Random.Range(0, lootTables.table.Count);
        Reward2ID = lootTables.table[y];
        int z = UnityEngine.Random.Range(0, lootTables.table.Count);
        Reward3ID = lootTables.table[z];


        //Set all cards 

        card1.SetNewCardID(Reward1ID);
        card2.SetNewCardID(Reward2ID);
        card3.SetNewCardID(Reward3ID);

        //Set the gameobject to inactive during the fight
    }



    public void OnclickCard1(){
        DM.PlayerDeck.Add(DM.CardFullList[Reward1ID]);
        DM.PutAllCardInDeck();
        FightIsOver?.Invoke();
    }


    public void OnclickCard2(){
        DM.PlayerDeck.Add(DM.CardFullList[Reward2ID]);
        DM.PutAllCardInDeck();
        FightIsOver?.Invoke();
    }


    public void OnclickCard3(){
        DM.PlayerDeck.Add(DM.CardFullList[Reward3ID]);
        DM.PutAllCardInDeck();
        FightIsOver?.Invoke();
    }


}
