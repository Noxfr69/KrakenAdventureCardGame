using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavedDATA : MonoBehaviour
{

public static int GameJustLaunched;
public static Vector2 BoatPosition = new Vector2(0,0);
public static int PlayerMoney;



void Awake() {
    PlayerPrefs.SetInt("GameJustLaunched", 0);
}
}
