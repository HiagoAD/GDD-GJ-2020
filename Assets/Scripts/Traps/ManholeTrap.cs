using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManholeTrap : BaseTrap
{
    [SerializeField] float activeTime = 3f;
    
    protected override void Activate()
    {
        Debug.Log("Active");
        Activated = true;
        StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(activeTime);
        Debug.Log("Deactive");
        Activated = false;
    }

    protected override void OnMaxUsageReached()
    {
        canActivate = false;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        currentUsage++;
        if(canActivate)
        {
            Activate();
        }
    }
}
