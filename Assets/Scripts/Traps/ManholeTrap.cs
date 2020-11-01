using UnityEngine;
using UnityEngine.UI;

public class ManholeTrap : BaseTrap
{
    [Header("Manhole Trap")]
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
        timerBG.transform.localScale = Vector3.one;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Activated)
        {
            BasicUnitController unitController = collision.gameObject.GetComponent<BasicUnitController>();
            if (unitController != null && unitController.IsGoingTo(true))
            {
                unitController.OnHitTrap(this);
            }
        }
    }
}
