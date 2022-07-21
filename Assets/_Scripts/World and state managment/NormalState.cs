using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NormalState : BaseState
{
    public NormalState(WorldManager currentContext, Factory factory)
     : base (currentContext, factory){} 

public override void EnterState(){
    Debug.Log("We've Enter Normal State");
    SceneManager.LoadScene("Main World");
    _ctx.bossbar.gameObject.SetActive(true);
    _ctx._events.UI.gameObject.SetActive(true);
    _ctx.LeaveStartMenu = false;
    
    

    
    
}
public override void UpdateState(){
    CheckSwitchState();
}
public override void ExitState(){
    Debug.Log("Normal State Left");
    _ctx._events.BackToSeaButton();
}
public override void CheckSwitchState(){
    //if button is pressed go to combat state
    if(_ctx.attackButtonPressed){
        SwitchState(_factory.Combat());
    }
    if(_ctx.MenuButtonisPressed){
        Debug.Log("switching from normal to mainmenu");
        SwitchState(_factory.MainMenu());
    }
    if(_ctx.IslandIsWanted){
        Debug.Log("switching from normal to island");
        SwitchState(_factory.Island());
    }
    if(_ctx.PortIsWanted){
        Debug.Log("switching from normal to port");
        SwitchState(_factory.Port());
    }
    if(_ctx.BossIsWanted){
        Debug.Log("Switching to boss state");
        SwitchState(_factory.FinalBoss());
    }
}
public override void InitializeSubState(){}

}
