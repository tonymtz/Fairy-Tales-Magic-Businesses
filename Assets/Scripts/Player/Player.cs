using UnityEngine;
using System.Collections;

public enum SpiritType
{
	TURTLE,
	COW
}

public enum StoneType
{
	ROCK
}

public class Player : MonoBehaviour
{
	/*
	 * Equipment
	 */

	[Header ("Equipment Assigned")]

	[SerializeField]
	private StoneType stoneSlot;

	[SerializeField]
	private SpiritType generatorSlot;

	[SerializeField]
	private SpiritType attackerSlot;

	[SerializeField]
	private SpiritType optionalSlot;

	[Header ("Visual Effects")]

	[SerializeField]
	private Transform energyHalo;

	/*
	 * Actions
	 */

	private PlayerCalling myPlayerCalling;

	/*
	 * Methods
	 */

	void Awake ()
	{
		myPlayerCalling = GetComponent<PlayerCalling> ();
	}

	void Start ()
	{
	}

	public void UseStoneSlot ()
	{
		// ??
	}

	/*
	 * Energy & Calling Spirits
	 */

	public void UseAttackerSlot ()
	{
		myPlayerCalling.CallSpirit (attackerSlot);
	}

	public void UseOptionalSlot ()
	{
		myPlayerCalling.CallSpirit (optionalSlot);
	}

	public void UseGeneratorSlot ()
	{
		myPlayerCalling.CallSpirit (generatorSlot);
	}

	public void StartEnergyRegeneration ()
	{
		energyHalo.gameObject.SetActive (true);
	}

	public void StopEnergyRegeneration ()
	{
		energyHalo.gameObject.SetActive (false);
	}

	public void AddEnergy (float energyAdded)
	{
		myPlayerCalling.AddEnergy (energyAdded);
	}

	public Transform Target {
		get {
			return myPlayerCalling.Target;
		}
	}

	public bool IsCalling {
		get {
			return myPlayerCalling.IsCalling;
		}
	}

	public float Energy {
		get {
			return myPlayerCalling.Energy;
		}
	}

	public float EnergyMax {
		get {
			return myPlayerCalling.EnergyMax;
		}
	}

	public float CallingTimeout {
		get {
			return myPlayerCalling.CallingTimeout;
		}
	}

	public float CallingTimeLeft {
		get {
			return myPlayerCalling.CallingTimeLeft;
		}
	}
}
