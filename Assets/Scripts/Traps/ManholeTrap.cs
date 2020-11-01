using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManholeTrap : BaseTrap
{
    [SerializeField] float activeTime = 3f;
    [SerializeField] Sprite timerSprite = null;
    [SerializeField] Sprite timerBGSprite = null;
    [SerializeField] Sprite timerFinishedSprite = null;
    [SerializeField] Transform timerTransform = null;

    float activatedAt = 0f;
    float deactivatedAt = 0f;

    Image timer = null;
    private void Start()
    {
        Image timerBG = new GameObject().AddComponent<Image>();
        timerBG.sprite = timerBGSprite;
        timerBG.preserveAspect = true;

        timer = new GameObject().AddComponent<Image>();
        timer.sprite = timerSprite;
        timer.preserveAspect = true;

        timer.type = Image.Type.Filled;
        timer.fillAmount = 1;

        UIFollower follower = timerBG.gameObject.AddComponent<UIFollower>();
        follower.target = timerTransform;

        timerBG.transform.SetParent(UIController.Instance.transform);
        timer.transform.SetParent(timerBG.transform);
        timer.transform.localPosition = Vector3.zero;
    }
    private void Update()
    {

        if(Time.time >= deactivatedAt && Activated)
        {
            Activated = false;
        }
        else
        {
            if (!Activated)
            {
                timer.fillAmount = 1;
            }
            else
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
    }


    protected override void OnMaxUsageReached()
    {
        canActivate = false;
        timer.fillAmount = 1;
        timer.sprite = timerFinishedSprite;
        timer.type = Image.Type.Simple;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (canActivate)
        {
            Activate();
            currentUsage++;
            timer.fillAmount = ((maxUsage - currentUsage) / (float)maxUsage);
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
