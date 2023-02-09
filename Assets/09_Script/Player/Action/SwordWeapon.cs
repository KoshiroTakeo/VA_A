using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordWeapon : WeaponBase,IWeaponAction
{

	[SerializeField] GameObject AttackPoint = null; // �����蔻��I�u�W�F�N�g
	SwordCollider Sword;

	public float fAddForce_Damage = 2.0f;

	


	// �����x���v�Z���镔�� =====================
	[Tooltip("�ړ����x�𑪒肷��t���[��")]
	private int velocityAverageFrames = 5;
	[Tooltip("�p�x�ω��𑪒肷��t���[��")]
	private int angularVelocityAverageFrames = 11;

	public bool estimateOnAwake = true;

	private Coroutine routine;
	private int sampleCount;
	[SerializeField] private Vector3[] velocitySamples; // �ۑ�����l��
	private Vector3[] angularVelocitySamples; // �ۑ�����l��
	[SerializeField] float fOnAttackVelocity = 3;

	//===========================================



	private void Start()
    {
		velocitySamples = new Vector3[velocityAverageFrames];
		angularVelocitySamples = new Vector3[angularVelocityAverageFrames];

		if (estimateOnAwake)
		{
			BeginEstimatingVelocity();
		}

		Sword = AttackPoint.GetComponent<SwordCollider>();
		Sword.SetSwordWeapon(this);
	}

    private void Update()
	{
		estimateOnAwake = this.gameObject.activeSelf;

		if (this.gameObject.activeSelf == true) SlashAttack();

	}

	void SlashAttack()
	{
		//Debug.Log(GetVelocityEstimate().y);
		if (GetVelocityEstimate().y > fOnAttackVelocity || GetVelocityEstimate().y < -fOnAttackVelocity)
		{
			AttackPoint.SetActive(true);
		}
		else if(GetVelocityEstimate().x > fOnAttackVelocity || GetVelocityEstimate().x < -fOnAttackVelocity)
        {
			AttackPoint.SetActive(true);
		}
		else if (GetVelocityEstimate().x > fOnAttackVelocity || GetVelocityEstimate().x < -fOnAttackVelocity)
        {
			AttackPoint.SetActive(true);
		}
		else
		{
			AttackPoint.SetActive(false);
		}
	}

	public string PrimalySkill()
	{
		string debugtext = "now charge Sword";

		if (bStanby_Primaly == true) return debugtext;
		debugtext = OnHaptic(0.3f, 0.4f);
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
		StartCoroutine(PowerUp());
	}

	IEnumerator PowerUp()
	{
		float oldDamage = Sword.fSwordDamage;
		Sword.fSwordDamage = Sword.fSwordDamage * fAddForce_Damage;
		GameObject particle = Instantiate(PrimalySkill_Particle, AttackPoint.transform);

		yield return fRecast_Primaly / 0.5;

		Destroy(particle);
		Sword.fSwordDamage = oldDamage;
	}

	void PhysicalUP()
	{
		Debug.Log("�g�̋���");
	}

	//IEnumerator StatusUp()
	//{
	//	float oldStatus = ;
		
	//	GameObject particle = Instantiate(SecondlySkill_Particle, AttackPoint.transform);

	//	yield return fRecast_Secondly / 0.5;

	//	Destroy(particle);
	//	Sword.fSwordDamage = oldStatus;
	//}

	//-------------------------------------------------
	public void BeginEstimatingVelocity()
	{
		FinishEstimatingVelocity();

		routine = StartCoroutine(EstimateVelocityCoroutine());
	}

	//-------------------------------------------------
	public void FinishEstimatingVelocity()
	{
		if (routine != null) // routine�̒��g������Ȃ�
		{
			StopCoroutine(routine); // �R���[�`�����~�߂�
			routine = null; // �Ȃ���
		}
	}


	//-------------------------------------------------
	public Vector3 GetVelocityEstimate()
	{
		// Compute average velocity
		Vector3 velocity = Vector3.zero;
		int velocitySampleCount = Mathf.Min(sampleCount, velocitySamples.Length);
		if (velocitySampleCount != 0)
		{
			for (int i = 0; i < velocitySampleCount; i++)
			{
				velocity += velocitySamples[i];

			}
			velocity *= (1.0f / velocitySampleCount);
		}

		return velocity;
	}


	//-------------------------------------------------
	public Vector3 GetAngularVelocityEstimate()
	{
		// Compute average angular velocity
		Vector3 angularVelocity = Vector3.zero;
		int angularVelocitySampleCount = Mathf.Min(sampleCount, angularVelocitySamples.Length);
		if (angularVelocitySampleCount != 0)
		{
			for (int i = 0; i < angularVelocitySampleCount; i++)
			{
				angularVelocity += angularVelocitySamples[i];
			}
			angularVelocity *= (1.0f / angularVelocitySampleCount);
		}

		return angularVelocity;
	}


	//-------------------------------------------------
	// �����x�`�F�b�N
	public Vector3 GetAccelerationEstimate()
	{
		Vector3 average = Vector3.zero;
		for (int i = 2 + sampleCount - velocitySamples.Length; i < sampleCount; i++)
		{
			if (i < 2)
				continue;

			int first = i - 2;
			int second = i - 1;

			Vector3 v1 = velocitySamples[first % velocitySamples.Length];
			Vector3 v2 = velocitySamples[second % velocitySamples.Length];
			average += v2 - v1;
		}
		average *= (1.0f / Time.deltaTime);
		return average;
	}

	//-------------------------------------------------
	private IEnumerator EstimateVelocityCoroutine()
	{
		sampleCount = 0;

		// ���؂�ɂ͑Ή����ĂȂ��H
		Vector3 previousPosition = transform.position;    // ���ݍ��W���擾
		Quaternion previousRotation = transform.rotation; // ���݊p�x���擾

		while (true) // �i���ɉ�
		{
			yield return new WaitForEndOfFrame();

			float velocityFactor = 1.0f / Time.deltaTime;

			int v = sampleCount % velocitySamples.Length;        // �ۑ����Ă���l
			int w = sampleCount % angularVelocitySamples.Length; // �ۑ����Ă���l
			sampleCount++;

			// Estimate linear velocity
			velocitySamples[v] = velocityFactor * (transform.position - previousPosition);

			// Estimate angular velocity
			Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

			float theta = 2.0f * Mathf.Acos(Mathf.Clamp(deltaRotation.w, -1.0f, 1.0f));
			if (theta > Mathf.PI)
			{
				theta -= 2.0f * Mathf.PI;
			}

			Vector3 angularVelocity = new Vector3(deltaRotation.x, deltaRotation.y, deltaRotation.z);
			if (angularVelocity.sqrMagnitude > 0.0f)
			{
				angularVelocity = theta * velocityFactor * angularVelocity.normalized;
			}

			angularVelocitySamples[w] = angularVelocity;

			previousPosition = transform.position;
			previousRotation = transform.rotation;
		}
	}

    


	
}

