using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct MoveStatus : IComponentData
{
    public float TimeZ;
    public float Mass;
    public float VelocityZ;
    public float VelocityX;
    public float ThrustZ;
    public float ThrustX;
    public float ResistanceZ; // 阻尼系数
    public float ResistanceX;
}

public class MoveStatusComponent : ComponentDataWrapper<MoveStatus> { }
