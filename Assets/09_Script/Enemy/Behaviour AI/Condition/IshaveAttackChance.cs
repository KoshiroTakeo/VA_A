//===============================================
// 攻撃の機会があるか
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IshaveAttackChance : Node
{
    public override NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }
}
