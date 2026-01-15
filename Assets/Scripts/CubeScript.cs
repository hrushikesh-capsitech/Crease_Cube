using Firebase.Analytics;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    [SerializeField] private GameObject hitParticlePrefab;

    private bool soundLocked = false;

    private Camera cameraObj;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 0.1f;

    [SerializeField] private Transform redCube;  
    [SerializeField] private float perfectTolerance = 0.4f;


    private bool IsPefectHit = false;
    private bool lockCombo = false;
    private void Start()
    {
        cameraObj = Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("platform"))
        {
            if (!soundLocked)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.plankHitClip);
                StartCoroutine(LockSoundFor(0.1f));
            }
            CheckPerfectHit(other);
            Transform parent = gameObject.transform.parent.transform;
            PlatformGenerator pg = parent.gameObject.GetComponent<PlatformGenerator>();
            pg.isMoving = false;
            if (IsPefectHit && !pg.isActive)
            {
                ScoreManager.Instance.AddComboScore();
                Debug.Log("Combo checked");
               if(!lockCombo) ScoreManager.Instance.showComboPopup();
                StartCoroutine(LockCombo(0.5f));
            }
            else
            {
                Debug.Log("Score resetted");
                ScoreManager.Instance.ResetScore();
            }
            SpawnParticles(other);
            StartCoroutine(ShakeCoroutine());
            GameManager.Instance.MoveCubeAlongtheSlide();
            GameManager.Instance.CancelFailCheck();

        }
            
    }

    IEnumerator LockSoundFor (float sec)
    {
        soundLocked = true;
        yield return new WaitForSeconds(sec);
        soundLocked = false;
    }
    IEnumerator LockCombo(float sec)
    {
        lockCombo = true;
        yield return new WaitForSeconds(sec);
        lockCombo = false;

    }

    private void SpawnParticles(Collider other)
    {
        Vector3 spawnPos = other.ClosestPoint(transform.position);

        Instantiate(hitParticlePrefab, spawnPos, Quaternion.identity,gameObject.transform);
    }

    IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        Vector3 beforePos = cameraObj.transform.position;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeStrength;
            float y = Random.Range(-1f, 1f) * shakeStrength;

            cameraObj.transform.localPosition = cameraObj.transform.position + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraObj.transform.localPosition = beforePos;
    }


    private void CheckPerfectHit(Collider plankCollider)
    {
        float plankRightX = plankCollider.bounds.max.x;
        float targetCenterX = redCube.position.x;

        if (Mathf.Abs(plankRightX - targetCenterX) <= perfectTolerance)
        {
            Debug.Log("PERFECT HIT!");
            ShowPerfect();
            return;
        }
        IsPefectHit = false;
    }

    void ShowPerfect()
    {
        Debug.Log("SHOW PERFECT ANIMATION");
        ScoreManager.Instance.showPopup();
        IsPefectHit = true;
        //FirebaseAnalytics.LogEvent("perfect_hit");

    }
}
