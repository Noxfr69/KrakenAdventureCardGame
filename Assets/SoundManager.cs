using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer MusicMaster;
    public AudioMixer SFXMaster;




    public void SetLevelMusic(float sliderMMusicValue){
        MusicMaster.SetFloat("Music_Master", Mathf.Log10(sliderMMusicValue)*20 );
    }

    
    public void SetLevelSFX(float sliderSFXValue){
        SFXMaster.SetFloat("SFX_Master", Mathf.Log10(sliderSFXValue)*20 );
    }



}
