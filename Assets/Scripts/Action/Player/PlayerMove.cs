using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove	: MonoBehaviour
{
	Rigidbody rb;

	Vector3 direction;
	public float speed = 12f;

	public AudioSource walkSound;
	// public float jump =	1f;

	// public Transform groundCheck;
	// public float groundDistance	= 0.4f;
	// public LayerMask groundMask;

	Vector3	velocity;
	// bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		// isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		float x	= Input.GetAxis("Horizontal");
		float z	= Input.GetAxis("Vertical");

		direction = (transform.forward * z) + (transform.right * x);
		rb.velocity = direction * speed;
		rb.angularVelocity = Vector3.zero;

		if((x != 0 || z != 0) && !walkSound.isPlaying)
		{
			walkSound.Play();
		}
		else if(x == 0 && z == 0 && walkSound.isPlaying)
		{
			walkSound.Stop();
		}
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