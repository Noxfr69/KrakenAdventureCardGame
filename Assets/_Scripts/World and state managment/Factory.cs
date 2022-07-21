public class Factory 
{
WorldManager _context;

public Factory(WorldManager currentContext){
    _context = currentContext;
}

public BaseState Combat(){
    return new CombatState(_context, this);
}
public BaseState MainMenu(){
    return new MainMenuState(_context, this);
}
public BaseState Island(){
    return new IslandState(_context, this);
}
public BaseState Port(){
    return new PortState(_context, this);
}
public BaseState Normal(){
    return new NormalState(_context, this);
}
public BaseState FinalBoss(){
    return new FinalBossState(_context, this);
}
public BaseState Lost(){
    return new LostState(_context, this);
}
}
