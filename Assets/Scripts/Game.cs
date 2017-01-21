using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public PeasantSpawner peasantSpawner;


    private void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        Instance = this;
        peasantSpawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<PeasantSpawner>();
        peasantSpawner.enabled = false;
	}

    public void StartGame()
    {
        peasantSpawner.enabled = true;
        RoyalBehaviour.Instance.CarriageSpeed = 15;
    }

	void Update()
    {
		
	}
}
