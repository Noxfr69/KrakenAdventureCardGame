using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New LootTable", menuName = "LootTable")]
public class LootTables : ScriptableObject
{
    public List<int> table;
}
