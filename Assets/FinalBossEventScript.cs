using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEventScript : MonoBehaviour
{
public Ennemy_SO FinalBoss;


private void Awake() {
    GameObject deck = GameObject.Find("DeckManager");
    DeckManager DM = deck.GetComponent<DeckManager>();

    DM.CurrentEnemy = FinalBoss;
    



}
}
