using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject left_Arm_Attack_Point, right_Arm_Attack_Point,
        left_Leg_Attack_Point, right_Leg_Attack_Point,
        bullet_Prefab, throw_Point;

    public float stand_Up_Timer = 2f;

    private Rigidbody myBody;
    private CharacterAnimation animationScript;
    private Player player;

    private void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();
        player = GetComponentInParent<Player>();
        myBody = GetComponentInParent<Rigidbody>();
    }

    void Left_Arm_Attack_On()
    {
        right_Arm_Attack_Point.GetComponent<AttackUniversal>().damage = GetAttackWeapon();
        left_Arm_Attack_Point.GetComponent<AttackUniversal>().damage = GetAttackWeapon();

        left_Arm_Attack_Point.SetActive(true);
    }

    void Left_Arm_Attack_Off()
    {
        if (left_Arm_Attack_Point.activeInHierarchy)
            left_Arm_Attack_Point.SetActive(false);
    }

    void Right_Arm_Attack_On()
    {
        right_Arm_Attack_Point.GetComponent<AttackUniversal>().damage = GetAttackWeapon();
        left_Arm_Attack_Point.GetComponent<AttackUniversal>().damage = GetAttackWeapon();

        right_Arm_Attack_Point.SetActive(true);
    }

    void Right_Arm_Attack_Off()
    {
        if (right_Arm_Attack_Point.activeInHierarchy)
            right_Arm_Attack_Point.SetActive(false);
    }

    void Left_Leg_Attack_On()
    {
        left_Leg_Attack_Point.SetActive(true);
    }

    void Left_Leg_Attack_Off()
    {
        if (left_Leg_Attack_Point.activeInHierarchy)
            left_Leg_Attack_Point.SetActive(false);
    }

    void Right_Leg_Attack_On()
    {
        right_Leg_Attack_Point.SetActive(true);
    }

    void Right_Leg_Attack_Off()
    {
        if (right_Leg_Attack_Point.activeInHierarchy)
            right_Leg_Attack_Point.SetActive(false);
    }

    void StandUpForDelegate()
    {
        StartCoroutine(StartUpAfterTime());
    }

    IEnumerator StartUpAfterTime()
    {
        yield return new WaitForSeconds(stand_Up_Timer);
        animationScript.StandUp();
    }

    void ThrowBullet()
    {
        GameObject obj = Instantiate(bullet_Prefab);
        obj.GetComponent<AttackUniversal>().collisionLayer = LayerMask.GetMask(player.IsPlayer ? "Enemy" : "Player");

        obj.transform.position = throw_Point.transform.position;
        Rigidbody rd = obj.GetComponent<Rigidbody>();
        rd.AddForce(transform.forward * 400f);
    }

    void DisableAIMovement()
    {
        GetComponentInParent<BaseMovement>().enabled = false;
        PlayerAttack playerAttack = GetComponentInParent<PlayerAttack>();
        if (playerAttack)
            playerAttack.enabled = false;

        transform.parent.gameObject.layer = 0;
    }

    void EnableAIMovement()
    {
        GetComponentInParent<BaseMovement>().enabled = true;
        PlayerAttack playerAttack = GetComponentInParent<PlayerAttack>();
        if (playerAttack)
            playerAttack.enabled = true;

        transform.parent.gameObject.layer = LayerMask.NameToLayer(player.IsPlayer ? "Player" : "Enemy");
    }

    void DisableFreezeX()
    {
        myBody.constraints &= ~RigidbodyConstraints.FreezePositionX;
    }

    void EnableFreezeX()
    {
        myBody.constraints |= RigidbodyConstraints.FreezePositionX;
    }

    public int GetAttackWeapon()
    {
        if (gameObject.transform.parent.GetComponent<PlayerAttack>())
        {
            return gameObject.transform.parent.GetComponent<PlayerAttack>().WeaponDamage;
        } else
        {
            return 6;
        }


    }
}
