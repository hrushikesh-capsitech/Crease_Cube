using System.Collections;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    [SerializeField] private GameObject hitParticlePrefab;

    private bool soundLocked = false;

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("platform"))
        {
            if (!soundLocked)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.plankHitClip);
                StartCoroutine(LockSoundFor(0.1f));
            }

            SpawnParticles(other);
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


    private void SpawnParticles(Collider other)
    {
        // Get contact position = closest point to cube surface
        Vector3 spawnPos = other.ClosestPoint(transform.position);

        Instantiate(hitParticlePrefab, spawnPos, Quaternion.identity);
    }
}
