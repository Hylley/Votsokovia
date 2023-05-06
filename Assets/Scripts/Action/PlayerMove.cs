using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove	: MonoBehaviour
{
	CharacterController controller;

	public float speed = 12f;
	public float gravity = -9.81f;
	public float jump =	1f;

	public Transform groundCheck;
	public float groundDistance	= 0.4f;
	public LayerMask groundMask;

	Vector3	velocity;
	bool isGrounded;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	// Update is called	once per frame
	void Update()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if(isGrounded && velocity.y	< 0)
		{
			velocity.y = -2f;
		}

		float x	= Input.GetAxis("Horizontal");
		float z	= Input.GetAxis("Vertical");

		Vector3	move = transform.right * x + transform.forward * z;

		controller.Move(move * speed * Time.deltaTime);

		if(Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jump * -2f * gravity);
		}

		velocity.y += gravity *	Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);
	}

	// private	void JumpInput()
	// {
	//	if(Input.GetKeyDown(jumpKey) && !isJumping)
	//	{
	//		isJumping =	true;
	//		// StartCoroutine(JumpEvent());
	//	}
	// }

	// private void	Jump()
	// `
	// {
	//	charController.isGrounded
	// }

	// private IEnumerator JumpEvent()
	// {
	//	   charController.slopeLimit = 90.0f;
	//	   float timeInAir = 0.0f;

	//	   do
	//	   {
	//		   float jumpForce = jumpFallOff.Evaluate(timeInAir);
	//		   charController.Move(Vector3.up *	jumpForce *	jumpMultiplier * Time.deltaTime);
	//		   timeInAir += Time.deltaTime;
	//		   yield return	null;
	//	   } while (!charController.isGrounded && charController.collisionFlags	!= CollisionFlags.Above);

	//	   charController.slopeLimit = 45.0f;
	//	   isJumping = false;
	// }

}