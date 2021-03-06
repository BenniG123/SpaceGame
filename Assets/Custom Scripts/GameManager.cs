﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject networkPrefab;
	public ArrayList spawners_1;
	public ArrayList spawners_2;
	private ArrayList players_1;
	private ArrayList players_2;

	void OnServerInitialized()
	{
			// SpawnPlayer((GameObject) spawners_2[Random.Range(0, spawners_2.Count - 1)], 2);
			//if(players_1.Count > players_2.Count) {
			//	SpawnPlayer((GameObject) spawners_2[Random.Range(0, spawners_2.Count - 1)], 2);
			//} else {
			SpawnPlayer((GameObject) spawners_1[Random.Range(0, spawners_1.Count - 1)], 1);
			//}
	}

	void OnConnectedToServer()
	{
			// SpawnPlayer((GameObject) spawners_1[Random.Range(0, spawners_1.Count - 1)], 1);
			//if(players_1.Count > players_2.Count) {
				SpawnPlayer((GameObject) spawners_1[Random.Range(0, spawners_1.Count - 1)], 1);
			//} else {
			//	SpawnPlayer((GameObject) spawners_1[Random.Range(0, spawners_1.Count - 1)], 1);
			//}
	}
	
	private void SpawnPlayer(GameObject s, int team)
	{
		GameObject net_player = (GameObject) Network.Instantiate(networkPrefab, s.transform.position, s.transform.rotation, 0);
		net_player.GetComponent<PlayerState> ().team = team;
		GetComponent<CameraFollow> ().target = net_player.transform;

		if (team == 1) {
			players_1.Add (net_player);
		} else {
			players_2.Add (net_player);
		}

	}

	// Use this for initialization
	void Start () {

		spawners_1 = new ArrayList(10);
		spawners_2 = new ArrayList(10);
		players_1 = new ArrayList (10);
		players_2 = new ArrayList (10);

		//Initialize all spawners
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Spawn1")) {
			spawners_1.Add(g);
		}

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Spawn2")) {
			spawners_2.Add(g);
		}

	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject player in players_1) {
			if (player == null) {
				players_1.Remove(player);
				SpawnPlayer((GameObject) spawners_1[0], 1);
			}
		}
		foreach (GameObject player in players_2) {
			if (player == null) {
				players_2.Remove(player);
				SpawnPlayer((GameObject) spawners_2[0], 2);
			}
		}
	
	}
	

}