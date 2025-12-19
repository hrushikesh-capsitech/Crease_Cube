using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    [SerializeField] public GameObject PlatformPrefab;

    [SerializeField] private GameObject SpawnPoint;

    [SerializeField] private float growSpeed = 2f;
    [SerializeField] private float maxHeight = 4f;

    private Vector3 startScale;
    private bool isGrowing = false;
    private bool isReleased = false;

    public bool isActive = true;
    void Start()
    {
        startScale = PlatformPrefab.transform.localScale;
    }

    void Update()
    {
        if (!isActive || isReleased) return;

        if (Input.GetMouseButtonDown(0))
        {
                isGrowing = true;
        }

        if (isGrowing)
        {
            PlatformPrefab.SetActive(true);
            GrowPlatFormSize();
        }

        if (Input.GetMouseButtonUp(0) && isGrowing)
        {
            isGrowing = false;
            isReleased = true;
            ReleasePlatform();
        }
    }

    public void DisablePlatform()
    {
        isActive = false;
        PlatformPrefab.SetActive(false);
    }

    public void GrowPlatFormSize()
    {
        float delta = growSpeed * Time.deltaTime;

        float newHeight = PlatformPrefab.transform.localScale.y + delta;
        newHeight = Mathf.Min(newHeight,maxHeight);

        float heightChange = newHeight - PlatformPrefab.transform.localScale.y;

       PlatformPrefab.transform.localScale = new Vector3(
            PlatformPrefab.transform.localScale.x,
            newHeight,
            PlatformPrefab.transform.localScale.z
        );

        PlatformPrefab.transform.position += Vector3.up * (heightChange / 1.3f);
    }

    public void ReleasePlatform()
    {
        Rigidbody rb = PlatformPrefab.GetComponent<Rigidbody>();
        HingeJoint hg = rb.GetComponent<HingeJoint>();
        if (hg != null)
        {
            hg.connectedBody = transform.gameObject.GetComponent<Rigidbody>();
        }
        if(rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        rb.AddForce(new Vector3(2f,0f,0f),ForceMode.Impulse);
    }
}
