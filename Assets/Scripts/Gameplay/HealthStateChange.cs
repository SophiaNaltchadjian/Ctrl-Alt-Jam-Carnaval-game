using UnityEngine;

public struct HealthStateChange
{
    public int Current { get; set; }
    public int Max { get; set; }
    public int Delta { get; set; }
    public Vector3? OriginatorPosition { get; set; }
    public float KnockbackMultiplier { get; set; }
}