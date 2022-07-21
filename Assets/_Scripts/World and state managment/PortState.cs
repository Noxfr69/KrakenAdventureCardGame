using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PortState : BaseState
{
     public PortState(WorldManager currentContext, Factory factory)
     : base (currentContext, factory){} 
public override void EnterState(){
    Debug.Log("We've Enter Port State");
    SceneManager.LoadScene("Port");
}
public override void UpdateState(){
    CheckSwitchState();
}
public override void ExitState(){
    Debug.Log("Port State Left");
    _ctx.OnStateSwitch();
}
public override void CheckSwitchState(){
    if(_ctx.MenuButtonisPressed){
        SwitchState(_factory.MainMenu());
    }
    if(_ctx.WorldButtonPressed){
        SwitchState(_factory.Normal());
    }
}
public override void InitializeSubState(){}
 
}
