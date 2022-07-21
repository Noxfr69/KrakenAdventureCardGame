using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/New Card")]
public class Card_SO : ScriptableObject
{
    public int ID;
    public int Buy_Price;
    public int Sell_Price;
    public new string name;
    public string Description; 
    public Sprite Image;
    public int Damage;
    public int Armor;
    public int HealthRegen;
    [Header("1- Attack, 2- Armor, 3- heal")]
    public int typeID;
    [Header("1- Common, 2- rare, 3- Epic, 4- Legendary")]
    public int Rarity;


}
