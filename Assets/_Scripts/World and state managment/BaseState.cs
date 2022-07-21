
public abstract class BaseState 
{
    protected WorldManager _ctx;
    protected Factory _factory;
    public BaseState(WorldManager currentContext, Factory factory){
        _ctx = currentContext;
        _factory = factory;
    }

public abstract void EnterState();
public abstract void UpdateState();
public abstract void ExitState();
public abstract void CheckSwitchState();
public abstract void InitializeSubState();

void UpdateStates(){}
protected void SwitchState(BaseState newState){
    //first leave the current state
    ExitState();
    //then enter the new one that has been passed as data
    newState.EnterState();
    //Let the wolrdManager Know about the change of state 
    _ctx._currenState = newState;
    //Add a bit to the boss bar
    

}
protected void SetSuperState(){}
protected void SetSubState(){}


}
