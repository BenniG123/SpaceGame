using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

	public GameObject activeWeapon;
	public GameObject secondaryWeapon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire(bool secondary = false, Transform target = null) {
		Quaternion projectile_quat = this.transform.rotation;
		projectile_quat *= Quaternion.Euler (90, 90, 0);
		GameObject projectile;
		if (!secondary) {
			projectile = (GameObject)Network.Instantiate (activeWeapon, this.transform.position, projectile_quat, 0);
		} else {
			projectile = (GameObject)Network.Instantiate (secondaryWeapon, this.transform.position, projectile_quat, 0);
		}
		((WeaponController)projectile.GetComponent<WeaponController> ()).team = GetComponent<PlayerState> ().team;
		if (target) {
			((WeaponController) projectile.GetComponent<WeaponController>()).target = target;
		}
	}
}
