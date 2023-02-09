using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR.Enemys
{
    public abstract class EliteState 
    {
        public enum STATE
        {
            OFFENCE,
            DEFENCE,

            MAX
        };
        public STATE name;

        public enum EVENT
        {
            ENTER,  // �s���J�n
            UPDATA, // �s����
            EXIT    // �s���I��
        };
        protected EVENT stage;



        protected int value;
        protected EliteState nextEnemyState;

        public EliteState(int _value)
        {
            value = _value;

            stage = EVENT.ENTER;
        }



        public virtual void Enter()
        {
            stage = EVENT.UPDATA;
        }
        public virtual void Updata()
        {
            stage = EVENT.UPDATA;
        }
        public virtual void Exit()
        {
            stage = EVENT.EXIT;
        }

        public EliteState Process()
        {
            //state����ǂݎ��
            if (stage == EVENT.ENTER) Enter();  //Enter�̏������s��

            if (stage == EVENT.UPDATA) Updata(); //Updata�̏������s��

            if (stage == EVENT.EXIT)      //Exit�̏������s��
            {
                Exit();
                return nextEnemyState; //���̍s���Ɉڂ�
            }

            return this; // EnemyState��Ԃ�
        }



        // �U��̕]��
        public STATE EvaluationState()
        {
            return STATE.OFFENCE;
        }



        public void NextState(EliteState enemyState)
        {
            nextEnemyState = enemyState;
            stage = EVENT.EXIT;
        }
    }
}

