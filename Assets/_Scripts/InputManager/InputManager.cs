using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Input inputController;
    public bool MouseLeftHold = false;
    public static event Action ESCpressed;
    public static Vector3 WorldPosition;

    void Awake() {
        inputController = new Input();
    }

    private void OnEnable() { inputController.Enable(); }
    private void OnDisable() { inputController.Disable(); }


    void Start()
    {
        
    }

    void Update()
    {
        if(inputController.Normal.Mouse_Left_Hold.inProgress){
            WorldPosition = inputController.Normal.MousePosition.ReadValue<Vector2>();
            MouseLeftHold = true;
        }else MouseLeftHold = false;

        if(inputController.Normal.Esc.WasPerformedThisFrame()){
            ESCpressed?.Invoke();
        }

    }
}
