﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    public static KeyboardManager instance;

    [HideInInspector]
    public AudioClip[] myClip;

    [HideInInspector]
    public int index = 0;

    AudioSource mainSource;

    public Transform recordCanvas;

    private int x_position = -420;

    public int gap = 50;

    bool inside_canvas = true;

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

    public void AddKeyNote(GameObject Note)
    {
        if (inside_canvas)
        {
            GameObject newNote = Instantiate(Note, recordCanvas);
            Vector3 temp = newNote.transform.localPosition;
            newNote.transform.localPosition = new Vector3(x_position, temp.y, temp.z);

            //Debug.Log(newNote.transform.localPosition);
            x_position = x_position + gap;

            if (x_position > 420)
                inside_canvas = false;
        }
    }

}
