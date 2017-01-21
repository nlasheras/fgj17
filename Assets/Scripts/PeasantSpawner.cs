using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantSpawner : MonoBehaviour {

    [Header("Spawning")]
    public float InitialDelay = 5f;
    public bool Spawning = true;

    [Space(10)]
    [Header("Spawn Timing")]
    public float MinSpawnTime = 0.5f;
    public float MaxSpawnTime = 2f;

    [Space(10)]
    [Header("Y Clamp")]
    public float MinimumYClamp;
    public float MaximumYClamp;

    ObjectPooler m_objectPooler;

    void Awake()
    {
        m_objectPooler = GetComponent<ObjectPooler>();
    }

    void Start () {
        Invoke("Spawn", InitialDelay);
    }

    public GameObject Spawn()
    {
        Vector3 spawnPosition = transform.position;
        float spawnY = Random.Range(MinimumYClamp, MaximumYClamp);

        spawnPosition.y += spawnY;


        GameObject nextGameObject = m_objectPooler.GetPooledGameObject();

        if (nextGameObject == null)
        {
            return null;
        }

        nextGameObject.transform.position = spawnPosition;
        nextGameObject.GetComponent<Renderer>().sortingOrder = Mathf.FloorToInt(spawnY)* -1;
        nextGameObject.gameObject.SetActive(true);
      
        // Invoke this function again after a random time
        Invoke("Spawn", Random.Range( MinSpawnTime, MaxSpawnTime ));

        return (nextGameObject);
    }

}
