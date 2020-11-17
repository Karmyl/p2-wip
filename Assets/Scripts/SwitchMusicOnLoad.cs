using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicOnLoad : MonoBehaviour
{
    public AudioClip newTrack;
    private AudioManager audiomanager;
    // Start is called before the first frame update
    void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();

        if (newTrack != null)
            audiomanager.ChangeBackgroundMusic(newTrack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
