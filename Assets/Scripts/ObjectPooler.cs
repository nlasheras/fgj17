using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {


    public GameObject GameObjectToPool;
    public int PoolSize = 20;
    public bool CanGrow = true;

    private List<GameObject> m_pooledGameObjects;
    private GameObject m_waitingPool;
   
    // Use this for initialization
    void Start () {
        FillObjectPool();
    }

    private void FillObjectPool()
    {
        // we create a container that will hold all the instances we create
        m_waitingPool = new GameObject("[ObjectPooler] " + name);

        m_pooledGameObjects = new List<GameObject>();

        for (int i = 0; i < PoolSize; i++)
        {
            AddObjectToThePool();
        }
    }

    public GameObject GetPooledGameObject()
    {
        for ( int i = 0; i < m_pooledGameObjects.Count; i++ )
        {
            if (!m_pooledGameObjects[i].gameObject.activeInHierarchy)
            {
                return m_pooledGameObjects[ i ];
            }
        }

        if ( CanGrow )
        {
            AddObjectToThePool();
        }

        return null;
    }

    private void AddObjectToThePool()
    {
        if ( GameObjectToPool == null )
        {
            Debug.LogWarning("The " + gameObject.name + " No Object defined", gameObject);
            return;
        }

        GameObject newGameObject = Instantiate( GameObjectToPool );

        newGameObject.gameObject.SetActive( false );
        newGameObject.transform.parent = m_waitingPool.transform;
        newGameObject.name = GameObjectToPool.name + "-" + m_pooledGameObjects.Count;
        m_pooledGameObjects.Add( newGameObject );
    }
}
