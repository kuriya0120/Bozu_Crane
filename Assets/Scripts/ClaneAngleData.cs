using UnityEngine;

//ƒV[ƒ“ŠÔó‚¯“n‚µ‚ğ‚·‚é‚½‚ß‚ÉScriptableObject‚É
[CreateAssetMenu]
public class ClaneAngleData : ScriptableObject
{
    public float Arm_Angle;
    public CoordinateData Hook_Position;
    public CoordinateData Hammer_Position;
}

[System.Serializable]
public class CoordinateData
{
    public float position_y;
    public float rotation_z;
}
