using UnityEngine;
using System.Collections;

/*
 * Slime Legacy
 */

public class Slime : Monster
{
	void Start ()
	{
		Initialize ();
	}

	void FixedUpdate ()
	{
		Move ();
	}

	override protected void Attack (Collider enemy)
	{
		Debug.Log ("----- slime:attack");

		PlayerMovement player = enemy.GetComponent<PlayerMovement> ();

		if (player != null) {
			player.TakeDamage (attack_power);
			return;
		}

		Spirit spirit = enemy.GetComponent<Spirit> ();

		if (spirit != null) {
			spirit.TakeDamage (attack_power);
			return;
		}
	}
}
