using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR.Enemys
{
    public class TurretEnemy : MasterEnemy
{
        //List<GameObject> MazzleHead = new List<GameObject>();
        [SerializeField] GameObject MazzleHead;
        [SerializeField] Transform Mazzle;

        // �e��ށF���e�A����
        GameObject SpawnBullet;

        [SerializeField] private bool bApproach_Player;
        Vector3 oldPlayerPos;

        private void Start()
        {
            SetUP();

            SpawnBullet = Parameter.Bullet;

            oldPlayerPos = PlayerPos.transform.position;

            Move.bApproach_To_Player = bApproach_Player;
        }

        private void FixedUpdate()
        {
            // 
            Move.StateCheck(CurrentState.name);
            Motion.StateCheck(CurrentState.name);
        }

        private void Update()
        {
            // �ǂ̏󋵂ł��邩���f
            CurrentState = CurrentState.Process();

            AimToTarget();

        }

        // �^���b�g�p
        public void AimToTarget()
        {
            oldPlayerPos = Vector3.MoveTowards(oldPlayerPos, PlayerPos.position, Time.deltaTime * Parameter.fATK_RotSpeed );

            MazzleHead.transform.LookAt(oldPlayerPos);

        }


        public void Shot()
        {
            GameObject spawnedBullet = Instantiate(SpawnBullet, Mazzle.position, Mazzle.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = Parameter.fBulletSpeed * Mazzle.forward;
            spawnedBullet.GetComponent<Bullet>().fBulletDamage = Parameter.nAttack;
            spawnedBullet.GetComponent<Bullet>().tag = Bullet.OWNER.ENEMY;
            Destroy(spawnedBullet, 2);
        }
    }
}
