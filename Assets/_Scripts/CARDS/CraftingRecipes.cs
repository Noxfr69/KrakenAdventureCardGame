using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public struct ID_matching{
    public int ID1;
    public int ID2;
}


[CreateAssetMenu(fileName = "Crafting Recipe", menuName = "Card/Crafting Recipe")]
public class CraftingRecipes : ScriptableObject
{
    public List<ID_matching> craftingRecipe = new List<ID_matching>();
    public List<int> craftingResults = new List<int>();

}
