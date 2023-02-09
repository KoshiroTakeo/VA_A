using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR.Enemys
{
    public class EnemyParameter : MonoBehaviour
    {
        // �e�p�����[�^
        public int nHP = 1000;
        int nMaxHP;
        public int nAttack = 100;
        public float fSpeed = 1;
        public float fBulletSpeed = 1;
        public float fATK_RotSpeed = 10;

        public float DangerLine = 0;
        public float SeafLine = 0;


        // ����
        public GameObject PrimaryWeapon;
        public GameObject SecondaryWeapon;

        // �e
        public GameObject Bullet;

        public EnemyParameter(EnemyData _data)
        {
            nMaxHP = nHP = _data.nLife;
            nAttack = _data.nAttack;
            fSpeed = _data.fSpeed;
            fBulletSpeed = _data.fBulletSpeed;
            fATK_RotSpeed = _data.Atk_Rotation;

            DangerLine = _data.nLife * _data.Dangervalue;
            SeafLine = _data.nLife * _data.SeafValue;

            Bullet = _data.Bullet;

        }

        public float CurrentHPValue()
        {
            float value;
            value = (float)nHP / (float)nMaxHP;

            return value;
        }
    }
}
