using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject adsPrefab;
    private int _poolSize = 5;

    public List<GameObject> adsPool;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        adsPool = new List<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject ad = Instantiate(adsPrefab);
            ad.SetActive(false);
            adsPool.Add(ad);
        }
    }

    public GameObject GetAd()
    {
        // Buscar un objeto inactivo en el pool
        for (int i = 0; i < adsPool.Count; i++)
        {
            if (!adsPool[i].activeInHierarchy)
            {
                return adsPool[i];
            }
        }

        // Si no hay objetos inactivos, crear uno nuevo y agregarlo al pool
        GameObject newAd = Instantiate(adsPrefab);
        newAd.SetActive(false);
        adsPool.Add(newAd);

        return newAd;
    }
}
