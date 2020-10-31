using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class BasicUnitController : MonoBehaviour {
	[Header ("Protections")]
	[SerializeField] private bool hasRainBoots = false;
	[SerializeField] private bool hasUmbrella = false;

	[Header ("Behaviour")]
	[SerializeField] private float speed = 1f;
	private Transform startPoint;
	private Transform destinationPoint;

	private bool goingToDestination = true;

	private Rigidbody2D rb;

	void Awake () {
		this.rb = GetComponent<Rigidbody2D> ();
	}

	public void Initialize (Transform startPoint, Transform destinationPoint, LevelDataObject levelDef) {
		this.hasRainBoots = RollChance(levelDef.RainBootsChance);
		this.hasUmbrella = RollChance(levelDef.UmbrellaChance);

		this.startPoint = startPoint;
		this.destinationPoint = destinationPoint;
		GoToDestination ();
	}

	public bool IsGoingTo (bool destination) {
		return this.goingToDestination == destination;
	}

	public void OnHitTrap(BaseTrap trap)
	{
		if (trap is ManholeTrap)
		{
			if (!(this.hasRainBoots || this.hasUmbrella))
				GoBackHome();
		} else if (trap is DoveTrap)
        {
			Debug.Log("Dove Shit");
        }
	}

	private bool RollChance(float chance) {
		return Random.Range(0f, 1f) <= chance;
	}

	private void GoBackHome () {
		Vector3 direction = (startPoint.position - destinationPoint.position).normalized;
		this.rb.velocity = direction * this.speed;
		this.goingToDestination = false;
	}

	private void GoToDestination () {
		Vector3 direction = (destinationPoint.position - startPoint.position).normalized;
		this.rb.velocity = direction * this.speed;
		this.goingToDestination = true;
	}
}