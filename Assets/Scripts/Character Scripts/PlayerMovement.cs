using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    private CharacterAnimation player_Anim;
    private Rigidbody myBody;
    private CheckGround checkGround;

    public float walk_Speed = 3f;

    protected bool canRotate = false;

    private float rotation_Y = 90f;

    public GameObject Enemy;

    public bool walkingForwards = true;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        myBody = GetComponent<Rigidbody>();
        player_Anim = GetComponentInChildren<CharacterAnimation>();
        checkGround = GetComponentInChildren<CheckGround>();
    }

    protected void UpdateTags()
    {
        foreach (AIMovement x in GameObject.FindObjectsOfType<AIMovement>())
        {
            if (x.gameObject.tag == Tags.ENEMY_TAG)
            {
                Enemy = x.gameObject;
            } 
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        RotatePlayer();
        AnimatePlayerWalk();

        if (Input.GetKeyDown(KeyCode.UpArrow) && checkGround.IsGrounded)
        {
            player_Anim.Jump();
            myBody.AddForce(new Vector3(0, 8f, 0), ForceMode.Impulse);
            //canRotate = true;
        } else
        {
            //canRotate = false;
        }

        if (!Enemy)
        {
            UpdateTags();
        }
        else
        {
            transform.LookAt(new Vector3(Enemy.transform.position.x, transform.position.y, 0));
        }

        // canRotate = !checkGround.IsGrounded;
    }

    private void FixedUpdate()
    {
        DetectMovement();
    }

    void DetectMovement()
    {
        if (Mathf.Abs(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS)) > 0.1f)
            myBody.constraints &= ~RigidbodyConstraints.FreezePositionX;
        else
            myBody.constraints |= RigidbodyConstraints.FreezePositionX;

        myBody.velocity = new Vector3(
            Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * walk_Speed, 
            myBody.velocity.y,
            myBody.velocity.z);
    }

    void RotatePlayer()
    {
        if (canRotate)
        {
            if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
            {
                transform.rotation = Quaternion.Euler(0f, rotation_Y, 0f);
            }
            else if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
            {
                transform.rotation = Quaternion.Euler(0f, -rotation_Y, 0f);
            }
        }
    }

    void AnimatePlayerWalk()
    {

        Debug.Log(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS));

        if (gameObject.transform.localEulerAngles.y < 92)
        {
            if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0)
            {
                if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
                {
                    player_Anim.Walk(true, false);
                }
                else if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
                {
                    player_Anim.Walk(true, true);
                }
            }
            else
            {
                player_Anim.Walk(false, false);
            }
        } else
        {
            if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0)
            {
                if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
                {
                    player_Anim.Walk(true, true);
                }
                else if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
                {
                    player_Anim.Walk(true, false);
                }
            }
            else
            {
                player_Anim.Walk(false, false);
            }
        }
    }

    public void WalkStop()
    {
        myBody.velocity = Vector3.zero;
        player_Anim.Walk(false, false);
    }
}
