using Unity.Entities;
using UnityEngine;

// 负责移动
public class MoveSystem : ComponentSystem {
    public struct Group
    {
        public ComponentArray<Transform> Transform;
        public ComponentDataArray<MoveInput> MoveInput;
        public ComponentDataArray<MoveStatus> MoveStatus;
        public int Length;
    }

    // 官方比较推荐这种注释自动注入的方法
    [Inject] private Group m_Group;

    private bool m_slowDownZ = false;

    protected override void OnUpdate()
    {
        for (int i = 0; i != m_Group.Length; i++)
        {


            var moveStatus = m_Group.MoveStatus[i];

            bool one = Mathf.Approximately(m_Group.MoveInput[i].Move.z, 1);
            bool negOne = Mathf.Approximately(m_Group.MoveInput[i].Move.z, -1);
            bool two = Mathf.Approximately(m_Group.MoveInput[i].Move.z, 2);
            bool negTwo = Mathf.Approximately(m_Group.MoveInput[i].Move.z, -2);

            if (one || negOne)
            {
                moveStatus.TimeZ += Time.deltaTime;
                m_slowDownZ = false;
            }
            else if (m_Group.MoveInput[i].Move.z == 0)
            {
                moveStatus.TimeZ = 0;
            }
            else if (two || negTwo) {
                m_slowDownZ = true;
            }

            if (m_slowDownZ) {
                if (moveStatus.ThrustZ > 0)
                {
                    moveStatus.ThrustZ -= 0.05f * moveStatus.Mass;
                    if (moveStatus.ThrustZ < 0)
                    {
                        moveStatus.ThrustZ = 0;
                    }
                }
                if (moveStatus.ThrustZ < 0)
                {
                    moveStatus.ThrustZ += 0.05f * moveStatus.Mass;
                    if (moveStatus.ThrustZ > 0)
                    {
                        moveStatus.ThrustZ = 0;
                    }
                }
            }


            var C = moveStatus.ResistanceZ;
            var m = moveStatus.Mass;
            var T = moveStatus.ThrustZ;

            moveStatus.ThrustZ += m_Group.MoveInput[i].Move.z * 150;

            moveStatus.VelocityZ = (T / C) -  Mathf.Pow( 2.71f, - (C/m)) * (T/C - moveStatus.VelocityZ);
            moveStatus.VelocityZ = Mathf.Clamp(moveStatus.VelocityZ, -20, 20); // 限制最大速度
            //Debug.Log(moveStatus.ThrustZ);

            m_Group.MoveStatus[i] = moveStatus;

            m_Group.Transform[i].position += m_Group.Transform[i].forward * moveStatus.VelocityZ * Time.deltaTime;
        }
    }

    
}
