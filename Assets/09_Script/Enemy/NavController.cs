
using UnityEngine;
using UnityEngine.AI;
using VR.Enemys;

public class NavController : MonoBehaviour
{
    protected GameObject npc;
    protected NavMeshAgent agent;
    protected float fnpcSpeed;
    protected Transform PlayerPos;

    private int currentIndex = -1;
    private int nstateID = 0;
    private float fcorrection = 1.5f;

    public bool bApproach_To_Player = false;
    

    public NavController(GameObject _npc, NavMeshAgent _agent, EnemyData _enemyData, Transform _player)
    {
        npc = _npc;           
        agent = _agent;
        fnpcSpeed = _enemyData.fSpeed;
        PlayerPos = _player;
    }

    public void StateCheck(EnemyState.STATE _state)
    {
        if(!(nstateID == (int)_state))
        {
            switch (_state)
            {
                case EnemyState.STATE.IDLE:                                                                 
                    agent.isStopped = true;
                    break;

                case EnemyState.STATE.PATROL:
                    SetRoute();
                    agent.speed = fnpcSpeed;
                    agent.isStopped = false;
                    break;

                case EnemyState.STATE.PURSUE:
                    SetRoute();
                    agent.speed = fnpcSpeed * fcorrection;
                    agent.isStopped = false;
                    break;

                case EnemyState.STATE.RUNAWAY:
                    SetRoute();
                    agent.speed = fnpcSpeed;
                    agent.isStopped = false;
                    break;

                case EnemyState.STATE.ATTACK:
                    agent.isStopped = true;
                    break;

                case EnemyState.STATE.KNOCKOUT:
                    agent.isStopped = true;
                    break;

                case EnemyState.STATE.SLEEP:
                    agent.isStopped = true;
                    break;

                default:
                    break;
            }

            nstateID = (int)_state;
        }

        //�G�[�W�F���g�̈ʒu����ь��݂̌o�H�ł̖ڕW�n�_�̊Ԃ̋��� ================================
        if (agent.remainingDistance < 1 )                                                                   
        {
            if (currentIndex >= EnemyWayPoint.Singleton.Checkpoints.Count - 1) currentIndex = 0;
            else currentIndex++;

            agent.SetDestination(EnemyWayPoint.Singleton.Checkpoints[currentIndex].transform.position);
        }
        else if(_state == EnemyState.STATE.PURSUE || _state == EnemyState.STATE.ATTACK)
        {
            if(bApproach_To_Player == true)
            {
                agent.SetDestination(PlayerPos.position);
            }
            
        }
        //=========================================================================================
        
    }



    // ����n�_�����G =============================================================================
    void SetRoute()
    {
        float lastDist = Mathf.Infinity;                                                          //�ŏ��͋����𖳌��Ƃ���(���ׂẴ`�F�b�N�|�C���g��T�m���邽��)

        for (int i = 0; i < EnemyWayPoint.Singleton.Checkpoints.Count; i++)                     //�`�F�b�N�|�C���g�̐���������
        {
            GameObject thisWP = EnemyWayPoint.Singleton.Checkpoints[i];                         //�ړI�n��ǂݍ���
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position); //�ړI�n�Ǝ��g�̋���
            if (distance < lastDist)                                                              //���ǂ蒅���ĂȂ���΍s��
            {
                currentIndex = i - 1;
                lastDist = distance;                                                              //�����̋��������̖ړI�n�܂łƂ���
            }
        }
    }
    //=============================================================================================
}
