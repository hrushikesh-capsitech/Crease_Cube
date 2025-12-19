using UnityEngine;

public class CubeScript : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.MoveToNextCube();
    }
}
