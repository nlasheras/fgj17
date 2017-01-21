using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PeasantSpawner peasantSpawner;

	void Start()
    {
        peasantSpawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<PeasantSpawner>();
        peasantSpawner.enabled = false;
	}
	
	void Update()
    {
		
	}
}
