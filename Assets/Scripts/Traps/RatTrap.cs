using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RatTrap : BaseTrap
{
    [SerializeField] RatProjectile projectilePrefab;
    [SerializeField] float startSpeed = 1f;
    [SerializeField] Transform startPosition = null;


    protected override void OnMaxUsageReached()
    {
        canActivate = false;
    }

    protected override void Activate()
    {
        RatProjectile proj = Instantiate(projectilePrefab);
        proj.Init(startSpeed, this);
        proj.transform.SetParent(startPosition);
        proj.transform.localPosition = -Vector3.forward;
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        currentUsage++;
        if (canActivate)
        {
            Activate();
        }
    }
}
