using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;

    private int CurrActiveCubeIndx = -1;

    [SerializeField] private GameObject CubeSpawner;
    [SerializeField] private float tolerance = 0.5f;

    private Camera main;

    void Start()
    {
        Instance = this;
        CubeSpawner.GetComponent<CubeSpawner>().SpawnFirstPos();
        CurrActiveCubeIndx = 0;
        main  = Camera.main;
    }


    void Update()
    {
        
    }

    public void MoveToNextCube()
    {
        CubeSpawner.GetComponent<CubeSpawner>().SpawnCube();

        GameObject prevCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx];
        CurrActiveCubeIndx++;
        GameObject currCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx];
        if (CubeSpawner.GetComponent<CubeSpawner>() != null)
        {
            prevCube.GetComponent<PlatformGenerator>().DisablePlatform();
        }
        
        if (CubeSpawner.GetComponent<CubeSpawner>() != null)
        {
            currCube.GetComponent<PlatformGenerator>().isActive = true;

            //main.transform.position = new Vector3(currCube.transform.position.x + 3f + tolerance, 4f, -6f);

            main.transform.DOMove(new Vector3(currCube.transform.position.x + 4f + tolerance, 3f, -6f), 0.5f).SetEase(Ease.InOutSine);
        }

        for (int i = 0; i< CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes.Count - 5; i++)
        {
            
                Destroy(CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[i]);
            
        }
    }
}
