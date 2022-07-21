using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandState : BaseState
{
    public IslandState(WorldManager currentContext, Factory factory)
     : base (currentContext, factory){} 
         
public override void EnterState(){
    Debug.Log("Enter Island");
    SceneManager.LoadScene("Island");

}
public override void UpdateState(){
    CheckSwitchState();
}

public override void ExitState(){
    Debug.Log("Island State Left");
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
