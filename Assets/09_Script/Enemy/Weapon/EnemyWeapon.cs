//============================================================
// EnemyWeapon.cs
//======================================================================
// �J������
//
// 2022/12/11 �G�̕���A�U���B�v���C���[�̃E�F�|���f�[�^�𗬗p
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // �����
    public enum WeaponStyle
    {
        GUN = 0,
        SWORD = 1,
        SHIRD = 2,
        FREE = 3,
        MAX = 4
    };
    public WeaponStyle Style;

    [SerializeField] WeaponData weaponData; // ����f�[�^

    // �K�v�X�e�[�^�X
    [SerializeField] protected float fAttack; // �З�
    [SerializeField] protected float fSpeed;  // �e���A�g�̋���
    [SerializeField] protected float fRecast_Primaly, fRecast_Secondly; //�����[�h����
    [SerializeField] protected bool bStanby_Primaly, bStanby_Secondly;  //�X�L����������

    public GameObject PrimaryBullet;
    protected GameObject SecondlyBullet;

    [SerializeField] protected WaitForSeconds WTS_SetTime_Primaly, WTS_SetTime_Secondly;



    public GameObject InstantWeapon(Transform _handPos)
    {
        // �v���n�u�𒼐ځA�e��ς���̂͏o���Ȃ�����
        GameObject instantWeapon = Instantiate(this.gameObject);
        instantWeapon.transform.parent = _handPos.transform;
        instantWeapon.transform.position = _handPos.transform.position;

        SetParameter();

        return instantWeapon;
    }

    // ����f�[�^���f
    public void SetParameter()
    {

        Style = (WeaponStyle)weaponData.Style;
        fAttack = weaponData.fAttack;
        fSpeed = weaponData.fBulletSpeed;
        fRecast_Primaly = weaponData.fRecast_Primaly;
        fRecast_Secondly = weaponData.fRecast_Secondly;
        bStanby_Primaly = bStanby_Secondly = false;
        WTS_SetTime_Primaly = new WaitForSeconds(fRecast_Primaly);
        WTS_SetTime_Secondly = new WaitForSeconds(fRecast_Secondly);

        if (!(weaponData.PrimaryBullet == null)) PrimaryBullet = weaponData.PrimaryBullet;
        if (!(weaponData.SecondlyBullet == null)) SecondlyBullet = weaponData.SecondlyBullet;

        Debug.Log(this.gameObject.name + "�Z�b�g");
    }


    protected IEnumerator Recasttime_Primaly(float _recasttime)
    {
        WaitForSeconds set = new WaitForSeconds(_recasttime);
        bStanby_Primaly = true;
        yield return set;
        bStanby_Primaly = false;
        Debug.Log("�g�p��");
    }

    protected IEnumerator Recasttime_Secondly(float _recasttime)
    {
        WaitForSeconds set = new WaitForSeconds(_recasttime);
        bStanby_Secondly = true;
        yield return set;
        bStanby_Secondly = false;
        Debug.Log("�g�p��");
    }

   
}
