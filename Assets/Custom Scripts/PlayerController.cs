using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float turn_rate;
	public float speed;
	public float maxSpeed;
	public float minSpeed;
	private WeaponManager weaponManager;
	private bool fire_1_held;
	private bool fire_2_held;
	private bool isQuitting;
	private bool isLooping;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		weaponManager = (WeaponManager) GetComponent<WeaponManager> ();
		fire_1_held = false;
		fire_2_held = false;
		isLooping = false;
		speed = minSpeed;
	}
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.right * speed * Time.deltaTime);

		if (isLooping) {
			transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * turn_rate);
		}

		if (networkView.isMine) {
			if(!isLooping) {
				if (Input.GetAxis ("Horizontal") != 0) {
					transform.RotateAround (transform.position, Vector3.up, turn_rate * Input.GetAxis ("Horizontal") * Time.deltaTime);
				}

				if (Input.GetAxis ("AccelTrigger") != 0 && speed < maxSpeed) {
					speed = Mathf.Clamp (speed + 100f * Input.GetAxis ("AccelTrigger") * Time.deltaTime, minSpeed, maxSpeed);
				} else if (Input.GetAxis ("DeccelTrigger") != 0 && speed > minSpeed) {
					speed = Mathf.Clamp (speed - 100f * Input.GetAxis ("DeccelTrigger") * Time.deltaTime, minSpeed, maxSpeed);
				} else if (Input.GetAxis ("AccelButton") != 0 && speed < maxSpeed) {
					speed = Mathf.Clamp (speed + 100f * Input.GetAxis ("AccelButton") * Time.deltaTime, minSpeed, maxSpeed);
				} else if (Input.GetAxis ("DeccelButton") != 0 && speed > minSpeed) {
					speed = Mathf.Clamp (speed - 100f * Input.GetAxis ("DeccelButton") * Time.deltaTime, minSpeed, maxSpeed);
				}

				if (Input.GetKeyDown(KeyCode.D)) {
					isLooping = true;
				}
			}
			if (Input.GetAxis ("Fire1") != 0 && !fire_1_held) {
				weaponManager.Fire ();
				fire_1_held = true;
			}
//			if (Input.GetAxis ("Fire2") != 0 && !fire_2_held) {
//				weaponManager.Fire (secondary:true);
//				fire_2_held = true;
//			}

			fire_1_held = Input.GetAxis ("Fire1") != 0;
			fire_2_held = Input.GetAxis ("Fire2") != 0;
		}
	}

	void OnApplicationQuit() {
		isQuitting = true;
	}
	
	void OnDestroy() {
		if (!isQuitting) {
			Instantiate (explosion, this.transform.position, this.transform.rotation);
		}
	}
	
	public void Kill() {
		Vector3 expl_pos = this.transform.position - Random.insideUnitSphere * 2f;
		this.rigidbody.AddExplosionForce (50f, expl_pos, 3);
		Destroy (this.gameObject , 3f);
	}


//	void FixedUpdate() {
//
//		if (networkView.isMine) {
//
//			rigidbody.rotation = rigidbody.rotation * Quaternion.Euler (Vector3.up * Input.GetAxis ("Horizontal") * turn_rate * Time.deltaTime);
//
//			if (Input.GetAxis ("AccelTrigger") != 0 && rigidbody.velocity.magnitude < maxSpeed) {
//				rigidbody.AddRelativeForce (Vector3.right * Mathf.Clamp (5000f * Input.GetAxis ("AccelTrigger") * Time.deltaTime, 0, 5000f), ForceMode.Acceleration);
//			} else if (Input.GetAxis ("DeccelTrigger") != 0 && rigidbody.velocity.magnitude > minSpeed) {
//				rigidbody.AddRelativeForce (Vector3.left * Mathf.Clamp (5000f * Input.GetAxis ("DeccelTrigger") * Time.deltaTime, 0, 5000f), ForceMode.Acceleration);
//			} else if (Input.GetAxis ("AccelButton") != 0 && rigidbody.velocity.magnitude < maxSpeed) {
//				Debug.Log (Input.GetAxis ("AccelButton"));
//				rigidbody.AddRelativeForce (Vector3.right * Mathf.Clamp (5000f * Input.GetAxis ("AccelButton") * Time.deltaTime, 0, 5000f), ForceMode.Acceleration);
//			} else if (Input.GetAxis ("DeccelButton") != 0 && rigidbody.velocity.magnitude > minSpeed) {
//				rigidbody.AddRelativeForce (Vector3.left * Mathf.Clamp (5000f * Input.GetAxis ("DeccelButton") * Time.deltaTime, 0, 5000f), ForceMode.Acceleration);
//			}
//
//			if (Input.GetKey ("joystick button 4")) {
//				rigidbody.AddRelativeForce (Vector3.forward * 5000f, ForceMode.Force);
//			} else if (Input.GetKey ("joystick button 5")) {
//				rigidbody.AddRelativeForce (Vector3.forward * -5000f, ForceMode.Force);
//			}
//		}
//	}

}