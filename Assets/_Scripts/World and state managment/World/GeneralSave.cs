using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GeneralSave", menuName = "SaveSO/GeneralSave")]
public class GeneralSave : ScriptableObject
{
    public Vector2 CurrentFillBar;
    public int iteration;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
         
    }
      
    private void OnDisable() {
        iteration = 1;
    }
}
