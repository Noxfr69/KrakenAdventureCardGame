using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Boat_Stats : MonoBehaviour
{
    public int BaseAttackStat = 0;
    public int BaseArmorStat = 0;
    public int BaseHealthStat = 100;

    public int CurrentBoatAttack;
    public int CurrentBoatArmor;
    public int CurrentBoatHealth;


     
    public int Ennemy_BaseAttackStat = 20;
    public int Ennemy_BaseArmorStat = 10;
    public int Ennemy_BaseHealthStat = 100;

    public int Ennemy_CurrentBoatAttack;
    public int Ennemy_CurrentBoatArmor;
    public int Ennemy_CurrentBoatHealth;

    private int count = 0;
    public int Encounter = 0;


    public void FirstStatSetUP(){
        if(count==0){
            CurrentBoatAttack = BaseAttackStat;
            CurrentBoatArmor = BaseArmorStat;
            CurrentBoatHealth = BaseHealthStat;


            Ennemy_CurrentBoatAttack = Ennemy_BaseAttackStat;
            Ennemy_CurrentBoatArmor = Ennemy_BaseArmorStat;
            Ennemy_CurrentBoatHealth = Ennemy_BaseHealthStat;


            count++;
        }else return;
   }

    public void StatsRefresh(){}

}
