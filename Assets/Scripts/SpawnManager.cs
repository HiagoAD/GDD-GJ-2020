using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	[SerializeField] private Transform startPoint = null;
	[SerializeField] private Transform destinationPoint = null;
	[SerializeField] private BasicUnitController spawnPrefab = null;
	[SerializeField] private Transform spawnsParent = null;

	void Start () {
		StartCoroutine (SpawnLoop ());
	}

	IEnumerator SpawnLoop () {
		while (true) {
			Spawn ();
			yield return new WaitForSeconds (1.5f);
		}
	}

	void Spawn () {
		BasicUnitController spawnedUnit = Instantiate (this.spawnPrefab, this.startPoint.position, Quaternion.identity, this.spawnsParent);
		spawnedUnit.Initialize (this.startPoint, this.destinationPoint);
	}
}