using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour {

    // Movement
    private float Speed = 15;
    public Vector2 Direction = Vector2.left;

    // Happiness
    public float HappinessTickRate = 1f;
    public float HappinessAmount = 1f;
    public GameObject HappinessText;

    // Sprites
    public Sprite HappySprite;
    public Sprite NeutralSprite;

    private float lastTick;

    private float m_initialSpeed;
    private Vector2 m_movement;
    private SpriteRenderer m_spriteRenderer;
    
    // Use this for initialization
    void Awake()
    {
        m_initialSpeed = Speed;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        Speed = m_initialSpeed;
        // Peasants entering the view are not pleased right away ;(
        lastTick = Time.time;
        SetToNeutral();
    }

    private void SetToNeutral()
    {
        m_spriteRenderer.sprite = NeutralSprite;
    }

    // Update is called once per frame
    void Update () {
        Move();
        
        if (InputSystem.Instance.IsWaving && (Time.time > HappinessTickRate + lastTick))
        {
            GenerateHappiness();
        }
    }

    private void GenerateHappiness()
    {
        Debug.Log(gameObject.name+": I AM SO HAPPY!");
        m_spriteRenderer.sprite = HappySprite;

        // Happiness amount added popup text
        Vector3 textPosition = new Vector3( transform.position.x, transform.position.y + 1 );
        Instantiate( HappinessText, textPosition, transform.rotation );

        Invoke("SetToNeutral", HappinessTickRate * 0.5f);
        lastTick = Time.time;       
    }

    public void Move()
    {
        m_movement = Direction * ( Speed ) * Time.deltaTime;
        transform.Translate(m_movement, Space.World);

        if (transform.position.x < 0)
        {
           m_spriteRenderer.flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER TRIGGER");
    }

    void OnTriggerExit2D(Collider2D other)
    {
       if (other.CompareTag("GameController"))
        {
            gameObject.SetActive(false);
        }
    }
}
