using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;

    SplitMesh[] objectives;

    int _score;

    public int levelIndex = 0;

    public AudioSource transitionSound;

    public int score {
        get {
            return _score;
        }
        set {
            if(_score != value) {
                _score = value;
                if(onScoreChange != null) {
                    onScoreChange(_score, maxScore);
                }
                if(_score == maxScore) {
                    EndLevel();
                }
            }
        }
    }

    public int maxScore {
        get {
            return objectives.Length;
        }
    }

    public event Action<int, int> onScoreChange;

    void Awake() {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        objectives = FindObjectsOfType<SplitMesh>();
        score = 0;

        Debug.Log("current level is "+levelIndex);
        Debug.Log("objectives : "+objectives.Length);
    }

    void Start() {
        instance = this;

        foreach(var objective in objectives) {
            objective.onSplit += () => score++;
        }
        score = 0;
    }

    public void EndLevel() {
        var tmp = GameObject.Find("Black Screen");
        var blackScreen = tmp.GetComponent<Image>();

        var player1 = GameObject.FindObjectOfType<Theme1Player>();
        if(player1 != null)
            player1.Stop();
        var player2 = GameObject.FindObjectOfType<Theme2Player>();
        if(player2 != null)
            player2.Stop();
        transitionSound.Play();

        blackScreen.DOFade(1f, 1f).SetDelay(5f).OnComplete(() => {
            StartCoroutine(GoToNextLevel());
        });
    }

    IEnumerator GoToNextLevel() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(++levelIndex);
    }
}