using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 3f;
    public float damage = 1f;

    [Range(3, 5)]
    public int seriousAttackCount = 3;
    public float seriousAttackTimeOffset = 1.5f;
    public bool isBullet;

    public string[] attackSounds;
    public GameObject hit_Fx_Prefab;

    private int attackedCount;
    private float previousAttackTime;

    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);

        if (hit.Length > 0)
        {
            GetSeriousAttackCount();
            for (int i = 0; i < hit.Length; i++)
            {
                HealthScript hScript = hit[i].GetComponent<HealthScript>();
                if (hScript)
                {
                    hScript.ApplyDamage(damage, attackedCount >= seriousAttackCount);

                    // blood
                    GameObject obj = Instantiate(hit_Fx_Prefab);
                    obj.transform.position = transform.position;
                    Destroy(obj, 10);

                    // sound
                    if (attackSounds.Length > 0)
                    {
                        string sound = attackSounds[Random.Range(0, attackSounds.Length - 1)];
                        AudioMgr.Singleton.PlaySound(AudioMgr.AudioChannelType.Audio3D, sound, transform);
                    }

                    if (isBullet)
                    {
                        Destroy(gameObject);
                    }
                    else
                        gameObject.SetActive(false);
                }
            }
        }
    }

    void GetSeriousAttackCount()
    {
        if (Time.time - previousAttackTime > seriousAttackTimeOffset)
        {
            attackedCount = 0;
        }
        else
        {
            attackedCount++;
        }
        previousAttackTime = Time.time;
    }
}
