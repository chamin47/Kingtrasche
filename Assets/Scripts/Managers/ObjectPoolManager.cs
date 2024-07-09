using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public List<ObjectPool> poolList = new List<ObjectPool>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // �Ŵ����� ���� �������� ���Ͻ�Ŵ
    public GameObject GetObjectFromPool(string poolName, Vector3 position)
    {
        ObjectPool pool = poolList.Find(p => p.name == poolName);
        return pool.GetObject(position);
    }

    public void ReturnObjectToPool(string poolName, GameObject obj)
    {
        ObjectPool pool = poolList.Find(p => p.name == poolName);
        pool.ReturnObject(obj);
    }

}
