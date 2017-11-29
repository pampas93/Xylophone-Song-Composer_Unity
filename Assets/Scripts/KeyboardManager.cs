using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    public static KeyboardManager instance;

    [HideInInspector]
    public AudioClip[] myClip;

    [HideInInspector]
    public int index = 0;

    AudioSource mainSource;

    private void Awake()
    {
        instance = this;
        myClip = new AudioClip[100];
        index = 0;
    }

    void Start()
    {
        mainSource = GameObject.Find("MainAudio").GetComponent<AudioSource>();

    }

   
}
