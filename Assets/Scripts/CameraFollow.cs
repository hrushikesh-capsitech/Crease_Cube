using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Fixed Camera Values")]
    public float fixedY = 4.1f;
    public float fixedZ = -6f;

    [Header("Smoothness")]
    public float smoothSpeed = 6f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = new Vector3(
            target.position.x,
            fixedY,
            (fixedZ + target.transform.position.z)
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
