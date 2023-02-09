using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordWeapon : WeaponBase,IWeaponAction
{

	[SerializeField] GameObject AttackPoint = null; // あたり判定オブジェクト
	SwordCollider Sword;

	public float fAddForce_Damage = 2.0f;

	


	// 加速度を計算する部分 =====================
	[Tooltip("移動速度を測定するフレーム")]
	private int velocityAverageFrames = 5;
	[Tooltip("角度変化を測定するフレーム")]
	private int angularVelocityAverageFrames = 11;

	public bool estimateOnAwake = true;

	private Coroutine routine;
	private int sampleCount;
	[SerializeField] private Vector3[] velocitySamples; // 保存する値数
	private Vector3[] angularVelocitySamples; // 保存する値数
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
		Debug.Log("斬撃強化");
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
		Debug.Log("身体強化");
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
		if (routine != null) // routineの中身があるなら
		{
			StopCoroutine(routine); // コルーチンを止める
			routine = null; // なくす
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
	// 加速度チェック
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

		// 横切りには対応してない？
		Vector3 previousPosition = transform.position;    // 現在座標を取得
		Quaternion previousRotation = transform.rotation; // 現在角度を取得

		while (true) // 永遠に回す
		{
			yield return new WaitForEndOfFrame();

			float velocityFactor = 1.0f / Time.deltaTime;

			int v = sampleCount % velocitySamples.Length;        // 保存している値
			int w = sampleCount % angularVelocitySamples.Length; // 保存している値
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

