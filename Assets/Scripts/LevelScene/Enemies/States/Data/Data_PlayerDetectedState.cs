using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State_Data/Player_Detected_State")]
public class Data_PlayerDetectedState : ScriptableObject
{
    public float longRangeActionTime = 1.5f;
    public float shortRangeActionTime = 0.75f;
}
