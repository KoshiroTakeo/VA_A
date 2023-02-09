//============================================================
// EnemyData.cs
//======================================================================
// �J������
//
// 
// 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create EnemyData")]

public class EnemyData : ScriptableObject
{ 
    [Header("�G�̃X�e�[�^�X")]
    public int nLife = 10;
    public int nAttack = 10;
    public int nDefence = 10;
    public float fSpeed = 10;
    public float fBulletSpeed = 10;

    [Header("���m����")]
    public float visDist = 40.0f;            //���m����
    [Header("����p")]
    public float visAngle = 60.0f;            //���m�p
    [Header("�U������")]
    public float shootDist = 30.0f;             //�U������
    [Header("�w��p�x")]
    public float behideAngle = 20.0f;             //�w��p�x
    [Header("�w�㋗��")]
    public float behideDist = 10.0f;             //�w��p�x
    [Header("�U���Ԋu")]
    public float Atk_Interbal = 5.0f;             //�U���Ԋu
    [Header("�U���p�x�C�����x")]
    public float Atk_Rotation = 5.0f;             //�U���p�x�C�����x
    [Header("�ҋ@��Ԃ֖߂�p�x")]
    public int BreakFrequency = 1000;             //�U���p�x�C�����x


    [Header("��_���[�W�ւ̊֐S�l")]
    public float Dangervalue = 0.07f; // 5�b�Ԃ�HP����ꂽ��h��ɓ���
    [Header("�s���`�ɂȂ�HP��")]
    public float SeafValue = 0.3f; // �댯��ԂɂȂ�HP�c�ʁ�

    public enum EnemyType
    {
        Normal_Axe,
        Normal_Gunner,

        Turret_FixedBase,
        Turret_CycleBase,


        MAX
    }

    
    public GameObject Bullet;
}
