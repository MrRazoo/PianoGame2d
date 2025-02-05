using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField]
    public GameObject objectToPool; // a Gameobject 
    [SerializeField]
    public int poolSize; // pool size 
    public Queue<GameObject> objectpool;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy any extra instances
        }
        
        objectpool = new Queue<GameObject>();
        InitializePool();
    }


    public void InitializePool() // this is just to set bullet prefab and pool count number from here (Optional)
    {
        for(int i = 0 ; i < poolSize ; i++)
        {
            GameObject pooledObj = Instantiate(objectToPool, transform.position, Quaternion.identity);
            pooledObj.SetActive(false);
            objectpool.Enqueue(pooledObj);
        }
    }

    public GameObject GetFromPool()
    {
        if(objectpool.Count > 0)
        {
            GameObject objectToReturn = objectpool.Dequeue();
            objectToReturn.SetActive(true);
            return objectToReturn;
        }
        else
        {
            // if there is empty pool ??? 
            return null;
        }
    }


    public void ReturnObjectToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false); // Deactivate the object
        objectpool.Enqueue(objectToReturn); // Put it back in the pool
    }

}
