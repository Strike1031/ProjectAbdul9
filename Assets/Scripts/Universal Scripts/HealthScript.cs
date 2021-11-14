using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private HealthBar mHealthBar;
    private Player mPlayer;
    private CharacterAnimation animationScript;

    private bool characterDied;

    public HealthBar HealthBar
    {
        set
        {
            mHealthBar = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        mPlayer = GetComponent<Player>();
        animationScript = GetComponentInChildren<CharacterAnimation>();
    }

    public void ApplyDamage(float damage, bool knockDown)
    {
        if (characterDied)
            return;

        health -= damage;
        
        // display health UI
        mHealthBar.TakeDamage(damage);

        if (health <= 0f)
        {
            AudioMgr.Singleton.PlaySound(AudioMgr.AudioChannelType.Audio3D, "Grunt_1", transform);

            animationScript.Death();
            characterDied = true;
            GameMgr.Singleton.EndRound(mPlayer);
            return;
        }

        if (knockDown)
        {
            if (Random.Range(0, 2) > 0)
            {
                AudioMgr.Singleton.PlaySound(AudioMgr.AudioChannelType.Audio3D, "Grunt_1", transform);

                animationScript.KnockDown();
            }
        }
        else
        {
            AudioMgr.Singleton.PlaySound(AudioMgr.AudioChannelType.Audio3D, "Grunt_2", transform);
            animationScript.Hit();
        }
    }

    public void Reset()
    {
        health = 100f;
        characterDied = false;
        mHealthBar.Reset();
        animationScript.Play_IdleAnimation();
    }
}
