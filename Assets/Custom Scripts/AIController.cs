using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public float speed = 200f;
	public float turn_rate = 70f;
	public GameObject projectile;
	public int team;
	private bool isQuitting;
	public GameObject explosion;
	public Transform target;
	private float shotTimer;

	// Use this for initialization
	void Start () {
		shotTimer = 0f;
		isQuitting = false;
	}
	// Update is called once per frame
	void Update () {

		shotTimer += Time.deltaTime;
		if (shotTimer > 1f) {
			Quaternion laser_quat = this.transform.rotation;
			laser_quat *= Quaternion.Euler(90, 90, 0);
			Instantiate(projectile,this.transform.position, laser_quat);
			shotTimer = 0f;
		}
		transform.Translate (Vector3.right * speed * Time.deltaTime, Space.Self);
		if (target != null) {
			//Turn to face target
			Vector3 diff = target.position - transform.position;
			diff.Normalize();
			float rot_y = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
			Quaternion target_rot = Quaternion.Euler(0f, rot_y - 90, 0f);
			transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, Time.deltaTime * turn_rate);
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
		Vector3 expl_pos = this.transform.position - Random.insideUnitSphere * 4f;
		this.rigidbody.AddExplosionForce (50f, expl_pos, 5);
		Destroy (this.gameObject , 3f);
	}
}
