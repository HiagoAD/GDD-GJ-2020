using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UIFollower : MonoBehaviour
{
    [SerializeField] public Transform target;

    
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position);
    }
}
