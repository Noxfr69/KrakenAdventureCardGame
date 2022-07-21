using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestContent : MonoBehaviour
{
    public CardInfoDisplay card1;
    public CardInfoDisplay card2;
    public CardInfoDisplay card3;

    public int Reward1ID;
    public int Reward2ID;
    public int Reward3ID;

    DeckManager DM;
    LootTables lootTables;
    public Button takeallButton;
    




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


public void OnClickTakeAll(){
    DM.PlayerDeck.Add(DM.CardFullList[Reward1ID]);
    DM.PlayerDeck.Add(DM.CardFullList[Reward2ID]);
    DM.PlayerDeck.Add(DM.CardFullList[Reward3ID]);
    Destroy(card1.gameObject, 0.02f);
    Destroy(card2.gameObject, 0.02f);
    Destroy(card3.gameObject, 0.02f);
    Destroy(takeallButton,0.02f);
    this.gameObject.SetActive(false);
}
}
