using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LookAtPoint : MonoBehaviour
{
    public Vector3 lookAtPoint = new Vector3(1,2,1);

    public void Update()
    {
        transform.LookAt(lookAtPoint);
    }
}