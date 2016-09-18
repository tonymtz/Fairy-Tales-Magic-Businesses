using UnityEngine;
using System.Collections;

/*
 * Callable Spirits:
 * [0] Turtle
 * [1] Cow
 */

public class PlayerCalling : MonoBehaviour
{
	[Header ("Basic Stats")]

	[SerializeField]
	private float energyMax;

	[SerializeField]
	private float energy;

	[Header ("Spirits")]

	[SerializeField]
	private Transform[] callableSpirits;

	[Header ("Visual Effects")]

	[SerializeField]
	private Transform target;

	[SerializeField]
	private Transform callingHalo;

	[SerializeField]
	private Transform callingBar;

	private Player player;

	private Transform spiritInProcess;

	private bool isCalling;

	private float callingTimeout;

	private float callingTimeLeft;

	void Start ()
	{
		player = GetComponent<Player> ();

		StopCalling ();
	}

	void Update ()
	{
		if (!isCalling) {
			return;
		}

		callingTimeLeft -= Time.deltaTime;

		if (callingTimeLeft < 0) {
			StopCalling ();
		}
	}

	private void StartCalling (Transform newSpirit)
	{
		spiritInProcess = newSpirit;
		callingTimeout = spiritInProcess.GetComponent<Spirit> ().CallingTimeout;
		callingTimeLeft = callingTimeout;

		callingHalo.gameObject.SetActive (true);
		target.gameObject.SetActive (true);
		callingBar.gameObject.SetActive (true);

		isCalling = true;
	}

	private void StopCalling ()
	{
		if (spiritInProcess != null) {
			// TODO - Stop using objetive's position.
			// - Why not?
			spiritInProcess.position = new Vector3 (
				target.position.x,
				0.0f,
				target.position.z
			);

			Spirit mySpirit = spiritInProcess.GetComponent<Spirit> ();

			mySpirit.GetComponent<Spirit> ().SpawnEffect ();

			AddEnergy (mySpirit.Magic_cost * -1);
		}

		callingHalo.gameObject.SetActive (false);
		target.gameObject.SetActive (false);
		callingBar.gameObject.SetActive (false);

		isCalling = false;
	}

	public void CallSpirit (SpiritType type)
	{
		// TODO - deprecate ugly array in favor of dictionary
		// see http://forum.unity3d.com/threads/finally-a-serializable-dictionary-for-unity-extracted-from-system-collections-generic.335797/

		Transform newSpirit;

		switch (type) {
		case SpiritType.COW:
			newSpirit = callableSpirits [1];
			break;
		case SpiritType.TURTLE:
		default:
			newSpirit = callableSpirits [0];
			break;
		}

		newSpirit = Instantiate (newSpirit);
		newSpirit.position = Vector3.down * 1000;

		float magicCost = newSpirit.GetComponent<Spirit> ().Magic_cost;

		if (energy < magicCost) {
			Destroy (newSpirit.gameObject);
		} else {
			StartCalling (newSpirit);
		}
	}

	public void AddEnergy (float energyAdded)
	{
		energy += energyAdded;

		if (energy > energyMax) {
			energy = energyMax;
		}
	}

	public float EnergyMax {
		get {
			return energyMax;
		}
	}

	public float Energy {
		get {
			return energy;
		}
	}

	public Transform Target {
		get {
			return target;
		}
	}

	public bool IsCalling {
		get {
			return isCalling;
		}
	}

	public float CallingTimeout {
		get {
			return callingTimeout;
		}
	}

	public float CallingTimeLeft {
		get {
			return callingTimeLeft;
		}
	}
}
