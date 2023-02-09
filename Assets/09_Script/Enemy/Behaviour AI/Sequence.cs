//*********************************************************
// SUCCESS��Ԃ����玟�̃m�[�h�֐i�ށAFAILURE�Ȃ�ȍ~�͓ǂ܂Ȃ�
//*********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    protected List<Node> nodes = new List<Node>();

    // �I������s�����e��������Node���i�[
    public Sequence(List<Node> _nodes)
    {
        this.nodes = _nodes;
    }

    // NodeState�i�s���󋵁j��Ԃ�
    public override NodeState Evaluate()
    {
        bool isAnyNodeRunning = false;

        foreach(var node in nodes)
        {
            // �i�[����Node����]��
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;

                case NodeState.SUCCESS: // false�̂܂܏o��
                    break;

                case NodeState.FAILURE: // ���s�����Ȃ炷���o��
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;

                default:
                    break;
            }
        }

        // isAnyNodeRunning ��True��false��State���ω�
        _nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return _nodeState;
    }
}
