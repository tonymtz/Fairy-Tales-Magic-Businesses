using UnityEngine;
using System.Collections;

public class Turtle : SpiritAttacker
{
	void Start ()
	{
		Initialize ();
	}

	void FixedUpdate ()
	{
		Move ();
	}

	override protected void Attack ()
	{
		Debug.Log ("----- turtle:attack");
	}
}
