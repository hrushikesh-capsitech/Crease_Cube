using UnityEngine;

public class PlatformAnim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 minX;
    private Vector3 maxX;
    [SerializeField] private GameObject Cube;

    public bool readyToMove = false;
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

            if ((maxX.x - insCube.transform.position.x) < 0.2f)
            {
                readyToMove = false;
               // insCube.transform.position = new Vector3(0.16f, 1.95f, 0f);
                Debug.Log("Moved through the slide");
                CubeSpawnerAnim.Instance.moveToNextPart();
            }
        }
    }

    public void MoveCubeAlongLen()
    {
       // Debug.Log("Moved through the slide");
        boxCollider = gameObject.GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;
        minX = bounds.min;
        maxX = bounds.max;
        // insCube = Instantiate(Cube,new Vector3(bounds.min.x,bounds.min.y +0.46f,0f),Quaternion.identity);
        if (CubeSpawnerAnim.Instance.ActiveCube != null)
        {
            insCube = CubeSpawnerAnim.Instance.ActiveCube;
            insCube.transform.position = new Vector3(bounds.min.x, 1.95f, 0f);
        }
        readyToMove = true;
        Debug.Log("bounds are" + minX + ", " + maxX);
    }
}
