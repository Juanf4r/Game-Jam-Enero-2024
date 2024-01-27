using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private List<GameObject> adsPrefabs;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private List<GameObject> adsPool;
    private int _minPoolSize = 20;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        InitializePool();
    }
        
    private void InitializePool()
    {
        adsPool = new List<GameObject>();

        for (int i = 0; i < _minPoolSize; i++)
        {
            GameObject adPrefab = GetRandomAdPrefab();
            GameObject ad = Instantiate(adPrefab, GetRandomAnchoredPosition(), Quaternion.identity, transform);
            ad.SetActive(false);
            adsPool.Add(ad);
        }
    }

    private GameObject GetRandomAdPrefab()
    {
        if (adsPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, adsPrefabs.Count);
            return adsPrefabs[randomIndex];
        }
        else
        {
            // Si no hay prefabs en la lista, devuelve un prefab de reemplazo o maneja la situación según tus necesidades.
            Debug.LogError("No hay prefabs disponibles en la lista.");
            return null;
        }
    }

    private Vector2 GetRandomAnchoredPosition()
    {
        float randomX = Random.Range(550,1500);
        float randomY = Random.Range(300, 700);

        return new Vector2(randomX, randomY);
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
        GameObject newAdPrefab = GetRandomAdPrefab();
        GameObject newAd = Instantiate(newAdPrefab, GetRandomAnchoredPosition(), Quaternion.identity, transform);
        newAd.SetActive(false);
        adsPool.Add(newAd);

        return newAd;
    }
}
