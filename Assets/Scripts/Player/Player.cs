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

	/*
	 * Actions
	 */

	private PlayerCalling myPlayerCalling;

	private PlayerMovement myPlayerMovement;

	/*
	 * Methods
	 */

	void Start ()
	{
		myPlayerCalling = GetComponent<PlayerCalling> ();
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

	//	public Transform GetObjetive ()
	//	{
	//		return myPlayerCalling.Objetive;
	//	}

	public Transform Objetive {
		get {
			return myPlayerCalling.Objetive;
		}
	}

	public bool IsCalling {
		get {
			return myPlayerCalling.IsCalling;
		}
	}

	//	public bool IsCalling ()
	//	{
	//		return myPlayerCalling.IsCalling;
	//	}
}
