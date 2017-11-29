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

    public void clicked()
    {
        mainSource.clip = KeyNote;
        mainSource.Play();

        int i = KeyboardManager.instance.index;
        KeyboardManager.instance.myClip[i] = KeyNote;

        KeyboardManager.instance.index++;

        KeyboardManager.instance.AddKeyNote(Note);
    }

    
}
