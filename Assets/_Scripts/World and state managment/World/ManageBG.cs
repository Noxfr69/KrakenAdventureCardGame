using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBG : MonoBehaviour
{
    private void Awake() {
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
        GameObject Deckmanager = GameObject.Find("DeckManager");
        var DM = Deckmanager.GetComponent<DeckManager>();

        SR.sprite = DM.CurrentEventSprite;
    }
}
