using Unity.Entities;
using UnityEngine;

// 负责移动
public class MoveSystem : ComponentSystem {
    public struct Group
    {
        public ComponentArray<Transform> Transform;
        public ComponentDataArray<MoveInput> MoveInput;

        public int Length;
    }

    // 官方比较推荐这种注释自动注入的方法
    [Inject] private Group m_Group;

    protected override void OnUpdate()
    {
        for (int i = 0; i != m_Group.Length; i++)
        {
            PositionMove(i);
            Rotate(i);
        }
    }

    private void Rotate(int i)
    {
        var rotation = m_Group.Transform[i].rotation;

        rotation = rotation * m_Group.MoveInput[i].Rotate;

        m_Group.Transform[i].rotation = rotation;
    }

    private void PositionMove(int i)
    {
        var tansform = m_Group.Transform[i];
        var position = m_Group.Transform[i].position;

        // 前进 后退
        position += tansform.forward * m_Group.MoveInput[i].Move.z * Time.deltaTime;

        // 上升 下降
        position += 2 * Vector3.Scale(m_Group.MoveInput[i].Move, new Vector3(0, 1, 0)) * Time.deltaTime;

        m_Group.Transform[i].position = position;
    }
}
