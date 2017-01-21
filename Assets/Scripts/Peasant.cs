using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour {

    // Movement
    public float Speed = 0;
    public Vector2 Direction = Vector2.left;

    // Happiness
    public float HappinessTickRate = 1f;
    public float HappinessAmount = 1f;

    private float lastTick;

    private float m_initialSpeed;
    private Vector2 m_movement;

    
    // Use this for initialization
    void Awake()
    {
        m_initialSpeed = Speed;    
    }

    protected virtual void OnEnable()
    {
        Speed = m_initialSpeed;
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
        lastTick = Time.time;
    }

    public void Move()
    {
        m_movement = Direction * (Speed / 10) * Time.deltaTime;
        transform.Translate(m_movement, Space.World);
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
