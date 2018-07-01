using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

// 负责移动输入
public class MoveInputSystem : ComponentSystem {

    public struct Group
    {
        public ComponentDataArray<MoveInput> MoveInput;

        public int Length;
    }

    // 官方比较推荐这种注释自动注入的方法
    [Inject] private Group m_Group;

    protected override void OnUpdate()
    {
        for (int i = 0; i != m_Group.Length; i++)
        {
            var moveInput = m_Group.MoveInput[i];

            if (Input.GetKey(KeyCode.W))
            {
                moveInput.Move.z = -1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveInput.Move.z = 1;
            }
            else
            {
                moveInput.Move.z = 0;
            }
            if (Input.GetKeyUp(KeyCode.W)){
                moveInput.Move.z = -2;
            }else if( Input.GetKeyUp(KeyCode.S))
            {
                moveInput.Move.z = 2;
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveInput.Move.y = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveInput.Move.y = -1;
            }
            else
            {
                moveInput.Move.y = 0;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                moveInput.Move.x = 1;
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                moveInput.Move.x = -1;
            }
            else
            {
                moveInput.Move.x = 0;
            }

            m_Group.MoveInput[i] = moveInput;
        }
    }
}
