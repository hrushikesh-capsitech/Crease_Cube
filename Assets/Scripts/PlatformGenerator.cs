using System.Collections;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    [SerializeField] public GameObject PlatformPrefab;

    [SerializeField] private GameObject platFormPrefabFromAssets;

    public GameObject SpawnPoint;

    [SerializeField] private float growSpeed = 2f;
    [SerializeField] private float maxHeight = 4f;
    [SerializeField] private float MoveSpeed = 1.5f;


    private Vector3 startScale;
    private bool isGrowing = false;
    private bool isReleased = false;
    private Vector3 firstScale;
    private Vector3 firstPos;
    public bool isActive = true;

    private bool SwitchToOpposite = false;
    public bool isMoving = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    float distance = 3f;
    float travelDis = 0f;
    public bool isFallingCube = false;
    void Start()
    {
        startScale = PlatformPrefab.transform.localScale;
        firstPos = PlatformPrefab.transform.localPosition;
        // PlatFormPrefabAtStart = Instantiate(PlatformPrefab);
    }

    void Update()
    {

        if (!isMoving)
        {
            StopCoroutine(leftMoveRoutine());
            StopCoroutine(rightMoveRoutine());
        }

        if (isMoving && (isMovingLeft || isMovingRight))
        {
            if ((distance - travelDis) >= 0.03f)
            {
                Debug.Log("movement running function is acxtive");
                travelDis += Time.deltaTime * MoveSpeed;
               if(isMovingRight) transform.position += new Vector3(Time.deltaTime * MoveSpeed, 0f, 0f);
                else
                {
                    transform.position -= new Vector3(Time.deltaTime * MoveSpeed, 0f, 0f);
                }
            }
            else
            {
                if (isMovingRight)
                {
                    isMovingRight = false;
                    StartCoroutine(leftMoveRoutine());
                }
                else
                {
                    isMovingLeft = false;
                    StartCoroutine(rightMoveRoutine());
                }
            }

        }
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
        rb.AddForce(new Vector3(5f,0f,0f),ForceMode.Impulse);


       if(GameManager.Instance != null) GameManager.Instance.StartFailCheck(2f);

    }

    public void SpawnForAnim(float time)
    {
        Debug.Log("Enter to spawn platform");
        float timeInSec = time;
        while (timeInSec > 0f)
        {
            GrowPlatFormSize();
            timeInSec -= Time.deltaTime;

        }
        ReleasePlatform();
    }

    public void ResetPlatform()
    {
        if(PlatformPrefab != null)
        {
            Destroy(PlatformPrefab);
        }
        PlatformPrefab = Instantiate(platFormPrefabFromAssets, platFormPrefabFromAssets.transform.position, platFormPrefabFromAssets.transform.rotation,transform);
    }

    public void moveCube()
    {
        isMoving = true;
        isMovingRight = true;
        MoveSpeed = Random.Range(1f, 2f);
    }

    IEnumerator rightMoveRoutine()
    {
        distance = 3f;
        travelDis = 0f;
        MoveSpeed = Random.Range(1f, 2f);
        yield return new WaitForSeconds(2f);
        isMovingRight = true;
    }

    IEnumerator leftMoveRoutine()
    {
         distance = 3f;
         travelDis = 0f;
        MoveSpeed = Random.Range(1f, 2f);
        yield return new WaitForSeconds(2f);
        isMovingLeft = true;
    }

    public void ActivateMovingCube()
    {
        Debug.Log("Falling cube is active");
        isFallingCube = true;
        StartCoroutine(GameOverDelayifNotMoves());
    }

    public void StopFalling()
    {
        StopAllCoroutines();
    }

    IEnumerator GameOverDelayifNotMoves()
    {
        yield return new WaitForSeconds(5f);
            Debug.Log("Falling cube is active and the active cube is also a child");
            isActive = false;
            PlatformPrefab.tag = "Untagged";
        PlatformPrefab.SetActive(false);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            yield return new WaitForSeconds(2f);
            GameManager.Instance.GameOver();
    }
}
