using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class BasicUnitController : MonoBehaviour {
	[SerializeField] private float speed = 1f;
	private Transform startPoint;
	private Transform destinationPoint;

	private bool goingToDestination = true;

	private Rigidbody2D rb;

	void Awake () {
		this.rb = GetComponent<Rigidbody2D> ();
	}

	public void Initialize (Transform startPoint, Transform destinationPoint) {
		this.startPoint = startPoint;
		this.destinationPoint = destinationPoint;
		GoToDestination ();
	}

	public bool IsGoingTo (bool destination) {
		return this.goingToDestination == destination;
	}

	protected virtual void OnHitTrap(BaseTrap trap)  {
		GoBackHome ();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		BaseTrap trap = other.GetComponent<BaseTrap>();
		if (trap && trap.Activated) OnHitTrap(trap);
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