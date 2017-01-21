using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalVisualBehaviour : MonoBehaviour
{
    public Animator m_queen;

	void Start()
    {
		
	}
	
	void Update()
    {
        m_queen.SetBool("Waving", InputSystem.Instance.IsWaving);	
	}
}
