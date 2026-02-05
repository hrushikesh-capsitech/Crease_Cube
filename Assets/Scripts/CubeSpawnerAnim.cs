using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnerAnim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public static CubeSpawnerAnim Instance;
    public GameObject firstCube;
    public GameObject secondCube;
    public GameObject ActiveCube;
    [SerializeField] private GameObject cubePrefab;
    private Vector3 firstCubePos;
    private Vector3 secondCubePos;
    private bool isMoving = false;
    [SerializeField] private GameObject cubeMovePrefab;
    public bool isActive = false;


    public List<GameObject> spawnedCubes = new List<GameObject>();
    void Start()
    {
        Instance = this;
        firstCubePos = firstCube.transform.position;
        SpawnCube();
    }

    
    void Update()
    {
        if (!isActive) return;
        if (isMoving)
        {
            secondCube.transform.position += new Vector3(-0.07f, 0f, 0f);
            if ((secondCube.transform.position.x - firstCubePos.x) < 0.1f)
            {
                isMoving = false;
                firstCube.GetComponent<PlatformGenerator>().PlatformPrefab.SetActive(false);
                firstCube.GetComponent<PlatformGenerator>().ResetPlatform();
                firstCube.SetActive(false);
                secondCube.transform.position = firstCubePos;
                firstCube.transform.position = secondCubePos;
             
                firstCube.SetActive(true);
                StartCoroutine(SpawnRoutine());
                
            }
        }
        if (isMoving)
        {
            return;
        }
    }

    IEnumerator SpawnRoutine()
    {
            yield return new WaitForSeconds(0.6f);

            secondCube.GetComponent<PlatformGenerator>().PlatformPrefab.SetActive(true);
            secondCube.GetComponent<PlatformGenerator>().SpawnForAnim(1.6f);
            GameObject temp = firstCube;
            firstCube = secondCube;
            secondCube = temp;

    }
    public void SpawnCube()
    {
        if (cubePrefab == null)
            return;

        Vector3 spawnPosition = new Vector3(3f, 1f, 0f);

        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity,transform);

        secondCube = newCube;
        secondCubePos = secondCube.transform.position;
        GameObject cube = Instantiate(cubeMovePrefab, new Vector3(-0.20f, 1.95f, 0f), Quaternion.identity);
        ActiveCube = cube;
        ActiveCube.transform.parent = firstCube.transform;
        firstCube.GetComponent<PlatformGenerator>().PlatformPrefab.SetActive(true);
        firstCube.GetComponent<PlatformGenerator>().SpawnForAnim(1.6f);

    }

    public void moveToNextPart()
    {

        firstCube.SetActive(false);
        ActiveCube.transform.parent = secondCube.transform;
        isMoving = true;
        
    }
}
