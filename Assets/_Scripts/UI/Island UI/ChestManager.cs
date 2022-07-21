using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    private void Awake() {
        this.gameObject.SetActive(false);
    }
    public void OnChestOpen(){
        this.gameObject.SetActive(true);
    }
    public void OnCloseChest(){
        this.gameObject.SetActive(false);
    }


}
