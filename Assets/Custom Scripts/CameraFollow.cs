using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public Texture red;
	private Color guiColor;

	// How long the object should shake for.
	public float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private Vector3 originalPos;
	public float hurtEffectTime = 0.0f;

	public void Start() {
		guiColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target) {

			Vector3 point = camera.WorldToViewportPoint (target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			originalPos = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);

			if (shake > 0)
			{
				transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
				shake -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				transform.position = originalPos;
			}

		} else {
			//GetComponent<GameManager>()
		}

		if (hurtEffectTime > 0.0f) {
			hurtEffectTime -= Time.deltaTime * decreaseFactor;
			guiColor.a = hurtEffectTime;
		}
		
	}

	void OnGUI() {
		if (hurtEffectTime > 0.0f) {
			GUI.color = guiColor;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), red, ScaleMode.StretchToFill);
		}
	}
}