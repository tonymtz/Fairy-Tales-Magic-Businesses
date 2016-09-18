using UnityEngine;
using System.Collections;

public abstract class SpiritAttacker : Spirit
{
	[SerializeField]
	protected float attack_power = 1f;

	[SerializeField]
	protected float attack_cooldown = 1f;

	[SerializeField]
	protected float attackDistance = 1f;

	protected bool isAttacking = false;

	protected bool IsEnemyNearby ()
	{
		RaycastHit hit;

		if (Physics.Raycast (myTransform.position, Vector3.right, out hit, attackDistance)) {
			return hit.transform.tag == "Monster";
		} else
			return false;
	}

	protected void Update ()
	{
		if (IsEnemyNearby ()) {
			isAttacking = true;
		}

		timeLeft -= Time.deltaTime;

		if (timeLeft < 0) {
			if (isAttacking) {
				Attack ();
			} else {
				isMoving = !isMoving;
			}
			timeLeft = movement_cooldown;
		}

		// TODO - remove this [DEBUG]
		Vector3 forward = transform.TransformDirection (Vector3.right) * attackDistance;
		Debug.DrawRay (transform.position, forward, Color.green);
	}

	override protected void Move ()
	{
		if (!isMoving || isAttacking) {
			return;
		}

		GetComponent<Transform> ().Translate (Vector3.right * movement_speed * Time.deltaTime);
	}

	virtual protected void Attack ()
	{
		throw new UnityException ("This should be overridden");
	}
}
