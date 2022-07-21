using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCardCraft : MonoBehaviour
{

    DeckManager DM;
    DragAndDrop_Card DandD;
    public GameObject Card1;
    GameObject Card2;
    public int numberOfCardInCraft = 0;
    public CraftingRecipes craftingRecipes;
    CardInfoDisplay cinfo1;
    CardInfoDisplay cinfo2;
    CardInfoDisplay cinfoNewCard;


    private void OnEnable() {
        DM = GetComponent<DeckManager>();        
    }

    public void OnCraftTry(GameObject card){
        if(card != Card1){
        numberOfCardInCraft++;
        if(numberOfCardInCraft == 1){
            Card1 = card;
            StartCoroutine(DM.HandleCombatUI());
            GameObject Drag = GameObject.Find("Combat_UI");
            DandD = Drag.GetComponent<DragAndDrop_Card>();
            
            
        }else{
            numberOfCardInCraft = 0;
            DandD.Card = null;
            Card2 = card;
            CardInfoDisplay Cinfo1;
            Cinfo1 = Card1.GetComponent<CardInfoDisplay>();
            CardInfoDisplay Cinfo2;
            Cinfo2 = Card2.GetComponent<CardInfoDisplay>();
            if(craftingRecipes.craftingRecipe.Exists(x => x.ID1 == Cinfo1.CardID && x.ID2 == Cinfo2.CardID)){

                int index = craftingRecipes.craftingRecipe.FindIndex(x => x.ID1 == Cinfo1.CardID && x.ID2 == Cinfo2.CardID);
                int newCradID = craftingRecipes.craftingResults[index];
                var newcard =  Instantiate(DM.cardPrefab, Card1.transform.position, Quaternion.identity);
                CardInfoDisplay newCardInfoDisplay = newcard.GetComponent<CardInfoDisplay>();
                newCardInfoDisplay.SetNewCardID(newCradID);
                //Add it to the player deck and remove the 2other cards
                CraftAndAddtodeck(Card1, Card2, newcard);
                numberOfCardInCraft = 1;
                
                Card1 = newcard;
                return;


            }else{

                foreach (Transform slots in DM.slotsTransform)
            {
                CardSlot Slot = slots.gameObject.GetComponent<CardSlot>();
                if(Slot.IsSlotActive == false && Card1 != null){
                    Slot.IsSlotActive = true;
                    Card1.transform.position = Slot.transform.position;
                    Card1 = null;
                }
            }
            foreach (Transform slots in DM.slotsTransform)
            {
                CardSlot Slot = slots.gameObject.GetComponent<CardSlot>();
                if(Slot.IsSlotActive == false && Card2 != null){
                    Slot.IsSlotActive = true;
                    Card2.transform.position = Slot.transform.position;
                    Card2 = null;
                }
            }
            }



            
        }
        }

    }


    private void CraftAndAddtodeck(GameObject Card1, GameObject Card2, GameObject NewCardCrafted){
        cinfo1 = Card1.GetComponent<CardInfoDisplay>();
        cinfo2 = Card2.GetComponent<CardInfoDisplay>();
        cinfoNewCard = NewCardCrafted.GetComponent<CardInfoDisplay>();
        var x1 = DM.PlayerHand.FindIndex(x => x.ID == cinfo1.CardID);
        DM.PlayerHand.Remove(DM.PlayerHand[x1]);
        var x2 = DM.PlayerHand.FindIndex(x => x.ID == cinfo2.CardID);
        DM.PlayerHand.Remove(DM.PlayerHand[x2]);
        DM.PlayerHand.Add(DM.CardFullList[cinfoNewCard.CardID]);
        DM.DrawnCard--;
        StartCoroutine(DM.HandleCombatUI());
        Destroy(Card1,0.06f);
        Destroy(Card2,0.06f);
    }



}
