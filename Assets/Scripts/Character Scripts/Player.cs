using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player enemy;
    private bool mIsPlayer;
    private HealthScript mHealthScript;
    private CharacterAnimation animationScript;

    public bool IsPlayer
    {
        set
        {
            if (value)
            {
                Destroy(GetComponent<AIMovement>());
                gameObject.tag = Tags.PLAYER_TAG;
                gameObject.layer = LayerMask.NameToLayer("Player");
                GetComponent<PlayerMovement>().SetAttackLayer(LayerMask.GetMask("Enemy"));
            }
            else
            {
                Destroy(GetComponent<PlayerMovement>());
                Destroy(GetComponent<PlayerAttack>());
                gameObject.tag = Tags.ENEMY_TAG;
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                GetComponent<AIMovement>().SetAttackLayer(LayerMask.GetMask("Player"));
            }
            mIsPlayer = value;
        }
        get
        {
            return mIsPlayer;
        }
    }

    private void Awake()
    {
        mHealthScript = GetComponent<HealthScript>();
        animationScript = GetComponentInChildren<CharacterAnimation>();

       
    }

    private void Update()
    {
        if (enemy && mIsPlayer)
        {
            float offsetX = Mathf.Abs(Camera.main.GetComponent<FollowCamera>().offset.x);
            Camera.main.GetComponent<FollowCamera>().offset.x = 
                (enemy.transform.position.x < transform.position.x ? -offsetX : offsetX);
        }
    }

    public void EnablePlayer(bool isEnable)
    {
        if (mIsPlayer)
        {
            GetComponent<PlayerMovement>().WalkStop();

            GetComponent<PlayerMovement>().enabled = isEnable;
            GetComponent<PlayerAttack>().enabled = isEnable;
        }
        else
        {
            GetComponent<AIMovement>().WalkStop();

            GetComponent<AIMovement>().enabled = isEnable;
        }

        FindObjectOfType<AudioMgr>().UpdateMusic();
    }

    public void SetHealthBar(HealthBar healthBar)
    {
        mHealthScript.HealthBar = healthBar;
    }

    public float GetHealth()
    {
        return mHealthScript.health;
    }

    public void SetIdle()
    {
        animationScript.Play_IdleAnimation();
    }

    public void Reset()
    {
        mHealthScript.Reset();
        EnablePlayer(false);

        gameObject.layer = LayerMask.NameToLayer(mIsPlayer ? "Player" : "Enemy");
    }
}
