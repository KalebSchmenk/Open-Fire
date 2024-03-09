using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Controller : MonoBehaviour
{
    public IObjectPool<GameObject> m_Pool;
    public bool collectionChecks = true;
    public int maxPoolSize = 40;
    public GameObject bullet;

    public IObjectPool<GameObject> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                m_Pool = new LinkedPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }
            return m_Pool;
        }
    }
    GameObject CreatePooledItem()
    {
        var go = Instantiate(bullet, this.transform.position, Quaternion.identity, null);
        var bulletObj = go.GetComponent<BulletController>();
        bulletObj.controller = this;

        return go;
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(true);

        var bulletObj = poolObject.GetComponent<BulletController>();
        bulletObj.name = "Pooled Bullet";
        bulletObj.controller = this;
        bulletObj.Start();
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(GameObject poolObject)
    {
        Destroy(poolObject);
    }
}
