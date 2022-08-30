using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{

    public Transform Target;

    // Update is called once per frame
    public void Update()
    {

        transform.LookAt(Target.position);

        transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.right);
    }
}
