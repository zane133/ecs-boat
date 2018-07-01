using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct Initialize : IComponentData
{
}

public class InitializeComponent : ComponentDataWrapper<Initialize> { }
