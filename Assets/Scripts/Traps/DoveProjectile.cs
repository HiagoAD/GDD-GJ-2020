using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class DoveProjectile : MonoBehaviour
{
    DoveTrap parent = null;
    public void Init(float speed, DoveTrap parent)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
        this.parent = parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BasicUnitController unitController = collision.gameObject.GetComponent<BasicUnitController>();
        if (unitController != null && unitController.IsGoingTo(true))
        {
            unitController.OnHitTrap(parent);
            Destroy(gameObject);
        }
    }
}
