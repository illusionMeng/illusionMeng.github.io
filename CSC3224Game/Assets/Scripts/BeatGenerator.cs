using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BeatStat
{
    private float locationInBeat;
    private string type;
    private bool isDouble;

    public BeatStat(float locationInBeat, string type, bool isDouble)
    {
        this.locationInBeat = locationInBeat;
        this.type = type;
        this.isDouble = isDouble;
    }

    public float getLocation() { return locationInBeat; }
    public string getType() { return type; }
    public bool getIsDouble() { return isDouble; }
};

public class BeatGenerator : MonoBehaviour
{
    public GameObject Beat;
    public GameObject DoubleBeat;

    public TextAsset file;

    private string input;

    private GameObject temp;

    private float currentBeat = -1;

    private List<BeatStat> beatContiner = new List<BeatStat>();
    private List<BeatStat> toBeDeleted = new List<BeatStat>();

    //  private float ;

    // Use this for initialization
    void Start()
    {
        input = file.text;
        string[] lines = input.Split('\n');
        for(int i = 0; i<lines.Length; i++)
        {
            string[] temp = lines[i].Split(' ');
            beatContiner.Add(new BeatStat(float.Parse(temp[0]), temp[1], bool.Parse(temp[2])));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.instance.start)
        {
            if (!(Mathf.Abs(currentBeat - GameHandler.instance.songPositionInBeats) <= 0.0001))
            {
                float velocity = GameHandler.instance.speedMultiplier * (GameHandler.instance.songBpm / 60f);
                float timeDiff = (GameHandler.instance.songPosition - GameHandler.instance.lastBeatPosition) - (GameHandler.instance.secPerBeat / 4);
                float offset = velocity * timeDiff;

                for (int i = 0; i < beatContiner.Count; i++)
                {
                    if (Mathf.Abs(beatContiner[i].getLocation() - GameHandler.instance.songPositionInBeats) <= 0.0001)
                    {
                        spawnBeat(offset, beatContiner[i].getType(), beatContiner[i].getIsDouble());
                        toBeDeleted.Add(beatContiner[i]);
                    }

                }

                for (int i = 0; i < toBeDeleted.Count; i++)
                    beatContiner.Remove(toBeDeleted[i]);


                toBeDeleted.Clear();

                currentBeat = GameHandler.instance.songPositionInBeats;
            }
        }
    }

    void spawnBeat(float offset, string type, bool isDouble)
    {

        float distance = 9.875f + offset;
        float rotation = 0;
        Vector2 pos = new Vector2(0, 0);

        if (!isDouble)
        {
            temp = Instantiate(Beat) as GameObject;
            if (type == "u")
            {
                temp.GetComponent<BeatObject>().key1 = KeyCode.W;
                temp.GetComponent<BeatObject>().key2 = KeyCode.I;

                pos.y += distance;
            }
            if (type == "d")
            {
                temp.GetComponent<BeatObject>().key1 = KeyCode.S;
                temp.GetComponent<BeatObject>().key2 = KeyCode.K;

                pos.y -= distance;
                rotation = 180;
            }
            if (type == "l")
            {
                temp.GetComponent<BeatObject>().key1 = KeyCode.A;
                temp.GetComponent<BeatObject>().key2 = KeyCode.J;

                pos.x -= distance;
                rotation = 90;
            }
            if (type == "r")
            {
                temp.GetComponent<BeatObject>().key1 = KeyCode.D;
                temp.GetComponent<BeatObject>().key2 = KeyCode.L;

                pos.x += distance;
                rotation = 270;
            }
        }
        else
        {
            temp = Instantiate(DoubleBeat) as GameObject;
            if (type == "u")
            {
                temp.GetComponent<DoubleBeatObject>().key1 = KeyCode.W;
                temp.GetComponent<DoubleBeatObject>().key2 = KeyCode.I;

                pos.y += distance;
            }
            if (type == "d")
            {
                temp.GetComponent<DoubleBeatObject>().key1 = KeyCode.S;
                temp.GetComponent<DoubleBeatObject>().key2 = KeyCode.K;

                pos.y -= distance;
                rotation = 180;
            }
            if (type == "l")
            {
                temp.GetComponent<DoubleBeatObject>().key1 = KeyCode.A;
                temp.GetComponent<DoubleBeatObject>().key2 = KeyCode.J;

                pos.x -= distance;
                rotation = 90;
            }
            if (type == "r")
            {
                temp.GetComponent<DoubleBeatObject>().key1 = KeyCode.D;
                temp.GetComponent<DoubleBeatObject>().key2 = KeyCode.L;

                pos.x += distance;
                rotation = 270;
            }
        }

        temp.transform.position = pos;
        temp.transform.Rotate(0, 0, rotation);
    }

}
