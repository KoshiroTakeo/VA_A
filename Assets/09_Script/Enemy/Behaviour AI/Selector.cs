//*********************************************************
// FAILURE�Ȃ玟�̃m�[�h�֐i�ށASUCCESS�Ȃ�I��
//*********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    protected List<Node> nodes = new List<Node>();

    // �I������s�����e��������Node���i�[
    public Selector(List<Node> _nodes)
    {
        this.nodes = _nodes;
    }

    // NodeState�i�s���󋵁j��Ԃ�
    public override NodeState Evaluate()
    {
        // Node�����ׂĕ]��
        foreach (var node in nodes)
        {
            // �i�[����Node����]��
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;

                case NodeState.SUCCESS:
                    _nodeState = NodeState.SUCCESS;
                    return _nodeState;

                case NodeState.FAILURE:
                    break;

                default:
                    break;
            }
        }

        _nodeState = NodeState.FAILURE;
        return _nodeState;
    }
}
