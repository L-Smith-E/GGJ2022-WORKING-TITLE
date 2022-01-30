using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EnemyAnimationType
{
    IDLE,   //0
    WALK,   //1
    WALK_BACK,  //2
    JUMP,   //3
    JUMP_BACK,  //4
    JUMP_SIDE,  //5
    ATTACK, //6
    ATTACK_SIDE //7
}