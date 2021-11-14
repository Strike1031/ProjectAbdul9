using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOffset : MonoBehaviour
{

    protected Animator animator;
    public bool UpdateAnimator = false;


    void Start()
    {
        animator = gameObject.transform.parent.GetComponent<Animator>();
    }


    void FixedUpdate()
    {

        //if (UpdateAnimator)
       // {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
       // } else
       // {
        //    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
       // }

        //if (animator.GetCurrentAnimatorClipInfo(0).Length == 1)
        //{
        //    if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "rig|compo")
        //    {
        //        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -4.575184f);
        //    }
        //    else
        //    {
        //        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
        //    }
        //} else if (animator.GetCurrentAnimatorClipInfo(0).Length > 1)
        //{
        //    if (animator.GetCurrentAnimatorClipInfo(0)[1].clip.name == "rig|compo")
        //    {
        //        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -4.575184f);
        //    }
        //    else
        //    {
        //        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
        //    }
        //} else if (animator.GetCurrentAnimatorClipInfo(0).Length > 2)
        //{
        //    if (animator.GetCurrentAnimatorClipInfo(0)[2].clip.name == "rig|compo")
        //    {
        //        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -4.575184f);
        //    }
        //    else
        //    {
        //        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
        //    }
        //}

    }
}
