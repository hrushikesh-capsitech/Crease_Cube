using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private Vector3 minX;
    private Vector3 maxX;
    [SerializeField] private GameObject Cube;

    private bool readyToMove = false;
    [SerializeField] private float Speed = 1f;

    private BoxCollider boxCollider;

    private GameObject insCube;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToMove)
        {
            Debug.Log("Entered into the update func");
            insCube.transform.position += new Vector3(0.06f, 0f, 0f);

            if((maxX.x - insCube.transform.position.x) < 0.3f)
            {
                readyToMove = false;
                GameObject currCube = CubeSpawner.Instance.spawnedCubes[GameManager.Instance.CurrActiveCubeIndx + 1];
                CubeSpawner.Instance.ActiveCube.transform.position = new Vector3(currCube.transform.position.x, 1.1f, 0f);
                GameManager.Instance.MoveToNextCube();
            }
        }
    }

    public void MoveCubeAlongLen()
    {
        boxCollider  = gameObject.GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;
        minX = bounds.min;
        maxX = bounds.max;
      // insCube = Instantiate(Cube,new Vector3(bounds.min.x,bounds.min.y +0.46f,0f),Quaternion.identity);
        if(CubeSpawner.Instance.ActiveCube != null)
        {
            insCube = CubeSpawner.Instance.ActiveCube;
            insCube.transform.position = new Vector3(bounds.min.x, bounds.min.y + 0.46f, 0f);
        }
        readyToMove = true;
        Debug.Log("bounds are" + minX + ", " + maxX);
    }
}
