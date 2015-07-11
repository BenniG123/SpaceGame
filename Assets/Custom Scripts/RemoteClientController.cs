using UnityEngine;
using System.Collections;

//Controller for remote client game objects
public class RemoteClientController : MonoBehaviour {

	//Master network object
	public GameObject networkObject;
	private PlayerController playerController;
	private bool isQuitting;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		playerController = (PlayerController)networkObject.GetComponent<PlayerController> ();
	}
	
	// INTERPOLATION AND PREDICTION DONE HERE
	void Update () {

		transform.Translate (Vector3.right * playerController.speed);
	
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
		Destroy (networkObject, 3f);
		Destroy (this.gameObject , 3f);
	}
}
