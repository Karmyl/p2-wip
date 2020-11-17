using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundFX : MonoBehaviour
{
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSoundEffect()
    {
        //DontDestroyOnLoad(gameObject);
        sound.Play();
    }
}
