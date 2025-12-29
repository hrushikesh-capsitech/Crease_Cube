using UnityEngine;

public class CubeScript : MonoBehaviour
{

    [SerializeField] private GameObject hitParticlePrefab;

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("platform"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.plankHitClip);
            SpawnParticles(other);
            GameManager.Instance.MoveCubeAlongtheSlide();
            GameManager.Instance.CancelFailCheck();

        }
            
    }


    private void SpawnParticles(Collider other)
    {
        // Get contact position = closest point to cube surface
        Vector3 spawnPos = other.ClosestPoint(transform.position);

        Instantiate(hitParticlePrefab, spawnPos, Quaternion.identity);
    }
}
