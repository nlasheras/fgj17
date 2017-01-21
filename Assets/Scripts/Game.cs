using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private PeasantSpawner peasantSpawner;

	void Start()
    {
        peasantSpawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<PeasantSpawner>();
        peasantSpawner.enabled = false;

        //TODO: Remove this call and HOOK the animation ending to it
        StartGame();
	}

    void StartGame()
    {
        Debug.Log("STARTING GAME");
        RoyalBehaviour.Instance.CarriageSpeed = 15;
        peasantSpawner.enabled = true;
    }

    void StopGame()
    {
        Debug.Log("STOPPING GAME");
        RoyalBehaviour.Instance.CarriageSpeed = 0;
        peasantSpawner.enabled = false;
    }
}
