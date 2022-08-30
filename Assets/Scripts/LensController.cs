using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(transform.position.x * 0, transform.position.y , 0 * transform.position.z));
        transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.right);
    }
}
