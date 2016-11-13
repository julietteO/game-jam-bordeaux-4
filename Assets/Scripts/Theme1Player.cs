using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Instrument {
    public string[] names;
    public GameObject[] gameObjects;

    public Instrument(string[] names) {
        this.names = names;
        this.gameObjects = new GameObject[this.names.Length];
    }
};

public class FadeIn {

    private AudioSource audio;
    public float currentVolume;

    public FadeIn(AudioSource audio) {
        this.audio = audio;
        audio.volume = 0;
    }

    public void Update() {
        currentVolume += (float)(0.1 * Time.deltaTime);
        audio.volume = currentVolume;
    }

    public bool IsIncreasing() {
        return audio.volume <= 1;
    }
}

public class FadeOut {

    private AudioSource audio;
    public float currentVolume;

    public FadeOut(AudioSource audio) {
        this.audio = audio;
        currentVolume = audio.volume;
    }

    public void Update() {
        currentVolume -= (float)(0.1 * Time.deltaTime);
        audio.volume = currentVolume;
    }

    public bool IsDecreasing() {
        return audio.volume <= 0;
    }
}

public class Theme1Player : MonoBehaviour {

    Instrument[] instruments;
    List<FadeIn> audioFadeIn;
    List<FadeOut> audioFadeOut;
    AudioSource mainSource;
    AudioSource secondLoopSource;
    int nextInstrumentIndex;
    bool secondLoopEnabled = false;
    bool onSecondLoop = false;
    bool isPlaying = false;
    bool isStopping = false;

    // Use this for initialization
    void Start() {

        instruments = new Instrument[] {
            new Instrument(new string[] { "Ins_1" }),
            new Instrument(new string[] { "Ins_2" }),
            new Instrument(new string[] { "Ins_3" }),
            new Instrument(new string[] { "Ins_4" }),
            new Instrument(new string[] { "Ins_5" }),
            new Instrument(new string[] { "Ins_6", "Ins_6_2"})
        };

        foreach(Instrument currentInstrument in instruments) {
            for (int i = 0; i < currentInstrument.names.Length; i++) {
                currentInstrument.gameObjects[i] = GameObject.Find(currentInstrument.names[i]);
            }
        }

        audioFadeIn = new List<FadeIn>();
        audioFadeOut = new List<FadeOut>();

        mainSource = (AudioSource) instruments[0].gameObjects[0].GetComponent("AudioSource");
        mainSource.Play();
        isPlaying = true;

        nextInstrumentIndex = 1;

        ScoreManager.instance.onScoreChange += (score, max) => {
            int mustStartInstrumentUntil = (int)(score * instruments.Length / (double)max);

            while (nextInstrumentIndex <= mustStartInstrumentUntil)
                this.PlayNextInstrument();
        };

    }

    void PlayNextInstrument() {
        if (nextInstrumentIndex < instruments.Length) {
            AudioSource nextInstrument = (AudioSource)instruments[nextInstrumentIndex].gameObjects[0].GetComponent("AudioSource");
            audioFadeIn.Add(new FadeIn(nextInstrument));
            nextInstrument.timeSamples = mainSource.timeSamples;
            nextInstrument.Play();

            if (nextInstrumentIndex == 5) {
                this.EnableSecondLoop();
            }
        }
        nextInstrumentIndex++;
    }

    void EnableSecondLoop() {
        foreach (Instrument currentInstrument in instruments) {
            if (currentInstrument.gameObjects.Length == 2) {
                AudioSource currentSource = (AudioSource)currentInstrument.gameObjects[0].GetComponent("AudioSource");
                if (secondLoopSource == null) {
                    secondLoopSource = currentSource;
                }
                
                currentSource.loop = false;
            }
        }

        secondLoopEnabled = true;
    }

    void PlayFirstLoop() {
        if (isStopping) {
            return;
        }

        secondLoopSource = null;
        onSecondLoop = false;

        foreach (Instrument currentInstrument in instruments) {
            if (currentInstrument.gameObjects.Length == 2) {
                AudioSource currentSource = (AudioSource)currentInstrument.gameObjects[0].GetComponent("AudioSource");

                if (secondLoopSource == null) {
                    secondLoopSource = currentSource;
                }

                currentSource.timeSamples = mainSource.timeSamples;
                currentSource.Play();
            }
        }
    }

    void PlaySecondLoop() {
        if (isStopping) {
            return;
        }

        secondLoopSource = null;
        onSecondLoop = true;

        foreach (Instrument currentInstrument in instruments) {
            if (currentInstrument.gameObjects.Length == 2) {
                AudioSource currentSource = (AudioSource)currentInstrument.gameObjects[1].GetComponent("AudioSource");

                if (secondLoopSource == null) {
                    secondLoopSource = currentSource;
                }

                currentSource.timeSamples = mainSource.timeSamples;
                currentSource.Play();
            }
        }
    }

    bool IsPlaying() {
        return isPlaying;
    }

    void Update() {
        if(isStopping) {
            return;
        }

        foreach (FadeIn fadeIn in audioFadeIn) {
            if (fadeIn.IsIncreasing()) {
                fadeIn.Update();
            } else {
                audioFadeIn.Remove(fadeIn);
            }
        }

        foreach (FadeOut fadeOut in audioFadeOut) {
            if (fadeOut.IsDecreasing()) {
                fadeOut.Update();
            }
        }

        if (secondLoopEnabled && !secondLoopSource.isPlaying) {
            if (onSecondLoop) {
                PlayFirstLoop(); 
            } else {
                PlaySecondLoop();
            }
        }
    }

    public void Stop() {
        foreach (Instrument currentInstrument in instruments) {
            if (currentInstrument.gameObjects.Length == 2 && onSecondLoop) {
                AudioSource currentSource = (AudioSource)currentInstrument.gameObjects[1].GetComponent<AudioSource>();
                DOTween.To(v => currentSource.volume = v, 1f, 0f, 1.5f); 
            }
            else {
                AudioSource currentSource = (AudioSource)currentInstrument.gameObjects[0].GetComponent<AudioSource>();
                DOTween.To(v => currentSource.volume = v, 1f, 0f, 1.5f); 
            }
        }

        isStopping = true;
    }
}

