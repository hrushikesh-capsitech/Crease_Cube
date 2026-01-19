using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformGenerator : MonoBehaviour
{

    [SerializeField] public GameObject PlatformPrefab;

    [SerializeField] private GameObject platFormPrefabFromAssets;

    public GameObject SpawnPoint;

    [SerializeField] private float growSpeed = 2f;
    [SerializeField] private float maxHeight = 4f;
    [SerializeField] private float MoveSpeed = 1.5f;
    [SerializeField] private GameObject BridgeParent;


    private Vector3 startScale;
    private bool isGrowing = false;
    private bool isReleased = false;
    private Vector3 firstScale;
    private Vector3 firstPos;
    public bool isActive = true;

    public bool SwitchToOpposite = false;
    public bool isMoving = false;
    public bool isMovingUp = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    float distance = 3f;
    float travelDis = 0f;
    public bool isFallingCube = false;
    private Vector3 spawnPos;
    private float direction = 0f;
    public bool isShiftToOriginal = false;
    private bool isRotated = false;
    void Start()
    {
        startScale = PlatformPrefab.transform.localScale;
        firstPos = PlatformPrefab.transform.localPosition;
        // PlatFormPrefabAtStart = Instantiate(PlatformPrefab);
       // SpawnPoint.transform.localPosition = PlatformPrefab.transform.localPosition;
       spawnPos = PlatformPrefab.transform.position;
    }

    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject()) return;

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

        if (SwitchToOpposite)
        {
            if(!isRotated) transform.Rotate(0f, -MoveSpeed / 2 * Time.deltaTime, 0f);

            if((transform.rotation.y - 90f) <= 1f)
            {
              if(!isRotated)  transform.Rotate(0f, -90f, 0f);
                SwitchToOpposite = false;
               if(CubeSpawner.Instance.ActiveCube.transform.parent == this.transform) isActive = true;
                isRotated = true;
            }
        }

        if (isShiftToOriginal)
        {
            transform.Rotate(0f, 90f, 0f);
            isShiftToOriginal = false;
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
        PlatformPrefab.SetActive(true);
        float delta = growSpeed * Time.deltaTime;

        float newHeight = PlatformPrefab.transform.localScale.y + delta;
        newHeight = Mathf.Min(newHeight,maxHeight);

        float heightChange = newHeight - PlatformPrefab.transform.localScale.y;

       PlatformPrefab.transform.localScale = new Vector3(
            PlatformPrefab.transform.localScale.x,
            newHeight,
            PlatformPrefab.transform.localScale.z
        );

        PlatformPrefab.transform.localPosition += Vector3.up * (heightChange / 2f);
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
       if(!SwitchToOpposite) rb.AddForce(new Vector3(5f,0f,0f),ForceMode.Impulse);
        else rb.AddForce(new Vector3(10f, 5f, 0f), ForceMode.Impulse);


        if (GameManager.Instance != null) GameManager.Instance.StartFailCheck(2f);

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
        PlatformPrefab = Instantiate(platFormPrefabFromAssets, spawnPos, platFormPrefabFromAssets.transform.rotation,BridgeParent.transform);
        PlatformPrefab.tag = "platform";
        PlatformPrefab.SetActive(false);
        isReleased = false;
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
