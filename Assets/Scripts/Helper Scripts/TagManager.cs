using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTags
{
    public const string MOVEMENT = "Movement";

    public const string PUNCH_1_TRIGGER = "Punch1";
    public const string PUNCH_2_TRIGGER = "Punch2";

    public const string KICK_1_TRIGGER = "Kick1";
    public const string KICK_2_TRIGGER = "Kick2";

    public const string COMBO_TRIGGER = "Combo";
    public const string THROW_TRIGGER = "Throw";

    public const string IDLE_ANIMATION = "Idle";

    public const string KNOCK_DOWN_TRIGGER = "KnockDown";
    public const string STAND_UP_TRIGGER = "StandUp";
    public const string HIT_TRIGGER = "Hit";
    public const string DEATH_TRIGGER = "Death";

    public const string JUMP_TRIGGER = "Jump";

    public const string EQUIP_TRIGGER = "Equip";
    public const string UNEQUIP_TRIGGER = "UnEquip";

    public const string MOVE_BACKWARDS = "Backwards";
}

public class Axis
{
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
}

public class Tags
{
    public const string GROUND_TAG = "Ground";
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";

    public const string UNTAGGED_TAG = "Untagged";
    public const string MAIN_CAMERA_TAG = "MainCamera";
}
