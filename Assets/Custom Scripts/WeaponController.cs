﻿using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public int team;
	public Transform target;
	public float timer;
	public float speed;
	public GameObject destroyPrefab;
	public int damage;
	public float force;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, timer);
		rigidbody.velocity = transform.rotation * Vector3.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {

		}
	}
	
	void OnTriggerEnter(Collider col) {
		PlayerState s = col.gameObject.GetComponent<PlayerState> ();
		if (s != null) {
			if (s.team != team) {
				s.TakeDamage(damage, force);
				Instantiate (destroyPrefab, this.transform.position, this.transform.rotation);
				Destroy (this.gameObject);
			}
		} else {
			Instantiate (destroyPrefab, this.transform.position, this.transform.rotation);
			Destroy (this.gameObject);
		}
	}

}
