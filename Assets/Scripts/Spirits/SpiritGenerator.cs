using UnityEngine;
using System.Collections;

public abstract class SpiritGenerator : Spirit
{
	[SerializeField]
	protected float castingRadius = 1f;

	[SerializeField]
	protected float castingDistance = 10f;

	protected void Update ()
	{
		timeLeft -= Time.deltaTime;

		if (timeLeft < 0) {
			isMoving = !isMoving;
			timeLeft = movement_cooldown;
		}
	}

	override protected void Move ()
	{
		if (!isMoving) {
			return;
		}

		GetComponent<Transform> ().Translate (Vector3.right * movement_speed * Time.deltaTime);
	}
}
