using UnityEngine;

public class PlatformAnim : MonoBehaviour
{

    private Vector3 minX;
    private Vector3 maxX;
    [SerializeField] private GameObject Cube;

    public bool readyToMove = false;
    [SerializeField] private float Speed = 5f;

    private BoxCollider boxCollider;

    private GameObject insCube;
    void Start()
    {

    }


    void Update()
    {
        if (readyToMove)
        {
            insCube.transform.position += new Vector3(0.25f, 0f, 0f);

            //insCube.transform.position += Vector3.right * Speed * Time.deltaTime;


            if ((maxX.x - insCube.transform.position.x) < 0.1f)
            {
                readyToMove = false;

                Debug.Log("Moved through the slide");
                insCube.transform.position = new Vector3(maxX.x - 0.15f, 1.95f, 0f);
                CubeSpawnerAnim.Instance.moveToNextPart();
            }
        }
    }

    public void MoveCubeAlongLen()
    {

        boxCollider = gameObject.GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;
        minX = bounds.min;
        maxX = bounds.max;

        if (CubeSpawnerAnim.Instance.ActiveCube != null)
        {
            insCube = CubeSpawnerAnim.Instance.ActiveCube;
            insCube.transform.position = new Vector3(bounds.min.x, 1.95f, 0f);
        }
        readyToMove = true;
        Debug.Log("bounds are" + minX + ", " + maxX);
    }
}
