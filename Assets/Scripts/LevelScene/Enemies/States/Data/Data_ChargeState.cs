using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State_Data/Charge_State")]
public class Data_ChargeState : ScriptableObject
{
    public float chargeSpeed = 6f;

    public float chargeTime = 0.5f;
}
