using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWin : MonoBehaviour
{

    public void OnClickedMenuButton(){
        var x = GameObject.Find("Persistant_UI");
        var y = x.GetComponent<Persistant_UI_Events>();

        y.OnClickedMenuButton();
    }

    public void OnclickQuit(){
        Application.Quit();
    }
}
