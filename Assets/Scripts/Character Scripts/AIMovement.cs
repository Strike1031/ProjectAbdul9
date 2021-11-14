using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : BaseMovement
{
    private CharacterAnimation aiAnim;

    private Rigidbody myBody;
    public float speed = 5f;

    private Transform playerTarget;

    private float attack_Distance = 1.5f;
    private float chase_Player_After_Attack = 1f;

    private float current_Attack_Time;
    private float default_Attack_Time = 1.232f;

    private bool followPlayer, attackPlayer;

    public bool canThrow = true;

    protected override void Awake()
    {
        base.Awake();

        aiAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        transform.LookAt(new Vector3(playerTarget.position.x, transform.position.y, 0));

        if (Mathf.Abs(transform.position.x - playerTarget.position.x) > attack_Distance)
            myBody.constraints &= ~RigidbodyConstraints.FreezePositionX;
        else
            myBody.constraints |= RigidbodyConstraints.FreezePositionX;

        if (!followPlayer)
            return;

        if (!playerTarget)
            return;

        if (Mathf.Abs(transform.position.x - playerTarget.position.x) > attack_Distance)
        {
            myBody.velocity = transform.forward * speed;

            if (myBody.velocity.sqrMagnitude != 0)
            {
                aiAnim.Walk(true, false);
            }
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
        {
            myBody.velocity = Vector3.zero;
            aiAnim.Walk(false, false);

            followPlayer = false;
            attackPlayer = true;
        }
    }

    void Attack()
    {
        if (!attackPlayer)
            return;

        current_Attack_Time += Time.deltaTime;

        if (current_Attack_Time > default_Attack_Time)
        {
            if (canThrow)
            {
                aiAnim.AIAttack(Random.Range(0, 7));
            } else
            {
                aiAnim.AIAttack(Random.Range(0, 6));
            }
            current_Attack_Time = 0f;
        }

        if (Vector3.Distance(transform.position, playerTarget.position) >
            attack_Distance + chase_Player_After_Attack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }

    public void WalkStop()
    {
        myBody.velocity = Vector3.zero;
        aiAnim.Walk(false, false);
    }
}
