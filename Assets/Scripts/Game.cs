using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    private PeasantSpawner peasantSpawner;

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

    public bool IsRunning { get; private set; }
     
    public void StartGame()
    {
        Debug.Log("STARTING GAME");
        RoyalBehaviour.Instance.CarriageSpeed = 15;
        peasantSpawner.enabled = true;
        IsRunning = true;
    }

    public void StopGame()
    {
        Debug.Log("STOPPING GAME");
        RoyalBehaviour.Instance.CarriageSpeed = 0;
        peasantSpawner.enabled = false;
        IsRunning = false;
    }
}
