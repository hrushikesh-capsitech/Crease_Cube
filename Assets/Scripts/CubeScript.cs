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
       if(other.gameObject.CompareTag("platform")) GameManager.Instance.MoveCubeAlongtheSlide();
    }
}
