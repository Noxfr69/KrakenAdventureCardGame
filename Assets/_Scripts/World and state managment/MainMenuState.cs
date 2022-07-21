using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainMenuState : BaseState
{
     public MainMenuState(WorldManager currentContext, Factory factory)
     : base (currentContext, factory){} 
public override void EnterState(){
    Debug.Log("We've Enter MainMenu State");
    SceneManager.LoadScene("StartMenu");
}
public override void UpdateState(){
    CheckSwitchState();
}
public override void ExitState(){
    Debug.Log("MainMenu State Left");
}
public override void CheckSwitchState(){
    if(_ctx.LeaveStartMenu == true){
    SwitchState(_factory.Normal());
    }
}
public override void InitializeSubState(){}
 
}
