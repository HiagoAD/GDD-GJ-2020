using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManholeTrap : BaseTrap
{
    [SerializeField] float activeTime = 3f;
    [SerializeField] Sprite timerSprite = null;
    [SerializeField] Transform timerTransform = null;

    float activatedAt = 0f;
    float deactivatedAt = 0f;

    Image timer = null;

    private void Update()
    {

        if(Time.time >= deactivatedAt && Activated)
        {
            Debug.Log("Deactivating");
            Destroy(timer.gameObject);
            timer = null;
            Activated = false;
        }
        else
        {
            if (timer != null)
            {
                timer.fillAmount = (deactivatedAt - Time.time) / activeTime;
            }
        }
    }

    protected override void Activate()
    {
        Debug.Log("Active");
        Activated = true;
        activatedAt = Time.time;
        deactivatedAt = activatedAt + activeTime;

        if(timer == null)
        {
            timer = new GameObject().AddComponent<Image>();
            timer.sprite = timerSprite;

            timer.type = Image.Type.Filled;
            timer.fillAmount = 1;

            UIFollower follower = timer.gameObject.AddComponent<UIFollower>();
            follower.target = timerTransform;

            timer.transform.SetParent(UIController.Instance.transform);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Activated)
        {
            BasicUnitController unitController = collision.gameObject.GetComponent<BasicUnitController>();
            if (unitController != null)
            {
                unitController.OnHitTrap(this);
            }
        }
    }
}
