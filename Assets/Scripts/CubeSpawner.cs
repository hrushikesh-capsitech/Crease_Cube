using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CubeSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static CubeSpawner Instance;
    [SerializeField] private GameObject cubePrefab;

    public List<GameObject> spawnedCubes = new List<GameObject>();


    void Start()
    {
        Instance = this;
    }


    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            SpawnCube();
        }
    }

    public void SpawnFirstPos()
    {
        GameObject newCube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);

        spawnedCubes.Add(newCube);
        newCube.GetComponent<PlatformGenerator>().isActive = true;
        SpawnCube();
    }

    public void SpawnCube()
    {
        if (cubePrefab == null)
            return;
 
        float minX = 2f;
 
        if (spawnedCubes.Count > 0)
        {
 
            minX = spawnedCubes[spawnedCubes.Count - 1].transform.position.x + 2;
 
        }
 
        float maxX = minX + 5f;
 
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0f, 0f);
 
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
 
        spawnedCubes.Add(newCube);
    }
}



