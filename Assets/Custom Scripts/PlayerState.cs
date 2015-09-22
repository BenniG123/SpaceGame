using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {

	public int team;
	public float shield;
	public float mhp;
	public float hp;
	public float shield_reset_time;
	public float shield_recharge_rate;
	public bool alive;
	public GUIText gt;

	void Start () {
		gt.text = "PLEASE SEE THIS";
		gt.color = Color.cyan;
		gt.fontSize = 20;
	}
	
	void Update ()
	{
		if (networkView.isMine) {
			// If health is less than or equal to 0...
			if (hp <= 0f) {
				// ... and if the player is not yet dead...
				if (alive) {
					Rigidbody r = gameObject.rigidbody;
					r.isKinematic = false;
					PlayerController controller = gameObject.GetComponent<PlayerController>();
					controller.Kill();
					alive = false;
				}
			}
		}
	}

	public void TakeDamage(int damage, float force) {
		//hp -= damage;
		if (networkView.isMine) {
			Camera.main.GetComponent<CameraFollow>().shake = 1.0f;
			Camera.main.GetComponent<CameraFollow>().shakeAmount = force;
			Camera.main.GetComponent<CameraFollow>().hurtEffectTime = 1.0f;
		}
	}
	
}
