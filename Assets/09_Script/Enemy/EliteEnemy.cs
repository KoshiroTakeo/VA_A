using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace VR.Enemys
{
    public class EliteEnemy : MasterEnemy
    {
        // �U��̃o�����X�l
        public int evaluteValue { get; set; }

        private Node topNode;

        // �ړI�n
        public Vector3 path;
        public GameObject racerobj;
        
        

        private void Start()
        {
            SetUP();
            path = PlayerPos.transform.position;
            Tree();

            
        }

        private void Tree()
        {
            OfenceAction ofenceAction = new OfenceAction(this.gameObject, racerobj.transform, Parameter.fSpeed);
            DefendAction defendAction = new DefendAction();

            IsDangerous isDangerous = new IsDangerous(Parameter, 0.5f); // HP��������Ă��邩
            IshavePath ishavePath = new IshavePath(this, PlayerPos, racerobj.transform, Parameter.fSpeed);

            
            Sequence goaltoPath = new Sequence(new List<Node> {ishavePath, ofenceAction });
            Sequence checkTactics = new Sequence(new List<Node> {isDangerous, defendAction});

            topNode = new Selector(new List<Node> {checkTactics,goaltoPath});
        }

        private void FixedUpdate()
        {
            // �󋵔��f���A�s�����s
            topNode.Evaluate();

            //CurrentState = CurrentState.Process();
        }
        
       
    }

}
