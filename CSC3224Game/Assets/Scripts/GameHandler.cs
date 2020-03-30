using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    public AudioSource music;
    public float songBpm;
    public float secondBpm;
    public float bpmChangesFromBeat;
    public float speedMultiplier;
    public Text scoreText;
    public Text comboText;
    public Text judgeText;
    public Text reminderText;

    public static GameHandler instance;
    public bool start;

    public float secPerBeat;
    public float songPosition = 0;
    public float lastBeatPosition = 0;
    public float songPositionInBeats = 0;
    private float dspSongTime;

    private int score = 0;
    private int combo = 0;

    private float lastChanged;

    // Use this for initialization
    void Start()
    {
        instance = this;
        scoreText.text = "Score : 0";
        comboText.text = "";
        judgeText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (Input.anyKeyDown)
            {
                foreach (Transform child in reminderText.transform)
                {
                    Destroy(child.gameObject);
                }
                
                start = true;
                secPerBeat = 60f / songBpm;

                dspSongTime = (float)AudioSettings.dspTime;
                music.Play();
            }
        }
        else
        {
            songPosition = (float)(AudioSettings.dspTime - dspSongTime);
            if (songPosition - lastBeatPosition >= (secPerBeat / 4))
            {
                songPositionInBeats += 0.25f;
                lastBeatPosition = songPosition - ((songPosition - lastBeatPosition) - (secPerBeat / 4));
            }

            if (songPositionInBeats == bpmChangesFromBeat && songBpm != secondBpm)
            {
                Debug.Log("BPM changed to " + secondBpm);
                songBpm = secondBpm;
                secPerBeat = 60f / songBpm;
            }

            if (Time.time - lastChanged >= 0.8)
                judgeText.text = "";

            if (songPositionInBeats >= 99.25)
            {
                music.Stop();
            }
        }
    }

    public void Hit(float disDiff)
    {
        float velocity = speedMultiplier * (songBpm / 60f);
        float timeDiff = (disDiff / velocity) * 1000;

        Debug.Log("Hit with offset " + timeDiff + "ms");

        if(timeDiff <= 30)
            Perfect();
        else if (timeDiff <= 90)
            Great();
        else if (timeDiff <= 130)
            Good();
        else
            Bad();

        scoreText.text = "Score : " + score;

        if (combo >= 5)
            comboText.text = "" + combo;
        else
            comboText.text = "";

        lastChanged = Time.time;
    }

    public void Perfect()
    {
        judgeText.text = "✦Perfect✦";
        judgeText.color = new Color(218 / 255f, 189 / 255f, 81 / 255f);
        score += 1000;
        combo++;

        Debug.Log("Perfect");
    }

    public void Great()
    {
        judgeText.text = "✣Great✣";
        judgeText.color = new Color(164 / 255f, 81 / 255f, 174 / 255f);
        score += 600;
        combo++;

        Debug.Log("Great");
    }

    public void Good()
    {
        judgeText.text = "✢Good✢";
        judgeText.color = new Color(123 / 255f, 192 / 255f, 119 / 255f);
        score += 300;
        combo++;

        Debug.Log("Good");
    }

    public void Bad()
    {
        judgeText.text = "-Bad-";
        judgeText.color = new Color(52 / 255f, 82 / 255f, 180 / 255f);
        combo = 0;

        Debug.Log("Bad");
    }

    public void Miss()
    { 
        judgeText.text = "-Miss-";
        judgeText.color = new Color(193 / 255f, 74 / 255f, 77 / 255f);
        combo = 0;

        Debug.Log("Miss");

        comboText.text = "";

        lastChanged = Time.time;
    }
}
