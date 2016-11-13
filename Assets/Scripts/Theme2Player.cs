using UnityEngine;
using System.Collections.Generic;

public class Theme2Player : MonoBehaviour {

    Instrument[] instruments;
    List<FadeIn> audioFadeIn;
    AudioSource mainSource;
    AudioSource secondLoopSource;
    int nextInstrumentIndex;
    bool secondLoopEnabled = false;
    bool onSecondLoop = false;

    // Use this for initialization
    void Start() {

        instruments = new Instrument[] {
            new Instrument(new string[] { "Ins_1", "Ins_1_2" }),
            new Instrument(new string[] { "Ins_2" }),
            new Instrument(new string[] { "Ins_3", "Ins_3_2" }),
            new Instrument(new string[] { "Ins_4", "Ins_4_2" }),
            new Instrument(new string[] { "Ins_5", "Ins_5_2" }),
            new Instrument(new string[] { "Ins_6", "Ins_6_2"}),
            new Instrument(new string[] { "Ins_7", "Ins_7_2"}),
        };

        foreach(Instrument currentInstrument in instruments) {
            for (int i = 0; i < currentInstrument.names.Length; i++) {
                currentInstrument.gameObjects[i] = GameObject.Find(currentInstrument.names[i]);
            }
        }

        audioFadeIn = new List<FadeIn>();

        mainSource = (AudioSource) instruments[0].gameObjects[0].GetComponent("AudioSource");
        mainSource.Play();

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

    void Update() {
        foreach (FadeIn fadeIn in audioFadeIn) {
            if (fadeIn.IsIncreasing()) {
                fadeIn.Update();
            } else {
                audioFadeIn.Remove(fadeIn);
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
}

