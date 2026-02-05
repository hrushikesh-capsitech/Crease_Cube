using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Fixed Camera Values")]
    public float fixedY = 5.5f;
    public float fixedZ = -6f;

    [Header("Smoothness")]
    public float smoothSpeed = 6f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = new Vector3(
            target.position.x,
            fixedY,
            fixedZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }

    //void LateUpdate()
    //{
    //    if (target == null) return;

    //    Vector3 currentPos = transform.position;

    //    float smoothX = Mathf.Lerp(
    //        currentPos.x,
    //        target.position.x,
    //        smoothSpeed * Time.deltaTime
    //    );

    //    transform.position = new Vector3(
    //        smoothX,
    //        fixedY,
    //        fixedZ    // 🔒 HARD LOCK Z
    //    );
    //}

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
