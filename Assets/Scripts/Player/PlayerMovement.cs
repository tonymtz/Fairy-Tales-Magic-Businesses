using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	[Header ("Basic Stats")]

	[SerializeField]
	private float speed = 1f;

	[Header ("Control Variables")]

	[SerializeField]
	private bool facingRight = true;

	[SerializeField]
	private bool isStunned = false;

	[SerializeField]
	private float stunCooldown = 1f;

	private Player self;

	private Transform myTransform;

	private SpriteRenderer mySpriteRenderer;

	private float stunTimeout;

	void Start ()
	{
		self = GetComponent<Player> ();
		myTransform = GetComponent<Transform> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if (!isStunned) {
			return;
		}

		stunTimeout -= Time.deltaTime;

		if (stunTimeout < 0) {
			isStunned = false;
		}
	}

	void FixedUpdate ()
	{
		if (isStunned) {
			return;
		}

		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");

		float modifier = 1f;

		if (self.IsCalling) {
			modifier -= 0.80f;
		}

		myTransform.Translate (
			new Vector3 (
				moveX * speed * Time.deltaTime * modifier,
				0f,
				moveY * speed * Time.deltaTime * modifier
			)
		);

		if (!self.IsCalling) {
			updateFacing (moveX);
		}

		/*
		 * Attacks
		 */

		if (self.IsCalling) {
			return;
		}

		if (
			Input.GetButton ("stoneSlot") ||
			Input.GetButton ("attackerSlot") ||
			Input.GetButton ("optionalSlot") ||
			Input.GetButton ("generatorSlot")) {
			self.Target.gameObject.SetActive (true);
		}

		if (Input.GetButtonUp ("stoneSlot")) {
			self.Target.gameObject.SetActive (false);
			self.UseStoneSlot ();
		}

		if (Input.GetButtonUp ("attackerSlot")) {
			self.Target.gameObject.SetActive (false);
			self.UseAttackerSlot ();
		}

		if (Input.GetButtonUp ("optionalSlot")) {
			self.Target.gameObject.SetActive (false);
			self.UseOptionalSlot ();
		}

		if (Input.GetButtonUp ("generatorSlot")) {
			self.Target.gameObject.SetActive (false);
			self.UseGeneratorSlot ();
		}
	}

	private void updateFacing (float moveX)
	{
		if (moveX == 0.0f) {
			return;
		}

		bool newFacingRight = moveX > 0.0f;

		if (facingRight != newFacingRight) {
			toggleFacing ();
		}
	}

	private void toggleFacing ()
	{
		facingRight = !facingRight;

		mySpriteRenderer.flipX = !mySpriteRenderer.flipX;

		Transform objetive = self.Target;

		objetive.localPosition = new Vector3 (facingRight ? 1 : -1, 0.1f, 0);
	}

	public void TakeDamage (float damage)
	{
		isStunned = true;
		stunTimeout = stunCooldown;
		myTransform.Translate (Vector3.left * damage);
	}
}
