//===============================================
// 攻撃がプレイヤーへどのくらい当たっているか
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGlib : Node
{
    public override NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }
}
