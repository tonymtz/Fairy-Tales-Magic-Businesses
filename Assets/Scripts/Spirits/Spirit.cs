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

	protected Rigidbody myRigidBody;

	protected Transform myTransform;

	protected bool isMoving = false;

	protected float timeLeft = 0f;

	protected void Initialize ()
	{
		myRigidBody = GetComponent<Rigidbody> ();
		myTransform = GetComponent<Transform> ();
	}

	public void TakeDamage (float damage)
	{
		float totalDamage = Mathf.Ceil (damage * (100 / (100 + defense)));

		Debug.Log (" --- damage calculated:" + totalDamage);

		if (totalDamage > 0) {
			hp_current -= totalDamage;
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
		// empty
	}

	virtual protected void Move ()
	{
		throw new UnityException ("This should be overridden");
	}
}
