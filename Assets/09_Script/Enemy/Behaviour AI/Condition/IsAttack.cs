//===============================================
// プレイヤーを攻撃できるか
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttack : Node
{
    public override NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }
}
