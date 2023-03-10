using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverNode : Node
{
    private NavMeshAgent agent;
    private EnemyAI ai;

    public GoToCoverNode(NavMeshAgent _agent, EnemyAI _ai)
    {
        this.agent = _agent;
        this.ai = _ai;
    }

    public override NodeState Evaluate()
    {
        Transform coverSpot = ai.GetBestCoverSpot();

        // 身を隠す所が無ければ失敗
        if (coverSpot == null)
        {
            Debug.Log("GoToCoverNode / 隠れる所がない");
            return NodeState.FAILURE;
        }

        ai.SetColor(Color.blue);
        float distance = Vector3.Distance(coverSpot.position, agent.transform.position);

        if(distance > 2.0f)
        {
            agent.isStopped = false;
            agent.SetDestination(coverSpot.position);

            Debug.Log("GoToCoverNode / 隠れる所へ出発");
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;

            Debug.Log("GoToCoverNode / 隠れる所へ到着");
            return NodeState.SUCCESS;
        }
    }
}
