using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossState : BaseState
{
    public FinalBossState(WorldManager currentContext, Factory factory)
     : base (currentContext, factory){} 
         
public override void EnterState(){
    Debug.Log("Enter FinalBossState");
    SceneManager.LoadScene("FinalBoss");
    _ctx.deckManager.StartCoroutine(_ctx.deckManager.getSlots());
    _ctx.bossbar.gameObject.SetActive(false);
    _ctx.backToSeaButton.gameObject.SetActive(false);

}
public override void UpdateState(){
    CheckSwitchState();
}

public override void ExitState(){
    Debug.Log("FinalBossState Left");
    _ctx.bossbar.gameObject.SetActive(true);
}
public override void CheckSwitchState(){
    if(_ctx.MenuButtonisPressed){
        SwitchState(_factory.MainMenu());
    }
        if(_ctx.LostIsWanted){
        SwitchState(_factory.Lost());
    }

}

public override void InitializeSubState(){}
}
