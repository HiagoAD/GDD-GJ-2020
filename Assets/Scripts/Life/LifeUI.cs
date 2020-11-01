using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {
	[SerializeField] private Sprite lifeFullIcon = null;
	[SerializeField] private Sprite lifeSlotIcon = null;
	[SerializeField] private Image[] lifeIcons;

	public void UpdateLifeCount (int livesLeft) {
		for (int i = 0; i < this.lifeIcons.Length; i++) {
			if (i < livesLeft)
				this.lifeIcons[i].sprite = this.lifeFullIcon;
			else
				this.lifeIcons[i].sprite = this.lifeSlotIcon;
		}
	}
}