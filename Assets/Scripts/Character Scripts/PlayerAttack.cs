using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState {
    NONE,
    KICK_1,
    PUNCH_1,
    ALL,
}

public class PlayerAttack : MonoBehaviour
{
    public bool bAvailableThrow;
    public float defaultThrowingTime = 2f;
    private CharacterAnimation player_Anim;

    private ComboState current_Combo_State;

    private float throwingTime;

    public bool weaponEquipped = false;

    public int WeaponDamage = 6;

    // Start is called before the first frame update
    void Awake()
    {
        throwingTime = defaultThrowingTime;
        player_Anim = GetComponentInChildren<CharacterAnimation>();
    }

    void Start()
    {
        current_Combo_State = ComboState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        throwingTime -= Time.deltaTime;
        ComboAttacks();
    }

    void ComboAttacks()
    {
        if (Input.GetMouseButtonDown(0) && !player_Anim.IsAnimating)
        {
            current_Combo_State = (ComboState)((int)++current_Combo_State % (int)ComboState.ALL);

            if (current_Combo_State == ComboState.KICK_1)
            {
                player_Anim.Kick_1();
            }
            if (current_Combo_State == ComboState.PUNCH_1)
            {
                player_Anim.Punch_1();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            player_Anim.Kick_1();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            player_Anim.Kick_2();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            player_Anim.Punch_1();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player_Anim.Punch_2();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player_Anim.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponEquipped = !weaponEquipped;

            if(weaponEquipped)
            {
                player_Anim.Equip(true);
                WeaponDamage = 8;
            } else
            {
                player_Anim.Equip(false);
                WeaponDamage = 6;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            throwingTime = defaultThrowingTime;
            player_Anim.Throw();
        }
    }
}
