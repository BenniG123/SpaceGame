using UnityEngine;
using System.Collections;

public class LaserBehaviour : MonoBehaviour {

	public float speed;
	public GameObject destroyPrefab;

	// Use this for initialization
	void Start () {

		Destroy (this.gameObject, 2f);
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.up * speed * Time.deltaTime);

	}

	void OnTriggerEnter(Collider col) {

		PlayerState s = col.gameObject.GetComponent<PlayerState> ();
		if (s != null) {
			s.TakeDamage(1, 1.0f);
		}

		Instantiate (destroyPrefab, this.transform.position, this.transform.rotation);
		Destroy (this.gameObject);
	}

}
