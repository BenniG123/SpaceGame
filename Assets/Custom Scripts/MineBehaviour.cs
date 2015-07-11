using UnityEngine;
using System.Collections;

public class MineBehaviour : MonoBehaviour {

	public float lifeTime;
	private float counter;
	public GameObject explosion;
	private bool isQuitting;

	// Use this for initialization
	void Start () {

		counter = lifeTime;
		isQuitting = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		counter -= Time.deltaTime;
		if (counter <= 0f) {
			Destroy (this.gameObject);
		}
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.GetComponent<PlayerState>().team != GetComponent<WeaponController> ().team) {
				Destroy (this.gameObject);
			}
		}
	}

	void OnApplicationQuit() {
		isQuitting = true; 
	}
	
	void OnDestroy() {
			if (!isQuitting) {
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, 15);
				int i = 0;
				while (i < hitColliders.Length) {
					if(hitColliders[i].gameObject.tag == "Player") {
						hitColliders[i].gameObject.GetComponent<PlayerState>().TakeDamage(5, 0);
						i++;
					}
				}
				Instantiate (explosion, this.transform.position, this.transform.rotation);
			}
	}

}
