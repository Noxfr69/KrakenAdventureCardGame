using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Threading.Tasks;

public class WorldManager : MonoBehaviour
{
#region Variable
  //Controler 
 public InputManager _input;
  // State Variable
  public BaseState _currenState;
  public Factory _states;
  public DeckManager deckManager;

  //UI
  public Persistant_UI_Events _events;
  public GameObject bossbar;
  public Vector3 bosbarpos;
  public GameObject bossBarfill;
  public GameObject backToSeaButton;
  public GameObject Player_Stats;
  //UI bools and checks
  public bool attackButtonPressed;
  public bool WorldButtonPressed;
  public bool MenuButtonisPressed;
  public bool IslandIsWanted;
  public bool PortIsWanted;
  public bool BossIsWanted;
  public bool LostIsWanted;
  public bool LeaveStartMenu = false;
  //save tools 
  public GeneralSave generalSave;

#endregion
  
  
  void Awake()
    {
      Debug.Log(SavedDATA.GameJustLaunched);
      if(SavedDATA.GameJustLaunched == 0){
    // setup the factory with the worldManager as a parameter    
        _states = new Factory(this);
        //Set the Basestate as the state launch from the Normal() factory call (So normal State)
        _currenState = _states.MainMenu();
        //Launch enterState on the current State
        _currenState.EnterState();
        SavedDATA.GameJustLaunched = 1;
        //Set the iteration use for reset the boss bar
        generalSave.iteration = 1;
        bosbarpos = bossbar.gameObject.transform.position;
      }




        ///// subscribing to all the events we need 
        _events.OnCLickedAttack += attackButtonPress;
        _events.OnCLickedWorld += worldButtonPress;
        _events.OnClickedMenu += MenuButtonPress;
        WorldEvent.OncolliderAttack += attackButtonPress;
        WorldEvent.OncolliderPort += PortAccess;
        WorldEvent.OncolliderIsland += IslandAccess;
        BossBar.BossBarFilledUP += FinalBossAccess;
        CombatWinScreen.FightIsOver += worldButtonPress;
        LostManager.GameContinue += MenuButtonPress;
        DeckManager.PlayerLost += LostAccess;
    }


  void Start()
    {
        
    }


  void Update()
    {
        _currenState.UpdateState();
    }

  public void OnStateSwitch(){
    var bb = bossBarfill.GetComponent<BossBar>();
    bb.addFill();
  
  }
#region Events subs and unsub

    private void attackButtonPress(){
      StartCoroutine(attackButtonPressCo());

    }
    private void worldButtonPress(){
      StartCoroutine(WorldButtonPressedCo());
    }
    private void MenuButtonPress(){
      StartCoroutine(MenuButtonPressCo());

    }
    private void IslandAccess(){
      StartCoroutine(IslandAccessCo());

    }
    private void PortAccess(){
      StartCoroutine(PortAccessCo());

    }
    private void FinalBossAccess(){
      StartCoroutine(FinalBossAccessCo());

    }
    private void LostAccess(){
      StartCoroutine(LostAccessCo());

    }


  void OnDestroy() {
    // unsubscribe from our events 
    _events.OnCLickedAttack -= attackButtonPress;
    _events.OnCLickedWorld -= worldButtonPress;
    _events.OnClickedMenu -= MenuButtonPress;
    WorldEvent.OncolliderAttack -= attackButtonPress;
    WorldEvent.OncolliderPort -= PortAccess;
    WorldEvent.OncolliderIsland -= IslandAccess;
    BossBar.BossBarFilledUP -= FinalBossAccess;
    CombatWinScreen.FightIsOver -= worldButtonPress;
    LostManager.GameContinue -= MenuButtonPress;
    DeckManager.PlayerLost -= LostAccess;
  }
#endregion



public IEnumerator attackButtonPressCo(){
  attackButtonPressed = true;
  yield return new WaitForSecondsRealtime(0.05f);
  attackButtonPressed = false;
}

public  IEnumerator WorldButtonPressedCo(){
  WorldButtonPressed = true;
  yield return new WaitForSecondsRealtime(0.05f);
  WorldButtonPressed = false;

}

public IEnumerator MenuButtonPressCo(){
  MenuButtonisPressed = true;
  yield return new WaitForSecondsRealtime(0.05f);
  MenuButtonisPressed = false;
}

public IEnumerator IslandAccessCo(){
  IslandIsWanted = true;
  yield return new WaitForSecondsRealtime(0.05f);
  IslandIsWanted = false;
}

public IEnumerator PortAccessCo(){
  PortIsWanted = true;
  yield return new WaitForSecondsRealtime(0.05f);
  PortIsWanted = false;
}

public IEnumerator FinalBossAccessCo(){
  BossIsWanted = true;
  yield return new WaitForSecondsRealtime(0.05f);
  BossIsWanted = false;
}

public IEnumerator LostAccessCo(){
  LostIsWanted = true;
  yield return new WaitForSecondsRealtime(0.05f);
  LostIsWanted = false;
}
}
