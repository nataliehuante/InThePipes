using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttackStateData", menuName = "Data/State_Data/Attack_State")]

public class Data_AttackState : ScriptableObject
{
    public float timeToAttackFor = 0.5f;
}
