using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    bool mIsGrounded = true;

    public bool IsGrounded
    {
        get { return mIsGrounded; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            mIsGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            mIsGrounded = false;
        }
    }
}
