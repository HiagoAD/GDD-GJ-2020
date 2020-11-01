using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RatTrap : BaseTrap
{
    [SerializeField] RatProjectile projectilePrefab;
    [SerializeField] float startSpeed = 1f;
    [SerializeField] Transform startPosition = null;
    [SerializeField] Sprite counterSprite = null;
    [SerializeField] Sprite counterBGSprite = null;
    [SerializeField] Sprite counterFinishedSprite = null;
    [SerializeField] Transform counterTransform = null;

    Image counter = null;
    private void Start()
    {
        Image counterBG = new GameObject().AddComponent<Image>();
        counterBG.sprite = counterBGSprite;
        counterBG.preserveAspect = true;

        counter = new GameObject().AddComponent<Image>();
        counter.sprite = counterSprite;
        counter.preserveAspect = true;

        counter.type = Image.Type.Filled;
        counter.fillAmount = 1;

        UIFollower follower = counterBG.gameObject.AddComponent<UIFollower>();
        follower.target = counterTransform;

        counterBG.transform.SetParent(UIController.Instance.transform);
        counter.transform.SetParent(counterBG.transform);
        counter.transform.localPosition = Vector3.zero;
    }

    protected override void OnMaxUsageReached()
    {
        canActivate = false;
        counter.fillAmount = 1;
        counter.sprite = counterFinishedSprite;
        counter.type = Image.Type.Simple;
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
        if (canActivate)
        {
            Activate();
            currentUsage++;
            counter.fillAmount = ((maxUsage - currentUsage) / (float)maxUsage);
        }
    }
}
