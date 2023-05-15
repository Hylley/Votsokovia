using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook	: MonoBehaviour
{
	public float mouseSensitivity;
	public float hitDistance;

	public Transform playerBody;

	private	float xAxisClamp;
	
	[Space(7)]

	public Transform hand;
	public Transform equiped;
	public float handItemLerpSpeed;
	[Space(7)]
		// How long	the	object should shake	for.
	public float shakeDuration = 0f;
	// Amplitude of the	shake. A larger	value shakes the camera	harder.
	public float shakeAmount = .05f;
	public float decreaseFactor	= 2f;
	Vector3	originalPos;

	private	void Awake()
	{
		LockCursor();
		xAxisClamp = 0.0f;
	}

	void OnEnable()
	{
		originalPos	= transform.localPosition;
	}


	private	void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private	void Update()
	{
		CameraRotation();

		if(equiped != null)
		{
			equiped.position = Vector3.Lerp(equiped.position, hand.position, handItemLerpSpeed);
			equiped.rotation = Quaternion.Lerp(equiped.rotation, transform.rotation, handItemLerpSpeed);
		}

		if (shakeDuration >	0)
		{
			transform.localPosition	= originalPos +	Random.insideUnitSphere	* shakeAmount;
			
			shakeDuration -= Time.deltaTime	* decreaseFactor;
		}
		else
		{
			shakeDuration =	0f;
			transform.localPosition	= originalPos;
		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.forward, out hit, hitDistance))
			{
				IInteractable interactableObject = hit.transform.GetComponent<IInteractable>();
				if(interactableObject	== null)
					return;
				interactableObject.Interact();
			}
		}
	}

	private	void CameraRotation()
	{
		float mouseX = Input.GetAxis("Mouse X")	* mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y")	* mouseSensitivity * Time.deltaTime;

		xAxisClamp += mouseY;

		if(xAxisClamp >	90.0f)
		{
			xAxisClamp = 90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(270.0f);
		}
		else if (xAxisClamp	< -90.0f)
		{
			xAxisClamp = -90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(90.0f);
		}

		transform.Rotate(Vector3.left *	mouseY);
		playerBody.Rotate(Vector3.up * mouseX);
	}

	private	void ClampXAxisRotationToValue(float value)
	{
		Vector3	eulerRotation =	transform.eulerAngles;
		eulerRotation.x	= value;
		transform.eulerAngles =	eulerRotation;
	}
}