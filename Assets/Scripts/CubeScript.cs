using UnityEngine;

public class CubeScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("platform"))
        {
            GameManager.Instance.MoveCubeAlongtheSlide();
            GameManager.Instance.CancelFailCheck();
        }
            
    }
}
