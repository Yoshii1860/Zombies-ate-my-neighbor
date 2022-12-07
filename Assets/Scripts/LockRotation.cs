using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    [SerializeField] Transform targetCamera;

    private void Update()
    {
        transform.LookAt(targetCamera);
    }
}
