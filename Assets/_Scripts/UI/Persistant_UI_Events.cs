using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistant_UI_Events : MonoBehaviour
{

    public event Action OnCLickedAttack;
    public event Action OnCLickedWorld;
    public event Action OnClickedMenu;
    public Canvas UI;
    Canvas ESCMenu;
    GameObject backtoseabutton;

    public void OnclickedAttackButton(){
        OnCLickedAttack?.Invoke();
    }

    public void OnclickedWorldButton(){
        OnCLickedWorld?.Invoke();
        backtoseabutton.gameObject.SetActive(false);

    }

    public void OnClickedMenuButton(){
        OnClickedMenu?.Invoke();
    }

    public void OnclickQuit(){
        Application.Quit();
    }

    private void Awake() {
        
        UI = GetComponent<Canvas>();
        GameObject escMenu = GameObject.Find("ESCMenu");
        ESCMenu = escMenu.GetComponent<Canvas>();
        InputManager.ESCpressed += Set_ESCMenu;
        ESCMenu.gameObject.SetActive(false);
        backtoseabutton = GameObject.Find("worldButton");
        backtoseabutton.gameObject.SetActive(false);
    }



    public void Set_ESCMenu(){
        Debug.Log("esc pressed");
        if(UI.gameObject.activeInHierarchy){
            if(ESCMenu.gameObject.activeInHierarchy){
                ESCMenu.gameObject.SetActive(false);
            }else ESCMenu.gameObject.SetActive(true);
        }
    }

    public void BackToSeaButton(){
        backtoseabutton.gameObject.SetActive(true);

    }

    private void OnDestroy() {
        InputManager.ESCpressed -= Set_ESCMenu;
    }
}
