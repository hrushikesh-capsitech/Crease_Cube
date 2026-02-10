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
    public int Count = 0;

    void Start()
    {
        Instance = this;
        Count = 0;
    }

    public void SpawnFirstPos()
    {
        GameObject newCube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity,transform);
       GameObject cube = Instantiate(cubeMovePrefab, new Vector3(0f,1.1f,0f), Quaternion.identity,transform);
        cube.transform.Rotate(0f, 114f, 0f);
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

        Count++;
        float minX = 2f;


        int val = Random.Range(0, 10);
        if(Count > 10 && (val > 5))
        {
            spawnCubeZ();
            return;
        }
        if (spawnedCubes.Count > 0)
        {
 
            minX = spawnedCubes[spawnedCubes.Count - 1].transform.position.x + 2;
 
        }
 
        float maxX = minX + 3f;
 
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0f, spawnedCubes[spawnedCubes.Count - 1].transform.position.z);
 
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity,transform);
            PlatformGenerator pg = spawnedCubes[spawnedCubes.Count - 1].GetComponent<PlatformGenerator>();
         if(pg.gameObject.transform.rotation.y != 0f) pg.isShiftToOriginal = true;
            pg.PlatformPrefab.GetComponent<PlatformScript>().changeDirection = false;
        if (Count > 3)
        {
            int isMoveOrNot = 0;

            if (Count % 3 == 0) isMoveOrNot = 2;
            else
            {
                isMoveOrNot = 0;
            }
                Debug.Log("Movement is activated");
            if(isMoveOrNot > 1)
            {
                newCube.GetComponent<PlatformGenerator>().isMoving = true;
                newCube.GetComponent<PlatformGenerator>().moveCube();
            }
        }
        spawnedCubes.Add(newCube);
    }

    public void spawnCubeZ()
    {
        float minX = 2f;
        float minZ = 2f;

        if (spawnedCubes.Count > 0)
        {

            minX = spawnedCubes[spawnedCubes.Count - 1].transform.position.x;
            minZ += spawnedCubes[spawnedCubes.Count - 1].transform.position.z;

        }

        float maxX = minX + 3f;
        float maxZ = minZ + 3f;

        Vector3 spawnPosition = new Vector3(minX, 0f, Random.Range(minZ, maxZ));
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity, transform);
        PlatformGenerator pg = spawnedCubes[spawnedCubes.Count - 1].GetComponent<PlatformGenerator>();
        if (!pg.PlatformPrefab.GetComponent<PlatformScript>().changeDirection)
        {
            Debug.Log("The platform direction is changed");
            pg.SwitchToOpposite = true;
        }
        spawnedCubes[spawnedCubes.Count - 1].GetComponent<PlatformGenerator>().PlatformPrefab.GetComponent<PlatformScript>().changeDirection = true;
        newCube.GetComponent<PlatformGenerator>().SwitchToOpposite = true;
        spawnedCubes.Add(newCube);          
    }
   
}



