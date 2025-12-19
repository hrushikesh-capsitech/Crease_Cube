using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CubeSpawner : MonoBehaviour
{


    public static CubeSpawner Instance;
    [SerializeField] private GameObject cubePrefab;

    [SerializeField] private GameObject cubeMovePrefab;
    public List<GameObject> spawnedCubes = new List<GameObject>();

    public GameObject ActiveCube;
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
       GameObject cube = Instantiate(cubeMovePrefab, new Vector3(0f,1.1f,0f), Quaternion.identity);
        cube.transform.parent = newCube.transform;
        ActiveCube = cube;
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



