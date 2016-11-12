using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour {

	[SerializeField] string format;

	void Start () {
		var text = GetComponent<Text>();
		ScoreManager.instance.onScoreChange += (score, max) => {
			text.text = string.Format(format, score, max);
		};

		text.text = string.Format(format, ScoreManager.instance.score, ScoreManager.instance.maxScore);
	}
}
