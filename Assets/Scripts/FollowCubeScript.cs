using UnityEngine;

public class FollowCubeScript : MonoBehaviour
{

    public float speed = 0.002f;
    public float heightOffset = 0.5f;
    public float rotateSpeed = 10f;

    void Update()
    {
        transform.localPosition += Vector3.up * Time.deltaTime;
    }


}

