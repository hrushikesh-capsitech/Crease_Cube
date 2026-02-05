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


    void Update()
    {
        if (readyToMove)
        {

            insCube.transform.position += Vector3.right * Speed * Time.deltaTime;


            if ((CubeSpawner.Instance.spawnedCubes[GameManager.Instance.CurrActiveCubeIndx + 1].GetComponent<PlatformGenerator>().SpawnPoint.transform.position.x + 0.1f) < insCube.transform.position.x)
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


            if (maxX.x <= CubeSpawner.Instance.spawnedCubes[GameManager.Instance.CurrActiveCubeIndx + 1].GetComponent<PlatformGenerator>().SpawnPoint.transform.position.x)
            {
                if ((maxX.x - insCube.transform.position.x) < 0.1f)
                {
                    readyToMove = false;

                    GameManager.Instance.MoveToNextCube();
                }
            }
            
        }
    }
     
    public void MoveCubeAlongLen()
    {
        boxCollider  = gameObject.GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;
        minX = bounds.min;
        maxX = bounds.max;

        if(CubeSpawner.Instance.ActiveCube != null)
        {
            insCube = CubeSpawner.Instance.ActiveCube;
            insCube.transform.position = new Vector3(bounds.min.x, bounds.min.y + 0.46f, 0f);

            insCubeOriginalY = insCube.transform.position.y;
        }
        //Invoke(nameof(readyFunc), 0.1f);

        readyToMove = true;
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
