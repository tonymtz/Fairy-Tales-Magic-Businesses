using UnityEngine;
using System.Collections;

public class CallingBar : MonoBehaviour
{
	[SerializeField]
	private Transform player;

	private Transform myTransform;

	private Vector3 originalSize;

	private float callingTimeout = 0f;

	private float callingTimeLeft = 0f;

	private void Awake ()
	{
		myTransform = GetComponent<Transform> ();
		originalSize = myTransform.localScale;
	}

	private void Start ()
	{
		myTransform.localScale = new Vector3 (originalSize.x, originalSize.y, originalSize.z);
	}

	private void FixedUpdate ()
	{
		callingTimeout = player.GetComponent<Player> ().CallingTimeout;
		callingTimeLeft = player.GetComponent<Player> ().CallingTimeLeft;
		updateFill ();
	}

	private void updateFill ()
	{
		float percentage = 100 * callingTimeLeft / callingTimeout;
		float newWidth = percentage * originalSize.x / 100;

		myTransform.localScale = new Vector3 (newWidth, originalSize.y, originalSize.z);
	}
}
