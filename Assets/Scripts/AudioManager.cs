using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx; // sound efects
    public AudioSource[] bgm; // background music

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Play sound effect
    public void PlaySFX(int soundToPlay){
        if(soundToPlay < sfx.Length){
            sfx[soundToPlay].Play();
        }
    }

    // Play background sound
    public void PlayBGS(int musicToPlay){
        if(musicToPlay >= bgm.Length){
            StopMusic();
        }else{
            if(!bgm[musicToPlay].isPlaying){
                StopMusic();
                bgm[musicToPlay].Play();
            }
        }
    }

    // Stop Music
    public void StopMusic(){
        for(int i=0; i<bgm.Length; i++){
            bgm[i].Stop();
        }
    }
}
