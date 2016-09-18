using UnityEngine;
using System.Collections;

/*
 * Monster Legacy
 */

public abstract class Monster : MonoBehaviour
{
	[SerializeField]
	protected float attack_power = 1f;

	[SerializeField]
	protected float attack_cooldown = 1f;

	[SerializeField]
	protected float defense = 1f;

	[SerializeField]
	protected float movement_speed = 1f;

	[SerializeField]
	protected float movement_cooldown = 1f;

	[SerializeField]
	protected float attackDistance = 1f;

	[SerializeField]
	protected float hp_current = 1f;

	[SerializeField]
	protected float hp_max = 1f;

	[SerializeField]
	protected Transform objetive;

	protected Rigidbody myRigidBody;

	protected Transform myTransform;

	protected bool isMoving = true;

	protected bool isAttacking = false;

	protected float timeLeft = 0f;

	protected void Initialize ()
	{
		myRigidBody = GetComponent<Rigidbody> ();
		myTransform = GetComponent<Transform> ();
	}

	protected void Move ()
	{
		if (!isMoving || isAttacking) {
			return;
		}

		if (objetive == null) {
			myTransform.Translate (Vector3.left * movement_speed * Time.deltaTime);
			return;
		}

		myTransform.position = Vector3.MoveTowards (
			myTransform.position,
			objetive.position,
			movement_speed * Time.deltaTime
		);
	}

	virtual protected void Attack (Collider enemy)
	{
	}

	void Update ()
	{
		Collider enemyNearby = GetEnemyNearby ();

		isAttacking = enemyNearby != null;

		timeLeft -= Time.deltaTime;

		if (timeLeft < 0) {
			if (isAttacking) {
				Attack (enemyNearby);
			} else {
				isMoving = !isMoving;
			}
			timeLeft = movement_cooldown;
		}

		// debug
		Vector3 forward = transform.TransformDirection (Vector3.left) * attackDistance;
		Debug.DrawRay (transform.position, forward, Color.red);
	}

	Collider GetEnemyNearby ()
	{
		RaycastHit hit;

		if (Physics.Raycast (myTransform.position, Vector3.left, out hit, attackDistance)) {
			if (hit.transform.tag == "Spirit" || hit.transform.tag == "Player") {
				return hit.collider;
			}
		}

		return null;
	}
}
