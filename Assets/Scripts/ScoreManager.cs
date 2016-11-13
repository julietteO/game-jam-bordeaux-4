using System;
using UnityEngine;

class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;

    SplitMesh[] objectives;

    int _score;
    
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
        instance = this;
        objectives = FindObjectsOfType<SplitMesh>();
        foreach(var objective in objectives) {
            objective.onSplit += () => score++;
        }
    }
}