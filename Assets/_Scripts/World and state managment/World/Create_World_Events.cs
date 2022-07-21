using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_World_Events : MonoBehaviour
{

    //public List<GameObject> eventPosition;
    //public List<Events_SO> events_SOs;
    //public Save_WorldPosition Save_WorldPosition;
    //private int x = 0;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    if(SavedDATA.GameJustLaunched == 1){
    //        Save_WorldPosition.SavedSprite.Clear();
    //        Save_WorldPosition.IDs.Clear();
    //        foreach (GameObject go in eventPosition)
    //        {
    //            int x = Random.Range(0, events_SOs.Capacity);
    //            SpriteRenderer sR = go.GetComponent<SpriteRenderer>();
    //            sR.sprite = events_SOs[x].normalstate;
    //            Save_WorldPosition.SavedSprite.Add(sR.sprite);
//
    //            WorldEvent WE = go.GetComponent<WorldEvent>();
    //            WE.ID = events_SOs[x].ID;
    //            Save_WorldPosition.IDs.Add(WE.ID);
    //            
    //            
    //        }
    //    SavedDATA.GameJustLaunched =2;
    //    }else{
    //        foreach(GameObject go in eventPosition)
    //        {
    //            SpriteRenderer sR = go.GetComponent<SpriteRenderer>();
    //            sR.sprite = Save_WorldPosition.SavedSprite[x];
    //            WorldEvent WE = go.GetComponent<WorldEvent>();
    //            WE.ID = Save_WorldPosition.IDs[x];
    //            x++;
    //        }
    //    }
    //}
//
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
}//
