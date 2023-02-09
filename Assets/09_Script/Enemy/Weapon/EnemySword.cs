using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : EnemyWeapon, IWeaponAction
{
	
	[SerializeField] GameObject AttackPoint = null; // �����蔻��I�u�W�F�N�g



	private void Start()
	{
		
	}

	private void Update()
	{
		
	}

	void SlashAttack()
	{
		StartCoroutine("SlashColliderOn");
	}

	IEnumerator SlashColliderOn()
    {
		AttackPoint.SetActive(true);

		yield return WTS_SetTime_Primaly;

		AttackPoint.SetActive(false);
	}
	


	// �X�L�������ƃ��L���X�g
	public string PrimalySkill()
	{
		string debugtext = "now charge";

		if (bStanby_Primaly == true) return debugtext;
		debugtext = "Shot";
		StartCoroutine(Recasttime_Primaly(fRecast_Primaly));

		PowerAttack();

		return debugtext;
	}

	public void SecondalySkill()
	{
		if (bStanby_Secondly == true) return;
		StartCoroutine(Recasttime_Secondly(fRecast_Secondly));

		PhysicalUP();
	}
	void PowerAttack()
	{
		Debug.Log("�a������");
	}

	void PhysicalUP()
	{
		Debug.Log("�g�̋���");
	}
}
