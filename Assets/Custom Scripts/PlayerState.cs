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
	
	void Update ()
	{
		// If health is less than or equal to 0...
		if (hp <= 0f) {
			// ... and if the player is not yet dead...
			if (alive) {
					PlayerController controller = gameObject.GetComponent<PlayerController>();
					controller.Kill();
					alive = false;
			}
		}
	}

	public void TakeDamage(int damage, float force) {
		hp -= damage;
	}
}
