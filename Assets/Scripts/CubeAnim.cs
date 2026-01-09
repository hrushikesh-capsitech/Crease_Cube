using UnityEngine;

public class CubeAnim : MonoBehaviour
{

    [SerializeField] private GameObject hitParticlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            SpawnParticles(other);
            CubeSpawnerAnim.Instance.firstCube.GetComponent<PlatformGenerator>().PlatformPrefab.GetComponent<PlatformAnim>().MoveCubeAlongLen();
        };
    }



    private void SpawnParticles(Collider other)
    {
        // Get contact position = closest point to cube surface
        Vector3 spawnPos = other.ClosestPoint(transform.position);

        Instantiate(hitParticlePrefab, spawnPos, Quaternion.identity,gameObject.transform);
    }


}
