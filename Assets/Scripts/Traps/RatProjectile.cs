using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class RatProjectile : MonoBehaviour
{
    RatTrap parent = null;
    public void Init(float speed, RatTrap parent)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        this.parent = parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BasicUnitController unitController = collision.gameObject.GetComponent<BasicUnitController>();
        if (unitController != null)
        {
            unitController.OnHitTrap(parent);
            Destroy(gameObject);
        }
    }
}
