using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CombatState : BaseState
{
    public CombatState(WorldManager currentContext, Factory factory)
     : base (currentContext, factory){} 


public override void EnterState(){
    Debug.Log("We've Enter Combat State");
    SceneManager.LoadScene("Combat");
    _ctx.deckManager.StartCoroutine(_ctx.deckManager.getSlots());
    _ctx.bossbar.gameObject.SetActive(false);
    _ctx.backToSeaButton.gameObject.SetActive(false);

    //Only work the first time of the run initialize the stats
    Player_Boat_Stats boat_Stats = _ctx.Player_Stats.GetComponent<Player_Boat_Stats>();
    boat_Stats.FirstStatSetUP();
}


public override void UpdateState(){
    CheckSwitchState();
}


public override void ExitState(){
    Debug.Log("Combat State Left");
    _ctx.bossbar.gameObject.SetActive(true);
    _ctx.OnStateSwitch();
}


public override void CheckSwitchState(){
    if(_ctx.WorldButtonPressed){
        SwitchState(_factory.Normal());
    }
    if(_ctx.MenuButtonisPressed){
        SwitchState(_factory.MainMenu());
    }
    if(_ctx.LostIsWanted){
        SwitchState(_factory.Lost());
    }
}


public override void InitializeSubState(){

    //handle combat trun logic
}
}
