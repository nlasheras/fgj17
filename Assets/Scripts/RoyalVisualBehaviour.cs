using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalVisualBehaviour : MonoBehaviour
{
    public Animator m_queen;
    public Animator m_royal;

	void Start()
    {
		
	}
	
	void Update()
    {
        m_queen.SetBool("Waving", InputSystem.Instance.IsWaving);	
        if (Game.Instance.IsFinished)
        {
            m_royal.SetTrigger("End");
        }
	}
}
