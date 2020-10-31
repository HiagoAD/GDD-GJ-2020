using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Collider2D))]
public class EndpointController : MonoBehaviour {
	[SerializeField] private bool isDestination = true;
	[SerializeField] private UnityEvent onEndPointReached = new UnityEvent ();
	void OnTriggerEnter2D (Collider2D other) {
		BasicUnitController unit = other.GetComponent<BasicUnitController> ();
		if (unit != null && unit.IsGoingTo (this.isDestination)) {
			Destroy (other.gameObject);
			this.onEndPointReached.Invoke ();
		}
	}
}