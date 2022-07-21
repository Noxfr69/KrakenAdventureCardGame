using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop_Card : MonoBehaviour
{
    Input inputController;
    [SerializeField]
    private float mouseDragspeed = 0.1f;
    private Vector3 velocity = Vector3.zero;
    DeckManager DM;
    GameObject deckManager;
    GameObject closestSlot;
    public GameObject Card = null;
    Combat_UI cUI;

    void Awake() {
        inputController = new Input(); 
        deckManager = GameObject.Find("DeckManager");
        DM = deckManager.GetComponent<DeckManager>();
        cUI = GetComponent<Combat_UI>();

        }
    private void OnEnable() { inputController.Enable(); inputController.Normal.Click.started += MouseDrag;}
    private void OnDisable() { inputController.Disable(); inputController.Normal.Click.started -= MouseDrag;}


    private void MouseDrag(InputAction.CallbackContext context) 
    {
        if(DM.IsGameOver == true){return;}
        Debug.Log("dragging");
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D[] hit2Ds = Physics2D.GetRayIntersectionAll(ray);
        foreach(RaycastHit2D hit2D in hit2Ds){
            if(hit2D.collider.gameObject.tag == "Card")
            {
                Debug.Log(hit2D.collider);
                if(!DM.IsGamePaused){
                    //play sound up
                    cUI.Combat_SFX.PlayOneShot(cUI.card_take);
                StartCoroutine(DragUpdate(hit2D.collider.gameObject));
                //Set the slot to inactive 
                SetSlotInactive(hit2D.collider.gameObject);
                return;
                }
                
            }
        }
        
    }

    private IEnumerator DragUpdate(GameObject clickedObject){
        while(inputController.Normal.Click.ReadValue<float>() != 0){
            //make it appear first #TODO
            Vector3 target = new Vector3(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y, 2) ;
            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, target, ref velocity, mouseDragspeed);
            yield return new WaitForFixedUpdate();
        }
        //Debug.Log("Stopped dragging");
        cUI.Combat_SFX.PlayOneShot(cUI.card_Put);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D[] hit2Ds = Physics2D.GetRayIntersectionAll(ray);
        foreach (RaycastHit2D hit2D in hit2Ds){
           Collider2D collider = hit2D.collider;
           if(collider.gameObject.tag == "Craft"){
               Debug.Log("we are in crafting zone");
               OnCardCraft oCC = deckManager.GetComponent<OnCardCraft>();
               if(Card == null | Card != clickedObject){
                   Card = clickedObject;
                   Debug.Log(clickedObject);
                   oCC.OnCraftTry(clickedObject);
                   yield return new WaitForFixedUpdate();
               }
               
           }
           if(collider.gameObject.tag == "PlayZone" ){
               Debug.Log("we are in Play zone");
               OnCardPlay oCP = deckManager.GetComponent<OnCardPlay>();
               Debug.Log(clickedObject);
               oCP.CardPlayed(clickedObject);  
               yield return new WaitForFixedUpdate();             
           }
           if(collider == null)yield return new WaitForFixedUpdate();

        }yield return new WaitForFixedUpdate();

    }

    public void SetSlotInactive(GameObject card){
        float shortesDistance = float.MaxValue;
        
        foreach (Transform slots in DM.slotsTransform)
        {
            float distance = Vector2.Distance(slots.position, card.transform.position);
            if(distance < shortesDistance){
                shortesDistance = distance;
                closestSlot = slots.gameObject;
            }    
        }
        CardSlot CS = closestSlot.GetComponent<CardSlot>();
        CS.IsSlotActive = false;

    }
}
