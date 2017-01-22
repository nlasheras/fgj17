using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    private PeasantSpawner peasantSpawner;
    private GameObject royalGO;

    private GameObject barCanvas;

    private void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        Instance = this;
        peasantSpawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<PeasantSpawner>();
        peasantSpawner.enabled = false;

        royalGO = GameObject.Find("Royal");
        royalGO.SetActive(false);
        SceneManager.LoadScene("Start_Screen", LoadSceneMode.Additive);

        barCanvas = GameObject.Find("BarCanvas");
        barCanvas.SetActive(false);
	}

    public void onStartGameClicked()
    {
        SceneManager.UnloadSceneAsync("Start_Screen");
        royalGO.SetActive(true);
    }

    public bool IsRunning { get; private set; }

    public bool IsFinished
    {
        get
        {
            if (!IsRunning)
            {
                int joy = RoyalBehaviour.Instance.Joy;
                return joy <= 0 || joy >= 100;
            }
            return false;
        }
    }
     
    public void StartGame()
    {
        Debug.Log("STARTING GAME");
        RoyalBehaviour.Instance.CarriageSpeed = 15;
        peasantSpawner.enabled = true;
        IsRunning = true;
        Cursor.visible = false;
        barCanvas.SetActive(true);
        SoundManager.Instance.StartCarriage();
    }

    public void StopGame()
    {
        Debug.Log("STOPPING GAME");
        RoyalBehaviour.Instance.CarriageSpeed = 0;
        peasantSpawner.enabled = false;
        IsRunning = false;
        Cursor.visible = true;
        barCanvas.SetActive(false);
        SceneManager.LoadScene("End_Screen", LoadSceneMode.Additive);
        SoundManager.Instance.StopCarriage();
    }
}
