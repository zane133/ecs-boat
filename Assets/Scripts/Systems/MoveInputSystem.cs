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
            PositionMove(i);
            Rotate(i);
        }
    }

    private void Rotate(int i)
    {
        var moveInput = m_Group.MoveInput[i];

        moveInput.Rotate = Quaternion.Euler(0f, Input.GetAxis("Horizontal"), 0f);

        m_Group.MoveInput[i] = moveInput;
    }

    private void PositionMove(int i)
    {
        var moveInput = m_Group.MoveInput[i];

        //moveInput.Move.x = Input.GetAxis("Horizontal");
        moveInput.Move.z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Q))
        {
            moveInput.Move.y = 1;
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            moveInput.Move.y = -1;
        }
        else
        {
            moveInput.Move.y = 0;
        }

        m_Group.MoveInput[i] = moveInput;
    }
}
