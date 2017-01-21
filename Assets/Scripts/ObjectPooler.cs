using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {


    public GameObject[] GameObjectsToPool;
    // How many objects per pool
    public int[] PoolSize;
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
        m_waitingPool = new GameObject( "[ObjectPooler] " + name);

        m_pooledGameObjects = new List<GameObject>();

        for (int j = 0; j < GameObjectsToPool.Length; j++)
        {
            for ( int i = 0; i < PoolSize[j]; i++)
            {
                AddObjectToThePool( GameObjectsToPool[j] );
            }
        }
    }

    public GameObject GetPooledGameObject()
    {
        int randomIndex = 0;

        for ( int i = 0; i < m_pooledGameObjects.Count; i++ )
        {
            randomIndex = Random.Range( 0, m_pooledGameObjects.Count );

            if (!m_pooledGameObjects[ randomIndex ].gameObject.activeInHierarchy)
            {
                return m_pooledGameObjects[ randomIndex ];
            }
            else if (CanGrow)
            {
                return AddObjectToThePool(m_pooledGameObjects[randomIndex]);
            }
        }

        return null;
    }

    private GameObject AddObjectToThePool(GameObject gameObjectToPool)
    {
        if (gameObjectToPool == null )
        {
            Debug.LogWarning("The " + gameObject.name + " No Object defined", gameObject);
            return null;
        }

        GameObject newGameObject = Instantiate(gameObjectToPool);

        newGameObject.gameObject.SetActive( false );
        newGameObject.transform.parent = m_waitingPool.transform;
        newGameObject.name = gameObjectToPool.name + "-" + m_pooledGameObjects.Count;
        m_pooledGameObjects.Add( newGameObject );
        return newGameObject;
    }
}
