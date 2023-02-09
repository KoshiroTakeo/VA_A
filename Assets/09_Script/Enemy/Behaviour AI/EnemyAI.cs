//*********************************************************
// Behiviour Tree���g�����GAI
//*********************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;    // ����HP
    [SerializeField] private float lowHealthThreshold;// �ޔ����J�n����HP
    [SerializeField] private float healthRestoreRate; // 

    [SerializeField] private float chasingRange;      // �ǐՔ͈�
    [SerializeField] private float shootingRange;     // �U���͈�

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Cover[] avaliableCovers;

    private Material material;
    private Transform bestCoverSpot;
    private NavMeshAgent agent;

    private Node topNode;

    private float _currentHealth;

    [SerializeField]
    public float currentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        // ����HP�ɍX�V
        _currentHealth = startingHealth;
        
        // Tree�̌`��
        ConstructBehaviourTree();
    }

    // �c���[�`��
    private void ConstructBehaviourTree()
    {
        // �e�s����Node�����[�t���琶��
        IsCoverAvaliableNode coverAvaliableNode = new IsCoverAvaliableNode(avaliableCovers, playerTransform, this); // �����ꏊ�֓���
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this); // ������
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold); // HP�m�F
        IsCovereNode isCovereNode = new IsCovereNode(playerTransform, transform); // ����
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this); // �ǐՍs��
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform); // �ǐՔ͈͊m�F�s��
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform); // �U���͈͊m�F�s��
        ShootNode shootNode = new ShootNode(agent, this); // �U���s��

        // 
        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode }); // �ǐՒ�����/�ǐՒ�
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode }); // �U��������/�U����


        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode}); // �����ꏊ���Ȃ��Ȃ�A����Node��
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence}); // Sequence���ʂ����s�Ȃ�A����Sequence��
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCovereNode, findCoverSelector }); // �v���C���[��������A����Selector��
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector }); // HP���Ⴂ�Ȃ�A����Selector��

        topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence }); // ������/�U��/�ǐ� �����珇�ɗD�悳��A���������B�eSequencer�Ŏ��s��Ԃ����玟��Node��ǂ�

    }



    private void Update()
    {
        // �󋵔��f���A�s�����s
        topNode.Evaluate();


        if(topNode.nodeState == NodeState.FAILURE)
        {
            SetColor(Color.white);
        }

        

        currentHealth += Time.deltaTime * healthRestoreRate;
    }

    public void Damage()
    {
        Debug.Log("Damage");
        currentHealth -= 10f;
    }

    public void SetColor(Color _color)
    {
        
        material.color = _color;
    }

    // �ł��߂������ꏊ���L��
    public void SetBestCover(Transform bestSpot)
    {
        this.bestCoverSpot = bestSpot;
    }

    // �ł��߂������ꏊ���o��
    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }
}
