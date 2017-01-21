using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	
	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float minDistanceToGround = 1;
	public float walkSpeed = 6;
	public float runSpeed = 6;
	public float Speed = 10f;
	public bool canJump;
	public float jumpForce = 220;
	int groundedMask;
	int planetNum;
	
	// System vars
	bool grounded;
	bool flying = false;
	Vector3 moveAmount;
	
	Vector3 targetFlyAmount;

	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody rigidbody;
	Animator anim;
	
	
	void Awake() {
		cameraTransform = Camera.main.transform;
		rigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update() {
		
		//groundedMask = transform.GetComponent<GravityBody> ().getPlanet ().layer;		
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");
		
		Vector3 moveDir = new Vector3(inputX,0, inputY);
		if (moveDir.magnitude > 1)
			moveDir = moveDir.normalized;
		
		Vector3 targetMoveAmount = Vector3.zero;
		/*if(CrossPlatformInputManager.GetButton("LButton")){
			targetMoveAmount = moveDir * runSpeed;
			if(anim)
				anim.SetFloat("Speed", moveDir.magnitude * runSpeed);
		} else {
			targetMoveAmount = moveDir * walkSpeed;
			if(anim)
				anim.SetFloat("Speed", moveDir.magnitude);
		}*/
		targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);
		
		
		//Jump condition
		/*
		if (Input.GetButtonDown("A") && canJump) {
			rigidbody.AddForce(transform.up * jumpForce);
		}
		*/

		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, minDistanceToGround)){
			if(hit.transform.gameObject.layer == groundedMask)
				grounded = true;
		}
		else {
			grounded = false;
		}
		
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
	}
}
