using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    public static KeyboardManager instance;         //Singleton Design

    [HideInInspector]
    public AudioClip[] myClip;                      //Final record clip

    [HideInInspector]
    public int index = 0;                           //Keeping track of added clips in the recordClip

    AudioSource mainSource;                         //Main Audio source

    public Transform recordCanvas;                  //Notes area or the record area


    //Used for positioning the new Added keys in record area and for playstick
    private int x_position = -420;
    public int gap = 50;

    bool inside_canvas = true;

    int totalKeys = 0;                              //Total Keys added to the record clip

    public GameObject playBack_stick;               //Used to visualise during play
    private Vector3 playStick_initial_position;     //Re initializing the position of playstick

    private IEnumerator coroutine;                  //Main coroutine to play the clip

    System.DateTime timeStarted;                    //Used to keep track of play time

    public int PlayDuration = 5;                    //Change in inspector (in seconds)

    private void Awake()
    {
        instance = this;
        myClip = new AudioClip[100];
        index = 0;
    }

    void Start()
    {
        //Initializing the main Audio source, coroutine to play clip, and posiiton of playStick

        mainSource = GameObject.Find("MainAudio").GetComponent<AudioSource>();
        coroutine = PlayWholeRecord();
        playStick_initial_position = playBack_stick.transform.localPosition;
    }


    //Adding a Keynote in the record Area
    public void AddKeyNote(GameObject Note)
    {
        if (inside_canvas)      //Keynotes are added only if it can fit in the recordArea
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


    //When Play button is clicked
    public void playClip()
    {
        timeStarted = System.DateTime.Now;
       
        //Debug.Log(index);
        playBack_stick.SetActive(true);

        StopCoroutine(coroutine);

        coroutine = null;
        coroutine = PlayWholeRecord();

        StartCoroutine(coroutine);
    }


    //Coroutine to play the recoreded clip
    IEnumerator PlayWholeRecord()
    {
        playBack_stick.transform.localPosition = playStick_initial_position;
        for (int i = 0; i < totalKeys; i++)
        {
            AudioClip c = myClip[i];
            mainSource.clip = c;
            mainSource.Play();
            yield return new WaitForSeconds(c.length);

            var playTime = System.DateTime.Now - timeStarted;            //If play time exceeds PlayDuration seconds stop playing
            if (playTime.Seconds > PlayDuration)
            {
                break;
            }

            Vector3 temp = playBack_stick.transform.localPosition;
            playBack_stick.transform.localPosition = new Vector3(temp.x + gap, temp.y, temp.z);
        }

        playBack_stick.SetActive(false);
        playBack_stick.transform.localPosition = playStick_initial_position;
        coroutine = null;
        coroutine = PlayWholeRecord();
    }


    //When Stop button is clicked
    public void stopClip()
    {
        StopCoroutine(coroutine);
    }


    //When Erase button is clicked
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
