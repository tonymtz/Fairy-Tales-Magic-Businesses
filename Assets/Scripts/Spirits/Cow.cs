using UnityEngine;
using System.Collections;

public class Cow : SpiritGenerator
{
	void Start ()
	{
		Initialize ();
	}

	void FixedUpdate ()
	{
		Move ();
	}
}
