using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour {
	[SerializeField] private LevelDataObject levelDef = null;

	[Header("Object references")]
	[SerializeField] private Transform startPoint = null;
	[SerializeField] private Transform destinationPoint = null;
	[SerializeField] private BasicUnitController spawnPrefab = null;
	[SerializeField] private Transform spawnsParent = null;

	[Header("Unit Events")]
	public UnityEvent onUnitDefeat = new UnityEvent();

	private int qtSpawned = 0;

	void Start () {
		StartCoroutine (SpawnLoop ());
	}

	IEnumerator SpawnLoop () {
		while (true) {
			Spawn ();
			yield return new WaitForSeconds (this.levelDef.GetCurrentSpawnPeriod(this.qtSpawned));
			this.qtSpawned++;
		}
	}

	void Spawn () {
		BasicUnitController spawnedUnit = Instantiate (this.spawnPrefab, this.startPoint.position, Quaternion.identity, this.spawnsParent);
		spawnedUnit.Initialize (this.startPoint, this.destinationPoint, this.levelDef);
		spawnedUnit.onDefeat.AddListener(() => this.onUnitDefeat.Invoke());
	}
}