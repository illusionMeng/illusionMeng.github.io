using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{

    private float bpm;
    private GameObject target;
    private float speedMultiplier;

    public float velocity;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.instance.start)
        {
            speedMultiplier = GameHandler.instance.speedMultiplier;
            velocity = speedMultiplier * (bpm / 60f);
            bpm = GameHandler.instance.songBpm;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);
        }
    }
}
