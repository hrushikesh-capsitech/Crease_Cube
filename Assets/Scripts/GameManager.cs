using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager Instance;

    public int CurrActiveCubeIndx = -1;
    public int prevActiveCubeIndx = -1;

    [SerializeField] private GameObject CubeSpawner;
    [SerializeField] private float tolerance = 0.5f;

    private Camera main;

    void Start()
    {
        Instance = this;
        CubeSpawner.GetComponent<CubeSpawner>().SpawnFirstPos();
        CurrActiveCubeIndx = 0;
        prevActiveCubeIndx = 0;
        main  = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToNextCube()
    {
        CubeSpawner.GetComponent<CubeSpawner>().SpawnCube();

        GameObject prevCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx];
        prevActiveCubeIndx = CurrActiveCubeIndx;
        CurrActiveCubeIndx++;
        GameObject currCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx];
        if (CubeSpawner.GetComponent<CubeSpawner>() != null)
        {
          prevCube.GetComponent<PlatformGenerator>().DisablePlatform();
        }
       
        if (CubeSpawner.GetComponent<CubeSpawner>() != null)
        {
           currCube.GetComponent<PlatformGenerator>().isActive = true;
          float distance = Vector3.Distance(main.transform.position, currCube.transform.position);
            main.transform.position = new Vector3(currCube.transform.position.x + 4f + tolerance, 4f, -6f);
        }
        CubeSpawner.GetComponent<CubeSpawner>().ActiveCube.transform.position = new Vector3(currCube.transform.position.x, 1.1f, 0f);

    }

    public void MoveCubeAlongtheSlide()
    {
        GameObject prevCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx];
        PlatformScript pc = prevCube.GetComponent<PlatformGenerator>()
           .PlatformPrefab.GetComponent<PlatformScript>();
        if (pc != null)
        {
            pc.MoveCubeAlongLen();
        }
       // MoveToNextCube();
    }
}
