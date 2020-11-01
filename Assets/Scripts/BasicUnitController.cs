using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Collider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class BasicUnitController : MonoBehaviour {
	[Header ("Protections")]
	[SerializeField] private bool hasRainBoots = false;
	[SerializeField] private bool hasUmbrella = false;

	[Header ("Behaviour")]
	[SerializeField] private float speed = 1f;
	private Transform startPoint;
	private Transform destinationPoint;

	[Header("Animations")]
	[SerializeField] private Animator baseSkin;
	[SerializeField] private Animator umbrellaBoot;
	[SerializeField] private Animator umbrella;
	[SerializeField] private Animator boot;
	[SerializeField] private Animator shitHit;
	[SerializeField] private Animator shitHitBoot;
	[SerializeField] private Animator waterHit;
	[SerializeField] private Animator ratDebuffBoot;
	[SerializeField] private Animator ratDebuffUmbrella;
	[SerializeField] private Animator ratDebuffBootUmbrella;
	[SerializeField] private Animator defeatedShitBoot;
	[SerializeField] private Animator defeatedWater;

	[Header("Events")]
	public UnityEvent onDefeat = new UnityEvent();
	public UnityEvent onHitRat = new UnityEvent();
	public UnityEvent onHitShit = new UnityEvent();
	public UnityEvent onHitWater = new UnityEvent();

	private bool goingToDestination = true;

	private Rigidbody2D rb;
	private Animator active;

	void Awake () {
		this.rb = GetComponent<Rigidbody2D> ();
		baseSkin.gameObject.SetActive(false);
		umbrellaBoot.gameObject.SetActive(false);
		umbrella.gameObject.SetActive(false);
		boot.gameObject.SetActive(false);
		shitHit.gameObject.SetActive(false);
		shitHitBoot.gameObject.SetActive(false);
		waterHit.gameObject.SetActive(false);
		ratDebuffBoot.gameObject.SetActive(false);
		ratDebuffUmbrella.gameObject.SetActive(false);
		ratDebuffBootUmbrella.gameObject.SetActive(false);
		defeatedShitBoot.gameObject.SetActive(false);
		defeatedWater.gameObject.SetActive(false);
	}

    private void Start()
    {
        if(hasRainBoots && hasUmbrella)
        {
			umbrellaBoot.gameObject.SetActive(true);
			active = umbrellaBoot;
        } else if(hasRainBoots)
        {
			boot.gameObject.SetActive(true);
			active = boot;
        } else if(hasUmbrella)
        {
			umbrella.gameObject.SetActive(true);
			active = umbrella;
        } else
        {
			baseSkin.gameObject.SetActive(true);
			active = baseSkin;
        }
    }

    public void Initialize (Transform startPoint, Transform destinationPoint, LevelDataObject levelDef) {
		this.hasRainBoots = RollChance(levelDef.RainBootsChance);
		this.hasUmbrella = RollChance(levelDef.UmbrellaChance);

		this.startPoint = startPoint;
		this.destinationPoint = destinationPoint;
		GoToDestination ();
	}

	public bool IsGoingTo (bool destination) {
		return this.goingToDestination == destination;
	}

	public bool OnHitTrap(BaseTrap trap) {
		if (trap is ManholeTrap) {
			if (!(this.hasRainBoots /*|| this.hasUmbrella*/))
            {
				StartCoroutine(ActiveHitAnimation(waterHit, defeatedWater, GoBackHome));
				//ActiveAnimation(defeatedWater);
				//GoBackHome();
            }
			this.onHitWater.Invoke();
			return true;
		} else if (trap is DoveTrap) {
			if (this.hasUmbrella)
            {
				if (hasRainBoots) ActiveAnimation(boot);
				else ActiveAnimation(baseSkin);
				this.hasUmbrella = false;
            }
			else
            {
				if (hasRainBoots) StartCoroutine(ActiveHitAnimation(shitHitBoot, defeatedShitBoot, GoBackHome));
				else StartCoroutine(ActiveHitAnimation(shitHit, defeatedShitBoot, GoBackHome));
				//GoBackHome();
			}
			this.onHitShit.Invoke();
			return true;
		} else if (trap is RatTrap) {
			if (this.hasUmbrella)
			{
				this.hasUmbrella = false;
				if (this.hasRainBoots) StartCoroutine(ActiveHitAnimation(ratDebuffBootUmbrella, boot, () => { }));//ActiveAnimation(boot);
				else StartCoroutine(ActiveHitAnimation(ratDebuffUmbrella, baseSkin, () => { }));
				this.onHitRat.Invoke();
				return true;
			}
			else if (this.hasRainBoots)
			{
				this.hasRainBoots = false;
				if (this.hasUmbrella) StartCoroutine(ActiveHitAnimation(ratDebuffBootUmbrella, umbrella, () => { }));
				else StartCoroutine(ActiveHitAnimation(ratDebuffBoot, baseSkin, () => { }));
				this.onHitRat.Invoke();
				return true;
			}
			else
				return false;
        }

		return false;
	}

	private void ActiveAnimation(Animator toActive)
    {
		active.gameObject.SetActive(false);
		toActive.gameObject.SetActive(true);
		active = toActive;
    }

	private IEnumerator ActiveHitAnimation(Animator hitToActive, Animator walkToActive, Action completeCallback)
    {
		Vector3 oldVec = rb.velocity;
		rb.velocity = Vector3.zero;
		GetComponent<Collider2D>().enabled = false;

		active.gameObject.SetActive(false);
		hitToActive.gameObject.SetActive(true);

		yield return new WaitForSeconds(hitToActive.GetCurrentAnimatorStateInfo(0).length);

		hitToActive.gameObject.SetActive(false);
		walkToActive.gameObject.SetActive(true);
		active = walkToActive;
		rb.velocity = oldVec;

		GetComponent<Collider2D>().enabled = true;

		completeCallback();
    }

	private bool RollChance(float chance) {
		return UnityEngine.Random.Range(0f, 1f) <= chance;
	}

	private void GoBackHome () {
		Vector3 direction = (startPoint.position - destinationPoint.position).normalized;
		this.rb.velocity = direction * this.speed;
		this.transform.localScale = new Vector3(-1, 1, 1);
		this.goingToDestination = false;
		this.onDefeat.Invoke();
	}

	private void GoToDestination () {
		Vector3 direction = (destinationPoint.position - startPoint.position).normalized;
		this.rb.velocity = direction * this.speed;
		this.transform.localScale = Vector3.one;
		this.goingToDestination = true;
	}
}