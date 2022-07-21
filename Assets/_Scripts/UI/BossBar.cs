using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBar : MonoBehaviour
{
    RectTransform FillBar;
    private float MaxFill = 450;
    public GeneralSave save;
    public static event Action BossBarFilledUP;


    private void Awake() {
        //WorldManager.changedScene += addFill;
        FillBar = GetComponent<RectTransform>();
        if(save.iteration == 1){
            FillBar.sizeDelta = new Vector2(0,39.9f);
            
        }
    }
    void Start()
    {

    }

    public void addFill(){
        Debug.Log("we are filling the bar");
        FillBar = GetComponent<RectTransform>();
        FillBar.sizeDelta = new Vector2(FillBar.sizeDelta.x+40f,39.9f);
        save.iteration++; 
        if(FillBar.sizeDelta.x >= MaxFill){
            BossBarFilledUP?.Invoke();
        }
    }

    public void ResetBar(){
        FillBar = GetComponent<RectTransform>();
        FillBar.sizeDelta = new Vector2(0,39.9f);
    }

    
    void Update()
    {
        
    }

    void OnDisable(){
        //WorldManager.changedScene -= addFill;
    }
}
