using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostState : BaseState
{
    public LostState(WorldManager currentContext, Factory factory)
     : base (currentContext, factory){} 
         
public override void EnterState(){
    Debug.Log("Enter LostState");
    SceneManager.LoadScene("LostGameScene");

}
public override void UpdateState(){
    CheckSwitchState();
}

public override void ExitState(){
    Debug.Log("Lost State Left");
    _ctx.bossbar.gameObject.SetActive(true);
    _ctx.OnStateSwitch();
}
public override void CheckSwitchState(){
    if(_ctx.MenuButtonisPressed){
        SwitchState(_factory.MainMenu());
    }

}

public override void InitializeSubState(){}
}
