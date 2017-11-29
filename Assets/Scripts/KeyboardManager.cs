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

    public Transform recordCanvas;

    private int x_position = -420;
    public int gap = 50;

    bool inside_canvas = true;

    int totalKeys = 0;

    public GameObject playBack_stick;
    private Vector3 playStick_initial_position;

    private IEnumerator coroutine;

    private void Awake()
    {
        instance = this;
        myClip = new AudioClip[100];
        index = 0;
    }

    void Start()
    {
        mainSource = GameObject.Find("MainAudio").GetComponent<AudioSource>();
        coroutine = PlayWholeRecord();
        playStick_initial_position = playBack_stick.transform.localPosition;
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

            totalKeys++;

            if (x_position > 420)
                inside_canvas = false;
        }
    }

    public void playClip()
    {
        Debug.Log(index);
        playBack_stick.SetActive(true);

        StartCoroutine(coroutine);
    }

    IEnumerator PlayWholeRecord()
    {

        for (int i = 0; i < totalKeys; i++)
        {
            AudioClip c = myClip[i];
            mainSource.clip = c;
            mainSource.Play();
            yield return new WaitForSeconds(c.length);

            Vector3 temp = playBack_stick.transform.localPosition;
            playBack_stick.transform.localPosition = new Vector3(temp.x + gap, temp.y, temp.z);
            //mainSource.Play();
        }

        playBack_stick.SetActive(false);
        playBack_stick.transform.localPosition = playStick_initial_position;
        coroutine = null;
        coroutine = PlayWholeRecord();
        //mainSource.Play();
        //yield return new WaitForSeconds(seconds);
        //Debug.Log("")
    }

    public void stopClip()
    {
        StopCoroutine(coroutine);
    }

    public void eraseRecord()
    {
        //clearing the coroutine and playstick
        playBack_stick.transform.localPosition = playStick_initial_position;
        playBack_stick.SetActive(false);
        coroutine = null;
        coroutine = PlayWholeRecord();

        //clearing the audio clip array
        for(int i = 0; i < totalKeys; i++)
        {
            myClip[i] = null;
        }

        //re initializing the variables
        index = 0;
        totalKeys = 0;
        x_position = -420;
        inside_canvas = true;

        //Destroying the key notes in record area
        foreach(Transform child in recordCanvas)
        {
            GameObject obj = child.gameObject;
            if(obj.tag == "KeyNote")
            {
                Destroy(obj);
            }
        }
    }

}
