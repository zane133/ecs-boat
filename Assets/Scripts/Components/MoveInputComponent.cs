using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct MoveInput : IComponentData {
    // 移动大小
    public Vector3 Move;
    public Quaternion Rotate;
}

public class MoveInputComponent : ComponentDataWrapper<MoveInput> {}
