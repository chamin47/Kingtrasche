using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 30;
    private List<GameObject> objects;

    private void Awake()
    {
        objects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objects.Add(obj);
        }

        // [todo] ½´ÆÃ¾ÀÀÌ ¾Æ´Ï¸é setactive(false)
    }

    public GameObject GetObject(Vector3 position)
    {
        foreach (GameObject obj in objects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                Managers.Sound.Play("laser_01", Sound.Effect);

                return obj;
            }
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
