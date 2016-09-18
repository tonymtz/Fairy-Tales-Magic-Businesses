using UnityEngine;
using System.Collections;

public class DynamicText : MonoBehaviour
{
	[SerializeField]
	private float ttl = 1f;

	[SerializeField]
	private float speed = 1f;

	private RectTransform myRectTransform;

	private TextMesh myText;

	void Awake ()
	{
		myRectTransform = GetComponent<RectTransform> ();
		myText = GetComponent<TextMesh> ();
	}

	void Start ()
	{
		Destroy (this.gameObject, ttl);
	}

	private void Update ()
	{
		Vector3 previous = myRectTransform.position;

		myRectTransform.position = new Vector3 (
			previous.x,
			previous.y + speed * Time.deltaTime,
			previous.z
		);
	}

	public DynamicText SetText (string m)
	{
		myText.text = m;
		return this;
	}

	public DynamicText SetColor (float r, float g, float b)
	{
		myText.color = new Color (r, g, b);
		return this;
	}
}
