using UnityEngine;
using UnityEngine.UI;

public class DoveTrap : BaseTrap
{
    [Header("Dove Trap")]
    [SerializeField] DoveProjectile projectilePrefab;
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
        counterBG.transform.localScale = Vector3.one;
    }

    protected override void Activate()
    {
        DoveProjectile proj = Instantiate(projectilePrefab);
        proj.Init(startSpeed, this);
        proj.transform.SetParent(startPosition);
        proj.transform.localPosition = Vector3.zero;
    }

    void Update() {
        counter.fillAmount = 1 - this.usageFraction;
    }
}
