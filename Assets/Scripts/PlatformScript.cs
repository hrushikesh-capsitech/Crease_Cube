using System.Collections;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{


    private Vector3 minX;
    private Vector3 maxX;
    [SerializeField] private GameObject Cube;

    private bool readyToMove = false;
    [SerializeField] private float Speed = 5f;

    private BoxCollider boxCollider;

    private GameObject insCube;

    private float insCubeOriginalY;

    public bool changeDirection = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (readyToMove)
        {
            //insCube.transform.position += new Vector3(0.18f, 0f, 0f);
            insCube.transform.localPosition += Speed * Time.deltaTime * (!changeDirection ? Vector3.right : new Vector3(0, 0, 1));


            if (((!changeDirection ? CubeSpawner.Instance.spawnedCubes[GameManager.Instance.CurrActiveCubeIndx + 1].GetComponent<PlatformGenerator>().SpawnPoint.transform.position.x + 0.1f
                : CubeSpawner.Instance.spawnedCubes[GameManager.Instance.CurrActiveCubeIndx + 1].GetComponent<PlatformGenerator>().SpawnPoint.transform.position.z) + 0.1f) < 
                (!changeDirection ?  insCube.transform.position.x : insCube.transform.position.z))
            {
                insCube.AddComponent<Rigidbody>();
                Rigidbody rb = insCube.GetComponent<Rigidbody>();

                rb.AddForce(new Vector3(Speed * 0.5f, 0f, 0f), ForceMode.Force);

                CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
                camFollow.SetTarget(insCube.transform);



                if ((insCubeOriginalY - insCube.transform.position.y) > 0.5f )
                {
                    readyToMove = false;
                    Invoke(nameof(gameOverFunc), 0.5f);
                }

            }


            if ((!changeDirection ? maxX.x : maxX.z) <= (!changeDirection ? CubeSpawner.Instance.spawnedCubes[GameManager.Instance.CurrActiveCubeIndx + 1].GetComponent<PlatformGenerator>().SpawnPoint.transform.position.x 
                : CubeSpawner.Instance.spawnedCubes[GameManager.Instance.CurrActiveCubeIndx + 1].GetComponent<PlatformGenerator>().SpawnPoint.transform.position.z))
            {
                if (((!changeDirection ? maxX.x : maxX.z) - (!changeDirection ? insCube.transform.position.x 
                    : insCube.transform.position.z) < 0.1f))
                {
                    readyToMove = false;

                    GameManager.Instance.MoveToNextCube();
                }
            }
            
        }
    }
     
    public void MoveCubeAlongLen(GameObject cube)
    {
        boxCollider  = gameObject.GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;
        minX = bounds.min;
        maxX = bounds.max;

        if(CubeSpawner.Instance.ActiveCube != null)
        {
            insCube = CubeSpawner.Instance.ActiveCube;
            insCube.transform.position = new Vector3(bounds.min.x, bounds.min.y + 0.46f, bounds.min.z);

            insCubeOriginalY = insCube.transform.position.y;
        }
        if (cube.GetComponent<PlatformGenerator>().isMovingUp)
        {
            changeDirection = true;
        }
        Invoke(nameof(readyFunc), 0.1f);
        insCube.transform.parent = null;
    }

    void readyFunc()
    {
               readyToMove = true;
    }

    void gameOverFunc()
    {
        GameManager.Instance.GameOver();
    }
}
