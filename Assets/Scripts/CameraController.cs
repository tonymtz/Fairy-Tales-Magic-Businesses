using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private Transform target;

	[SerializeField]
	private float dampTime = 0.15f;

	private UnityEngine.Camera myCamera;

	private Vector3 velocity = Vector3.zero;

	void Start ()
	{
		myCamera = GetComponent<UnityEngine.Camera> ();
	}

	void Update ()
	{
		Vector3 point = myCamera.WorldToViewportPoint (target.position);
		Vector3 delta = target.position - myCamera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z));
		Vector3 destination = transform.position + delta;

		transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);
	}
}
