using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;
    private bool isAnimating;
    public bool IsAnimating
    {
        get {
            return isAnimating;
        }
    }

    public GameObject RealWeaponGO;
    public GameObject SpineGO;
    public Vector3 StoppedWeaponPos;
    public Vector3 StoppedWeaponRot;
    public string StoppedWeaponBoneName = "torso";
    public float timeEventStarts = 0.02f;
    public float timeEventEnds = 0.1f;
    public bool weaponStopped = false;

    public bool forceNormalLocation = false;
    public GameObject NormalLocation;

    public bool useBothWeapons = false;
    public GameObject BothWeaponGO;
    public string BothStoppedWeaponBoneName = "torso";

    public bool useBothWeaponsTransform = false;
    public Vector3 BothStoppedWeaponPos;
    public Vector3 BothStoppedWeaponRot;
    // Start is called before the first frame update

    public bool facingRight = false;

    public bool canWalkBackwards = false;
    public Vector3 FaceStoppedWeaponPos;
    public Vector3 FaceStoppedWeaponRot;
    public Vector3 FaceBothStoppedWeaponPos;
    public Vector3 FaceBothStoppedWeaponRot;

    public void Start()
    {
        InvokeRepeating("CheckAnims", 1, 0.001f);
    }

    public void CheckAnims()
    {
        Debug.Log(gameObject.transform.parent.localEulerAngles.y.ToString());

        if (gameObject.transform.parent.localEulerAngles.y < 92)
        {

            if (weaponStopped)
            {
                RealWeaponGO.transform.parent.name = "a";

                RealWeaponGO.transform.position = (SpineGO.transform.position - StoppedWeaponPos);
                RealWeaponGO.transform.localEulerAngles = StoppedWeaponRot;

                if (useBothWeapons)
                {

                    BothWeaponGO.transform.parent.name = "a";

                    if (useBothWeaponsTransform)
                    {
                        BothWeaponGO.transform.position = (SpineGO.transform.position - BothStoppedWeaponPos);
                        BothWeaponGO.transform.localEulerAngles = BothStoppedWeaponRot;
                    }
                    else
                    {
                        BothWeaponGO.transform.position = (SpineGO.transform.position - StoppedWeaponPos);
                        BothWeaponGO.transform.localEulerAngles = StoppedWeaponRot;
                    }
                }
            }
            else
            {
                RealWeaponGO.transform.parent.name = StoppedWeaponBoneName;

                if (useBothWeapons)
                {
                    BothWeaponGO.transform.parent.name = BothStoppedWeaponBoneName;
                }

                if (forceNormalLocation)
                {
                    RealWeaponGO.transform.position = NormalLocation.transform.position;
                    RealWeaponGO.transform.rotation = NormalLocation.transform.rotation;

                    if (useBothWeapons)
                    {
                        BothWeaponGO.transform.position = NormalLocation.transform.position;
                        BothWeaponGO.transform.rotation = NormalLocation.transform.rotation;
                    }
                }
            }


        } else
        {

            if (weaponStopped)
            {
                RealWeaponGO.transform.parent.name = "a";

                RealWeaponGO.transform.position = (SpineGO.transform.position - FaceStoppedWeaponPos);
                RealWeaponGO.transform.localEulerAngles = FaceStoppedWeaponRot;

                if (useBothWeapons)
                {

                    BothWeaponGO.transform.parent.name = "a";

                    if (useBothWeaponsTransform)
                    {
                        BothWeaponGO.transform.position = (SpineGO.transform.position - FaceBothStoppedWeaponPos);
                        BothWeaponGO.transform.localEulerAngles = FaceBothStoppedWeaponRot;
                    }
                    else
                    {
                        BothWeaponGO.transform.position = (SpineGO.transform.position - FaceStoppedWeaponPos);
                        BothWeaponGO.transform.localEulerAngles = FaceStoppedWeaponRot;
                    }
                }
            }
            else
            {
                RealWeaponGO.transform.parent.name = StoppedWeaponBoneName;

                if (useBothWeapons)
                {
                    BothWeaponGO.transform.parent.name = BothStoppedWeaponBoneName;
                }

                if (forceNormalLocation)
                {
                    RealWeaponGO.transform.position = NormalLocation.transform.position;
                    RealWeaponGO.transform.rotation = NormalLocation.transform.rotation;

                    if (useBothWeapons)
                    {
                        BothWeaponGO.transform.position = NormalLocation.transform.position;
                        BothWeaponGO.transform.rotation = NormalLocation.transform.rotation;
                    }
                }
            }
        }
    }

    void Awake()
    {
        isAnimating = false;
        anim = GetComponent<Animator>();
    }

    void EnableAnimating()
    {
        isAnimating = true;
    }

    void EnableWeapon()
    {

        weaponStopped = !weaponStopped;
        Invoke("UpdateAnim", timeEventEnds);
    }

    void DisableWeapon()
    {
        weaponStopped = !weaponStopped;
        Invoke("UpdateAnim", timeEventEnds);
    }

    void UpdateAnim()
    {
        gameObject.GetComponent<Animator>().Rebind();
        gameObject.GetComponent<Animator>().Play("Equip", 0, timeEventStarts+ 0.3f);
    }

    void DisableAnimating()
    {
        isAnimating = false;
    }

    public void Walk(bool move, bool backwards)
    {
        if (canWalkBackwards)
        {
            if (!backwards)
            {
                anim.SetBool(AnimationTags.MOVEMENT, move);
                anim.SetBool(AnimationTags.MOVE_BACKWARDS, backwards);
            }
            else
            {
                anim.SetBool(AnimationTags.MOVEMENT, move);
                anim.SetBool(AnimationTags.MOVE_BACKWARDS, backwards);

            }
        } else
        {
            anim.SetBool(AnimationTags.MOVEMENT, move);
        }
    }

    public void Kick_1()
    {
        if (isAnimating) return;
        anim.SetTrigger(AnimationTags.KICK_1_TRIGGER);
    }

    public void Kick_2()
    {
        if (isAnimating) return;
        anim.SetTrigger(AnimationTags.KICK_2_TRIGGER);
    }

    public void Punch_1()
    {
        if (isAnimating) return;
        anim.SetTrigger(AnimationTags.PUNCH_1_TRIGGER);
    }

    public void Punch_2()
    {
        if (isAnimating) return;
        anim.SetTrigger(AnimationTags.PUNCH_2_TRIGGER);
    }

    public void Attack()
    {
        if (!weaponStopped)
        {
            if (isAnimating) return;
            anim.SetTrigger(AnimationTags.COMBO_TRIGGER);
        }
    }

    public void Throw()
    {
        if (!weaponStopped)
        {
            if (isAnimating) return;
            anim.SetTrigger(AnimationTags.THROW_TRIGGER);
        }
    }

    public void Death()
    {
        anim.SetTrigger(AnimationTags.DEATH_TRIGGER);
    }

    public void KnockDown()
    {
        anim.SetTrigger(AnimationTags.KNOCK_DOWN_TRIGGER);
    }

    public void StandUp()
    {
        anim.SetTrigger(AnimationTags.STAND_UP_TRIGGER);
    }

    public void Hit()
    {
        anim.SetTrigger(AnimationTags.HIT_TRIGGER);
    }

    public void Jump()
    {
        anim.SetTrigger(AnimationTags.JUMP_TRIGGER);
    }

    public void Play_IdleAnimation()
    {
        anim.Play(AnimationTags.IDLE_ANIMATION);
    }

    public void AIAttack(int attack)
    {
        switch (attack)
        {
            case 0:
                anim.SetTrigger(AnimationTags.KICK_1_TRIGGER); break;
            case 1:
                anim.SetTrigger(AnimationTags.KICK_2_TRIGGER); break;
            case 2:
                anim.SetTrigger(AnimationTags.PUNCH_1_TRIGGER); break;
            case 3:
                anim.SetTrigger(AnimationTags.PUNCH_2_TRIGGER); break;
            case 4:
                anim.SetTrigger(AnimationTags.COMBO_TRIGGER); break;
            case 5:
                anim.SetTrigger(AnimationTags.PUNCH_2_TRIGGER); break;
            case 6:
                anim.SetTrigger(AnimationTags.PUNCH_1_TRIGGER); break;
            case 7:
                anim.SetTrigger(AnimationTags.THROW_TRIGGER);
                break;
        }
    }

    public void Equip(bool state)
    {
        if (state)
        {
            if (isAnimating) return;
            anim.SetTrigger(AnimationTags.EQUIP_TRIGGER);
        }else {
            if (isAnimating) return;
            anim.SetTrigger(AnimationTags.UNEQUIP_TRIGGER);
        }
    }
}
