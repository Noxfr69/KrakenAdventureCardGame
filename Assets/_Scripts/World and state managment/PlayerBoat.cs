using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBoat : MonoBehaviour
{
    Input inputController;
    private Vector2 newPos;
    GameObject bossbar;
    bool clicked = false;
    public Sprite boat;
    public Sprite boatleft;
    public SpriteRenderer SR;


    private void OnEnable() { inputController.Enable(); }
    private void OnDisable() { inputController.Disable(); }


    private void Awake() {
        clicked = false;
        Debug.Log(SavedDATA.BoatPosition);
        inputController = new Input();
        //get position from saveddata
        transform.position = SavedDATA.BoatPosition;
    }


    void Update()
    {
        
        if(inputController.Normal.Mouse_Left_Hold.inProgress){
            clicked = true;
            if(EventSystem.current.IsPointerOverGameObject())return;
            newPos =  Camera.main.ScreenToWorldPoint(InputManager.WorldPosition);
            Vector2 TP = new Vector2(transform.position.x, transform.position.y);
            var Dir = TP - newPos;
             var angle = Vector2.SignedAngle(Vector2.up, Dir);
            if(angle < 0){
                SR.sprite = boatleft;
            }else SR.sprite = boat;
        }   
        if(clicked == true){
        transform.position =  Vector2.MoveTowards(transform.position, newPos, 3*Time.deltaTime);


        SavedDATA.BoatPosition = transform.position;
        }

    }



    //implement the bar getting fill by distance 

}

