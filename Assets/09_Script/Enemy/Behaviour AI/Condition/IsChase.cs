//===============================================
// プレイヤーを追っているか
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsChase : Node
{
    public override NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }
}
