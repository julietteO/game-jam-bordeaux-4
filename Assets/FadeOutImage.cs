using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeOutImage : MonoBehaviour {
	void Start () {
		GetComponent<Image>().DOFade(0f, 1f);
	}
}
