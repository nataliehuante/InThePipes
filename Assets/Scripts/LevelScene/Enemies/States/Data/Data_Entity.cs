using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity_Data/Base_Data")]
public class Data_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public float minAttackDistance = 3f;
    public float maxAttackDistance = 4f;
    public float biteDistance = 0.5f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
