using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBeatObject : MonoBehaviour
{

    public bool clickAble = false;

    public KeyCode key1;
    public KeyCode key2;

    private float collisionBoxX;
    private float collisionBoxY;

    private float clickStart;
    private bool k1Clicked = false;
    private bool k2Clicked = false;
    private bool flag = false;

    // public AudioSource hitSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key1))
        {
            k1Clicked = true;
            if (!flag)
            {
                clickStart = Time.time;
                flag = true;
            }
        }

        if (Input.GetKeyDown(key2))
        {
            k2Clicked = true;
            if (!flag)
            {
                clickStart = Time.time;
                flag = true;
            }
        }

        if(Time.time - clickStart <= 0.1f)
        {
            if(k1Clicked && k2Clicked)
            {
                if (clickAble)
                {
                    float diffX = Mathf.Abs(transform.position.x - collisionBoxX);
                    float diffY = Mathf.Abs(transform.position.y - collisionBoxY);

                    GameHandler.instance.Hit(Mathf.Max(diffX, diffY));
                    // hitSound.Play();
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            k1Clicked = false;
            k2Clicked = false;
            flag = false;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            collisionBoxX = other.transform.position.x;
            collisionBoxY = other.transform.position.y;
           // Debug.Log("Entered detect area centered at " + collisionBoxX + ", " + collisionBoxY);
            clickAble = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            clickAble = false;
            GameHandler.instance.Miss();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
