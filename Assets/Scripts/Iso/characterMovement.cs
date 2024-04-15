using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovement : MonoBehaviour
{
	// declare reference variables
	PlayerInputs playerInput;
	CharacterController characterController;
	Animator animator;

	// variables to store optimized setter/getter parameter IDs
	int isWalkingHash;
	int isRunningHash;

	// variables to store player input values
	Vector2 currentMovementInput;
	Vector3 currentMovement;
	Vector3 currentRunMovement;
	bool isMovementPressed;
	bool isRunPressed;
	float rotationFactorPerFrame = 15.0f;
	float runMultiplier = 4.0f;

	private void Awake()
	{
		// initially set reference variables
		playerInput = new PlayerInputs();
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

		isWalkingHash = Animator.StringToHash("isWalking");
		isRunningHash = Animator.StringToHash("isRunning");
		// set the player input callbacks
		playerInput.CharacterControls.Movement.started += onMovementInput;
		playerInput.CharacterControls.Movement.canceled += onMovementInput;
		playerInput.CharacterControls.Movement.performed += onMovementInput;
		playerInput.CharacterControls.Running.started += onRun;
		playerInput.CharacterControls.Running.canceled += onRun;
	}
	void onRun(InputAction.CallbackContext context)
	{
		isRunPressed = context.ReadValueAsButton();
	}

	// handler function to set the player input values
	void onMovementInput(InputAction.CallbackContext context){
			currentMovementInput = context.ReadValue<Vector2>();
			currentMovement.x = currentMovementInput.x;
			currentMovement.z = currentMovementInput.y;
			currentRunMovement.x = currentMovementInput.x * runMultiplier;
			currentRunMovement.z = currentMovementInput.y * runMultiplier;
			isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
	}
	void handleRotation(){
		Vector3 positionToLookAt;
		// the change in position our character should point to
		positionToLookAt.x = currentMovement.x;
		positionToLookAt.y = 0.0f;
		positionToLookAt.z = currentMovement.z;
		//current rotation of our character
		Quaternion currentRotation = transform.rotation;
		if (isMovementPressed)
		{
			//create a new rotation based on where the player is currently pressing
			Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
			transform.rotation = Quaternion.Slerp(currentRotation,targetRotation, rotationFactorPerFrame * Time.deltaTime);
		}
	}

	void handleAnimation()
	{
		// get parameter value from animator
		bool isWalking = animator.GetBool(isWalkingHash);
		bool isRunning = animator.GetBool(isRunningHash);

		// start walking if movement pressed is true and not already walking
		if (isMovementPressed && !isWalking){
			animator.SetBool(isWalkingHash, true);
		}
		// stop walking if isMovementPressed is false and not already walking
		else if (!isMovementPressed && isWalking){
			animator.SetBool(isWalkingHash, false);
		}
		// run if movement and run pressed are true and not currently running
		if ((isMovementPressed && isRunPressed) && !isRunning)
		{
			animator.SetBool(isRunningHash, true);
		}
		// stop running if movement or run pressed are false and currently running
		else if ((!isMovementPressed || !isRunPressed) && isRunning){
			animator.SetBool(isRunningHash,false);
		}
	}

	void handleGravity()
	{
		//apply proper gravity depending on if the character is grounded or not
		if (characterController.isGrounded)
		{
			float groundedGravity = -.05f;
			currentMovement.y = groundedGravity;
			currentRunMovement.y = groundedGravity;
		} else
		{
			float gravity = -9.8f;
			currentMovement.y += gravity;
			currentRunMovement.y += gravity;
		}
	}
    // Update is called once per frame
    void Update()
    {
		handleRotation();
		handleAnimation();
		if (isRunPressed)
		{
			characterController.Move(currentRunMovement * Time.deltaTime);
		} else {
			characterController.Move(currentMovement * Time.deltaTime);
		}
    }


	private void OnEnable() {
		playerInput.CharacterControls.Enable();
	}

	private void OnDisable() {
		playerInput.CharacterControls.Disable();
	}
}
