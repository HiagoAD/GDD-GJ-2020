using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "LevelData", menuName = "ScriptableObjects/LevelDataObject")]
public class LevelDataObject : ScriptableObject {
	[Header ("Frequency")]
	[Tooltip ("The initial frequency of the spawning of units")][SerializeField] private float initialUnitsPerSecond = 0f;
	[Tooltip ("The increase on the unit spawn frequency after every spawn")][SerializeField] private float unitsPerSecondIncrease = 0f;

	[Header ("Probabilities")]
	[Tooltip ("The probability of the unit having rain boots")][SerializeField] private float rainBootsChance = 0;
	[Tooltip ("The probability of the unit having an umbrella")][SerializeField] private float umbrellaChance = 0;

	public float RainBootsChance {
		get {
			return this.rainBootsChance;
		}
	}

	public float UmbrellaChance {
		get {
			return this.umbrellaChance;
		}
	}

	public float GetCurrentSpawnPeriod (int qtAlreadySpawned = 0) {
		return 1 / (this.initialUnitsPerSecond + qtAlreadySpawned * this.unitsPerSecondIncrease);
	}
}