using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseTrap : MonoBehaviour, IPointerClickHandler
{
    [Header("Usages")]
    [SerializeField] protected int maxUsage = 1;
    [SerializeField] protected float rechargeUsageTime = 5f;

    public bool Activated { get; protected set; } = false;

    protected float usageFraction {
        get { return this.currentUsage / (float)this.maxUsage; }
    }

    protected Coroutine rechargeCoroutine;

    private int currentUsage = 0;
    private bool canActivate() {
        return this.currentUsage < this.maxUsage && !this.Activated;
    }

    protected void TryUseTrap() {
        if (this.canActivate()) {
            Activate();
            this.currentUsage++;
            if (this.rechargeCoroutine == null)
                this.rechargeCoroutine = StartCoroutine (RechargeLoop ());
        }
    }

    protected IEnumerator RechargeLoop() {
        while (this.currentUsage > 0) {
            yield return new WaitForSeconds (this.rechargeUsageTime);
            this.currentUsage--;
        }
        this.rechargeCoroutine = null;
    }

    protected virtual void Activate()
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TryUseTrap();
    }
}
