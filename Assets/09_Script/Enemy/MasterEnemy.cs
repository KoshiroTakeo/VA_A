//============================================================
// �V�[����̓G
//======================================================================
// �J������
// 20221107:�����󂯓n������
// 20221108:�p�����[�^�[�Ǘ�
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VR.Enemys
{
    public class MasterEnemy : MonoBehaviour, IDamageble<float>
    {
        // �K�v�R���|�[�l���g
        protected EnemyState CurrentState;
        // Behaviour�N���X��������
        protected NavMeshAgent NavMesh;
        protected NavController Move;
        protected Animator Anime;
        protected AnimationState Motion;
        protected ActionTrigger Trigger;

        // UI�֌W
        [SerializeField] protected GameObject BarPrefab;
        protected LifeBar BarStatus;

        // Enemy�̃p�����[�^�f�[�^
        [SerializeField] EnemyData Data;
        [SerializeField] Transform UIPopPos;
        protected EnemyParameter Parameter;

        // �v���C���[���
        protected Transform PlayerPos;


        protected string SetUP()
        {
            PlayerPos = GameObject.FindWithTag("Player").transform;
            NavMesh = GetComponent<NavMeshAgent>();
            Anime = GetComponent<Animator>();

            Parameter = new EnemyParameter(Data);
            CurrentState = new Idle(this.gameObject,PlayerPos, Data);
            Move = new NavController(this.gameObject, NavMesh, Data, PlayerPos);
            Motion = new AnimationState(Anime);
                                  

            // �A�j���[�V�����C�x���g�ŏE����悤�ɃR���|�[�l���g
            //Trigger = this.gameObject.AddComponent<ActionTrigger>();
            //Trigger = new AttackTrigger();

            // HP�o�[�̏����擾
            BarPrefab = Instantiate(BarPrefab, UIPopPos);
            BarStatus = BarPrefab.GetComponent<LifeBar>();
            BarStatus.SetLifeBar(this.gameObject, Parameter.nHP);

            return this.gameObject.name + "[SetUp Complete]";
        }

        

        public void AddDamage(float _damage)
        {
            Parameter.nHP = Parameter.nHP - (int)_damage;
            BarStatus.UpdataLife(Parameter.nHP);
        }

        public void AnimCall_Shot()
        {
            Debug.Log("�e�۔���");
        }

        public void AnimCall_Attack()
        {
            Debug.Log("���e�U��");
        }
    }
}
