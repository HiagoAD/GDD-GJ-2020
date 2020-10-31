using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseTrap : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int maxUsage = 1;

    public bool Activated { get; protected set; } = false;

    protected bool canActivate = true;

    private int _currentUsage = 0;
    protected int currentUsage
    {
        get { return _currentUsage; }
        set
        {
            if (_currentUsage >= maxUsage)
            {
                OnMaxUsageReached();
            } else
            {
                _currentUsage = value;
            }
        }
    }

    protected virtual void OnMaxUsageReached()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void Activate()
    {
        throw new System.NotImplementedException();
    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    
}
