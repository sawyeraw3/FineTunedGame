using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float minDistanceToGround = 1;
	public float walkSpeed = 6;
	public string playerID;
	GameObject body;
	
	// System vars
	//bool grounded;
	//bool flying = false;
	Vector3 moveAmount;
	
	//Vector3 targetFlyAmount;
	Rigidbody rigidbody;
	Vector3 smoothMoveVelocity;
	
	void Awake() {
		rigidbody = GetComponent<Rigidbody> ();
		body = gameObject.transform.FindChild ("Body").gameObject;
	}
	
	void Update() {

		if (Time.timeScale == 0)
			return;

		//groundedMask = transform.GetComponent<GravityBody> ().getPlanet ().layer;		

		// Look rotation:
		/*
		transform.Rotate(Vector3.up * CrossPlatformInputManager.GetAxis("Mouse X") * mouseSensitivityX);
		verticalLookRotation += CrossPlatformInputManager.GetAxis("Mouse Y") * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
		*/

		// Calculate movement:
		float inputX = Input.GetAxisRaw(playerID + "Horizontal");
		float inputY = Input.GetAxisRaw(playerID + "Vertical");
		
		Vector3 moveDir = new Vector3(inputX,0, inputY);
		if (moveDir.magnitude > 1)
			moveDir = moveDir.normalized;
		if(moveDir.magnitude != 0)
			body.transform.rotation = Quaternion.LookRotation (moveDir);

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
		/*
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, minDistanceToGround)){
			if(hit.transform.gameObject.layer == groundedMask)
				grounded = true;
		}
		else {
			grounded = false;
		}*/
		
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
	}
}
