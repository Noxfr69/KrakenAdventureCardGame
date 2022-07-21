using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New WorldPosition", menuName = "SaveSO/WorldPosition")]
public class Save_WorldPosition : ScriptableObject
{
    public List<Sprite> SavedSprite;

    public List<int> IDs;

    public Vector3 boatPosition;

    private void OnEnable()
     {
         hideFlags = HideFlags.DontUnloadUnusedAsset;
         
     }
      
    private void OnApplicationQuit() {
        boatPosition = new Vector3(0,0,0);
       
    }
}
