using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool isRight = false;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = gameObject.transform.GetChild(0).position;

        if (isRight)
        {
            other.gameObject.transform.localRotation = new Quaternion(0, -90, 0, 0);
        } else
        {
            other.gameObject.transform.localRotation = new Quaternion(0, 90, 0, 0);
        }
    }

}
