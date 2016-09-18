using UnityEngine;
using System.Collections;

public abstract class SpiritGenerator : Spirit
{
	[SerializeField]
	protected float castingRadius = 3f;

	[SerializeField]
	protected SphereCollider energyRegenerator;

	[SerializeField]
	private float energyRestoredByTime = 1f;

	[SerializeField]
	private float restoreCooldown = 0.5f;

	private float timeout;

	private bool isRestoring;

	override protected void Initialize ()
	{
		base.Initialize ();
		energyRegenerator.radius = castingRadius;
	}

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

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.tag != "Player") {
			return;
		}

		isRestoring = true;

		Player player = other.GetComponent<Player> ();
		player.StartEnergyRegeneration ();
	}

	void OnTriggerExit (Collider other)
	{
		if (other.transform.tag != "Player") {
			return;
		}

		isRestoring = false;

		Player player = other.GetComponent<Player> ();
		player.StopEnergyRegeneration ();
	}

	void OnTriggerStay (Collider other)
	{
		if (other.transform.tag != "Player" || !isRestoring) {
			return;
		}

		timeout -= Time.deltaTime;

		if (timeout < 0) {
			Player player = other.GetComponent<Player> ();
			player.AddEnergy (energyRestoredByTime);
			timeout = restoreCooldown;
		}
	}
}
