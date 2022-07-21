using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Events_SO : ScriptableObject
{
    public Sprite Event_mapstate;
    public Sprite Event_BackGround;
    public LootTables Event_lootTable;
    public Ennemy_SO Event_ennemy;
    public int ID;
    [Header("1- Port, 2- Island, 3- Combat")]
    public int TypeID;
    



}
