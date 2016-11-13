using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlobouTextManager : MonoBehaviour {
	public static BlobouTextManager instance;

	string[][] texts = new string[][]{
		new string[] {
			"(psst, tu peux appuyer sur la gachette arrière pour discuter)",
			"C'est bien toi, tu es le Peintre !",
			"Tu peux m'aider ?",
			"Je ne peux plus être moi même...",
			"Ces règles veulent absolument me faire rentrer dans un moule",
			"Mais je suis pas du genre \"angles droits\" moi, tu vois ?",
			"Je pense que tu peux les détruire avec ton Briseur"
		},
		new string[] {
			"Oh ? Dans ce parc aussi il y en a ?",
			"Je pense que ça ne sera pas aussi facile"
		},
		new string[] {
		}
	};

	int textIndex = 0;
	public int level = 0;
	public Text text;

	void Awake() {
		instance = this;
	}

	void Start () {
		level = SceneManager.GetActiveScene ().buildIndex;
		text.text = texts[level][textIndex];	
	}

	public void nextText() {
		if (textIndex < texts[level].Length) {
			textIndex++;
			text.text = texts[level][textIndex];
		} else {
			gameObject.SetActive(false);
		}
	}

}
