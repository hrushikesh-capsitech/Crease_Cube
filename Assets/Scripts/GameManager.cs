using DG.Tweening;
using Firebase.Analytics;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;

    public int CurrActiveCubeIndx = -1;
    public int prevActiveCubeIndx = -1;

    [SerializeField] private GameObject CubeSpawner;
    [SerializeField] private float tolerance = 0.5f;

    private Camera main;

    private Coroutine failRoutine;

    [SerializeField] private Button restartButton;


    void Start()
    {
        Instance = this;
        CubeSpawner.GetComponent<CubeSpawner>().SpawnFirstPos();
        CurrActiveCubeIndx = 0;
        prevActiveCubeIndx = 0;
        main  = Camera.main;
        main.transform.position = new Vector3(4f, 5.5f, -6f);

        //FirebaseAnalytics.LogEvent("game_start");

        StartCoroutine(WaitForFirebase());

    }


    void Update()
    {
        
    }

    public void MoveToNextCube()
    {
        CubeSpawner.GetComponent<CubeSpawner>().SpawnCube();
        ScoreManager.Instance.AddScore();

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
            if(!currCube.GetComponent<PlatformGenerator>().SwitchToOpposite) currCube.GetComponent<PlatformGenerator>().isActive = true;
            currCube.GetComponent<PlatformGenerator>().isMoving = false;

            bool IsShiftedDirection = currCube.GetComponent<PlatformGenerator>().SwitchToOpposite;
            main.transform.DOMove(new Vector3(!IsShiftedDirection ? currCube.transform.position.x + 4f + tolerance :currCube.transform.position.x, 5.5f,
                !IsShiftedDirection ? -6f + currCube.transform.position.z : currCube.transform.position.z + tolerance - 5f),
               0.8f).SetEase(Ease.InOutSine);

        }
        CubeSpawner.GetComponent<CubeSpawner>().ActiveCube.transform.position = new Vector3(currCube.transform.position.x, 1.1f, currCube.transform.position.z);
       CubeSpawner.GetComponent<CubeSpawner>().ActiveCube.transform.parent = currCube.transform;
        if(CurrActiveCubeIndx > 20) currCube.GetComponent<PlatformGenerator>().ActivateMovingCube();
        for (int i = 0; i< CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes.Count - 5; i++)
        {
            
                Destroy(CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[i]); 
            
        }
    }
    public void MoveCubeAlongtheSlide()
    {
        GameObject prevCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx];
        prevCube.GetComponent<PlatformGenerator>().StopFalling();
        PlatformScript pc = prevCube.GetComponent<PlatformGenerator>()
           .PlatformPrefab.GetComponent<PlatformScript>();

        GameObject nextCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx + 1];
        if (pc != null)
        {
            pc.MoveCubeAlongLen(nextCube);
        }
        
    }


    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        
       // Destroy(CubeSpawner.GetComponent<CubeSpawner>().ActiveCube);

        AppStateManager.Instance.SetGameOver();
        GameOverScore.Instance.GameOverScores();

        if (FireBaseManager.IsFirebaseReady)
        {
            FirebaseAnalytics.LogEvent("game_over", new Parameter[]
            {
            new Parameter("score", ScoreManager.Instance.score)
            });
        }

        AdMobManager.Instance.ShowInterstitial();
        AdMobManager.Instance.LoadInterstitial();

    }

    public void StartFailCheck(float delay)
    {
        if (failRoutine != null)
        {
            StopCoroutine(failRoutine);
        }
        failRoutine = StartCoroutine(FailAfterDelay(delay));
    }


    IEnumerator FailAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject prevCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx];
       HingeJoint hg = prevCube.GetComponent<PlatformGenerator>()
           .PlatformPrefab.GetComponent<HingeJoint>();

        prevCube.GetComponent<PlatformGenerator>()
           .PlatformPrefab.gameObject.tag = "Untagged";
        Destroy(hg);

        yield return new WaitForSeconds(1f);
        GameOver();
    }


    public void CancelFailCheck()
    {
        if (failRoutine != null)
        {
            StopCoroutine(failRoutine);
            failRoutine = null;
        }
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        AppStateManager.Instance.SetGameplay();

        CurrActiveCubeIndx = -1;
        prevActiveCubeIndx = -1;

        main.transform.DOMove(new Vector3(4f, 5.5f, -6f), 0.8f);

        foreach (GameObject cube in CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes)
        {
            Destroy(cube);
        }

        Destroy(CubeSpawner.GetComponent<CubeSpawner>().ActiveCube);
        CubeSpawner.GetComponent<CubeSpawner>().Count = 0;
        CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes.Clear();

        CubeSpawner.GetComponent<CubeSpawner>().SpawnFirstPos();
      

        CurrActiveCubeIndx = 0;
        prevActiveCubeIndx = 0;

        PlayerPrefs.SetInt("CurrentScore", 0);

    }

    public void retryBtnOnClick()
    {
        GameObject prevCube;
        if (CubeSpawner.GetComponent<CubeSpawner>().ActiveCube.transform.parent == null)
        {
            prevCube = CubeSpawner.GetComponent<CubeSpawner>().spawnedCubes[CurrActiveCubeIndx];
            GameObject ActiveCube = CubeSpawner.GetComponent<CubeSpawner>().ActiveCube;
            ActiveCube.transform.parent = prevCube.transform;
            ActiveCube.transform.position = new Vector3(0f, 0.733f, 0f);
            ActiveCube.GetComponent<Rigidbody>().isKinematic = true;
            ActiveCube.GetComponent<Rigidbody>().useGravity = false;
            ActiveCube.transform.rotation = Quaternion.identity;
        }
        else
        {
            prevCube = CubeSpawner.GetComponent<CubeSpawner>().ActiveCube.transform.parent.gameObject;
        }        
        prevCube.GetComponent<PlatformGenerator>().ResetPlatform();
        prevCube.GetComponent<PlatformGenerator>().resetTimer();
        AppStateManager.Instance.SetGameplay();
        Time.timeScale = 1f;
    }


    IEnumerator WaitForFirebase()
    {
        while (!FireBaseManager.IsFirebaseReady)
            yield return null;

        FirebaseAnalytics.LogEvent("game_start");
    }


}
