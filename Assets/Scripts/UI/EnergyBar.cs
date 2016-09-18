using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour
{
	[SerializeField]
	private Transform fill;

	[SerializeField]
	private Player player;

	private float originalWidth;

	private RectTransform fillRT;

	void Start ()
	{
		fillRT = fill.GetComponent<RectTransform> ();
		originalWidth = fillRT.rect.width;
	}

	void Update ()
	{
		float energyMax = player.EnergyMax;
		float energyCurrent = player.Energy;

		float percentage = 100 * energyCurrent / energyMax;
		float newWidth = percentage * originalWidth / 100;

		fillRT.sizeDelta = new Vector2 (newWidth, fillRT.rect.height);
	}
}
