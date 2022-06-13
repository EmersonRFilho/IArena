using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
