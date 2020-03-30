using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatObject : MonoBehaviour
{
    public KeyCode key1;
    public KeyCode key2;

    //public MovementHandler mh;

    private bool clickAble = false;

    private float collisionBoxX;
    private float collisionBoxY;

   // public AudioSource hitSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key1) || Input.GetKeyDown(key2))
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            collisionBoxX = other.transform.position.x;
            collisionBoxY = other.transform.position.y;
          //  Debug.Log("Entered detect area centered at " + collisionBoxX + ", " + collisionBoxY);
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
