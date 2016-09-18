using UnityEngine;
using System.Collections;

public abstract class Spirit : MonoBehaviour
{
	[SerializeField]
	protected float magic_cost = 1f;

	[SerializeField]
	protected float callingTimeout = 2f;

	[SerializeField]
	protected float defense = 1f;

	[SerializeField]
	protected float movement_speed = 1f;

	[SerializeField]
	protected float movement_cooldown = 1f;

	[SerializeField]
	protected float hp_current = 1f;

	[SerializeField]
	protected float hp_max = 1f;

	[SerializeField]
	private Transform spawnEffect;

	[SerializeField]
	private Transform killEffect;

	[SerializeField]
	private Transform dynamicText;

	protected Rigidbody myRigidBody;

	protected Transform myTransform;

	protected bool isMoving = true;

	protected float timeLeft = 0f;

	private Vector3 originalPosition;

	virtual protected void Initialize ()
	{
		myRigidBody = GetComponent<Rigidbody> ();
		myTransform = GetComponent<Transform> ();
	}

	public void TakeDamage (float damage)
	{
		float totalDamage = Mathf.Ceil (damage * (100 / (100 + defense)));

		Transform txt = (Transform)Instantiate (dynamicText, myTransform.position + Vector3.up / 2, Quaternion.identity, myTransform);
		txt.GetComponent<DynamicText> ().SetText ("-" + totalDamage);

		if (totalDamage > 0) {
			hp_current -= totalDamage;
		}

		if (hp_current <= 0) {
			Kill ();
		}
	}

	public float Magic_cost {
		get {
			return magic_cost;
		}
	}

	public float CallingTimeout {
		get {
			return callingTimeout;
		}
	}

	public void SpawnEffect ()
	{
		Instantiate (spawnEffect, myTransform, false);
	}

	public void Kill ()
	{
		Vector3 effectPosition = new Vector3 (
			myTransform.position.x,
			0.1f,
			myTransform.position.z
		);
		Instantiate (killEffect, effectPosition, Quaternion.identity);
		Destroy (gameObject);
	}

	abstract protected void Move ();
}
