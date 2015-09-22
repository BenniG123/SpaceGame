using UnityEngine;
using System.Collections;

public class MineBehaviour : MonoBehaviour {

	public float lifeTime;
	private float counter;
	public GameObject explosion;
	private bool isQuitting;
	public int team;
	public float radius;

	// Use this for initialization
	void Start () {
		counter = lifeTime;
	}
	
	// Update is called once per frame
	void Update () {
		counter -= Time.deltaTime;
		if (counter <= 0f) {
			Explode();
			Destroy (this.gameObject);
		}
	
	}

	void OnTriggerEnter(Collider col) {
		PlayerState s = col.gameObject.GetComponent<PlayerState> ();
		if (s != null) {
			if (s.team != team) {
				Explode();
				Destroy (this.gameObject);
			}
		}
	}


	private void Explode() {

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

		foreach (Collider c in hitColliders) {
			PlayerState s = c.gameObject.GetComponent<PlayerState> ();
			if (s != null) {
				s.TakeDamage(2, 5.0f);
			}
		}
		
		/* foreach (Collider c in hitColliders) {
			Rigidbody r = c.rigidbody;
			if (r != null) {
				r.AddExplosionForce(500.0f, transform.position, radius);
			}
		}*/

		Instantiate (explosion, this.transform.position, this.transform.rotation);
	}


}
