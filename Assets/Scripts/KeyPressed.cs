using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressed : MonoBehaviour {

    public AudioClip KeyNote;

    AudioSource mainSource;

    public GameObject Note;

    private void Start()
    {
        mainSource = GameObject.Find("MainAudio").GetComponent<AudioSource>();
    }

    //When a Key is clicked
    public void clicked()
    {
        //Play the corresposing clip to the key
        mainSource.clip = KeyNote;
        mainSource.Play();

        //Adding this keyNote into the record clip
        int i = KeyboardManager.instance.index;
        KeyboardManager.instance.myClip[i] = KeyNote;
        KeyboardManager.instance.index++;

        //Adding the key into record area
        KeyboardManager.instance.AddKeyNote(Note);
    }

    
}
