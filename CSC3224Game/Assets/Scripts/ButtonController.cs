using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite unpressed;
    public Sprite pressed;
    public Sprite doublePressed;

    public KeyCode key1;
    public KeyCode key2;

   public AudioSource hitSound;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        spriteRenderer.sprite = unpressed;

        //if(Input.GetKeyDown(key1) || Input.GetKeyDown(key2))
       //     hitSound.Play();

        if (Input.GetKey(key1) ^ Input.GetKey(key2))
        {
            spriteRenderer.sprite = pressed;
        }
        else if (Input.GetKey(key1) && Input.GetKey(key2))
        {
            spriteRenderer.sprite = doublePressed;
        }

    }
}
