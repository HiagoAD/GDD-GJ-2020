using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				ActiveAnimation(defeatedWater);
				GoBackHome();
            }
			return true;
		} else if (trap is DoveTrap) {
			if (this.hasUmbrella)
				this.hasUmbrella = false;
			else
            {
				ActiveAnimation(defeatedShitBoot);
				GoBackHome();
			}

			return true;
		} else if (trap is RatTrap) {
			if (this.hasUmbrella)
			{
				this.hasUmbrella = false;
				if (this.hasRainBoots) ActiveAnimation(boot);
				else ActiveAnimation(baseSkin);
				return true;
			}
			else if (this.hasRainBoots)
			{
				this.hasRainBoots = false;
				if (this.hasUmbrella) ActiveAnimation(umbrella);
				else ActiveAnimation(baseSkin);
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

	private bool RollChance(float chance) {
		return Random.Range(0f, 1f) <= chance;
	}

	private void GoBackHome () {
		Vector3 direction = (startPoint.position - destinationPoint.position).normalized;
		this.rb.velocity = direction * this.speed;
		this.transform.localScale = new Vector3(-1, 1, 1);
		this.goingToDestination = false;
		GetComponent<Collider2D>().enabled = false;
	}

	private void GoToDestination () {
		Vector3 direction = (destinationPoint.position - startPoint.position).normalized;
		this.rb.velocity = direction * this.speed;
		this.transform.localScale = Vector3.one;
		this.goingToDestination = true;
	}
}