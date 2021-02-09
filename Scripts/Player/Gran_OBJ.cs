using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gran_OBJ : MonoBehaviour
{
    public static Gran_OBJ objectInstance;

    public List<GameObject> thePool;

    public GameObject GranToPool;

    public int amountToPool;




    private void Awake()
    {
        objectInstance = this;
    }

    // Use this for initialization
    void Start()
    {
        thePool = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject tempOBJ = Instantiate(GranToPool) as GameObject;
            tempOBJ.SetActive(false);
            thePool.Add(tempOBJ);
        }
    }

    public GameObject GetPooledOBJ()
    {
        for (int i = 0; i < thePool.Count; i++)
        {
            if (!thePool[i].activeInHierarchy)
            {
                return thePool[i];
            }
        }
        return null;
    }
}
