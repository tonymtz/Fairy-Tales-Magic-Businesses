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
	private float energy_max;

	[SerializeField]
	private float energy;

	[Header ("Spirits")]

	[SerializeField]
	private Transform[] callableSpirits;

	[Header ("Visual Effects")]

	[SerializeField]
	private Transform objetive;

	[SerializeField]
	private Transform callingHalo;

	private Transform spiritInProcess;

	private bool isCalling;

	private Player self;

	private float timeout;

	void Start ()
	{
		self = GetComponent<Player> ();

		StopCalling ();
	}

	void Update ()
	{
		if (!isCalling) {
			return;
		}

		timeout -= Time.deltaTime;

		if (timeout < 0) {
			StopCalling ();
		}
	}

	private void StartCalling (Transform newSpirit)
	{
		spiritInProcess = newSpirit;
		timeout = spiritInProcess.GetComponent<Spirit> ().CallingTimeout;

		callingHalo.gameObject.SetActive (true);
		objetive.gameObject.SetActive (true);

		isCalling = true;
	}

	private void StopCalling ()
	{
		if (spiritInProcess != null) {
			// TODO - Stop using objetive's position.
			// - Why not?
			spiritInProcess.position = new Vector3 (
				objetive.position.x,
				0.0f,
				objetive.position.z
			);

			Spirit mySpirit = spiritInProcess.GetComponent<Spirit> ();

			mySpirit.GetComponent<Spirit> ().SpawnEffect ();

			AddEnergy (mySpirit.Magic_cost * -1);
		}

		callingHalo.gameObject.SetActive (false);
		objetive.gameObject.SetActive (false);

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

		if (energy > energy_max) {
			energy = energy_max;
		}
	}

	public float Energy_max {
		get {
			return energy_max;
		}
	}

	public float Energy {
		get {
			return energy;
		}
	}

	public Transform Objetive {
		get {
			return objetive;
		}
	}

	public bool IsCalling {
		get {
			return isCalling;
		}
	}
}
