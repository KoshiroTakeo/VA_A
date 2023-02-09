using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCovereNode : Node
{
    private Transform target;
    private Transform origin;

    public IsCovereNode(Transform _target, Transform _origin)
    {
        this.target = _target;
        this.origin = _origin;
    }

    public override NodeState Evaluate()
    {
        RaycastHit hit;

        if(Physics.Raycast(origin.position, target.position - origin.position, out hit))
        {
            // �W�I�i�v���C���[�j�ȊO
            if(hit.collider.transform != target)
            {
                Debug.Log("IsCovereNode / �^�[�Q�b�g���瓦���Ă���");
                return NodeState.SUCCESS;
            }
        }

        Debug.Log("IsCovereNode / �^�[�Q�b�g�����A������");
        return NodeState.FAILURE;
    }
}
