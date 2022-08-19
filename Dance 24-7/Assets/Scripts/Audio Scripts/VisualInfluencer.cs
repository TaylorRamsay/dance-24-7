using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// index = (int)((currentPlaybackTime / TotalTime) * csvArraySize);
// the first half gets some playback ratio for you to then convert to an index based on the size of your processed CSV data
// you really only need to do this during a monobehaviour's script (once per frame
// Iff you want to detect local maxima, you'll need to keep some values around to use as a part of the evaluation.
// the int cast being on the full evaluation is key; if you cast the ratio calculation to int, the expression will always evaluate to 0.
// the more sample points you have in your CSV, the more your effects will line up.
// As an additional suggestion for your own testing purposes, I'd recommend you come up with a few, differently grained data sets for the same piece of music.
// Give yourself the ability to swap those data sets in so you can evaluate the look/feel on the fly.
// It will help you make an informed decision on what data sizes "feel" best

public class VisualInfluencer : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter track;
    public string csvPath;

    public GameObject currentNPC;
    public Material characterMaterial;
    public Material instrumentMaterial;
    private Color characterGlow;
    private Color instrumentGlow;

    private List<float> musicData = new List<float>();
    private int currTime = 0;
    private int totalTime = 0;
    private int index = 0;
    public float multiplier = 1;

    void ReadCSV(ref List<float> csvList)
    {
        if (File.Exists(csvPath))
        { 
            var lines = File.ReadLines(csvPath);
            foreach (var line in lines)
            { 
                if (!string.IsNullOrWhiteSpace(line))
                {
                    csvList.Add(float.Parse(line));
                }
            }   
        }

    }

    void Start()
    {
        ReadCSV(ref musicData);
        track.EventDescription.getLength(out totalTime);
        characterMaterial.GetColor("_EmissionColor");
        instrumentMaterial.GetColor("_EmissionColor");
    }

    void Update()
    {
        //print("------------Current Visual Influence Info (Chords)---------------");
        //print("Current time:" + currTime + "\n\tTotal time: " + totalTime);

        track.EventInstance.getTimelinePosition(out currTime);
        index = (int)(((float)currTime / (float)totalTime) * musicData.Count);

        if (characterMaterial.name == "bassDrumMaterial")
        {
            print("Current index: " + index);
            print("List Size: " + musicData.Count);
            print("Current List Item: " + musicData[index]);
        } 

        characterGlow = characterMaterial.GetColor("_Color");
        instrumentGlow = instrumentMaterial.GetColor("_Color");

        characterGlow *= (musicData[index] * multiplier);
        instrumentGlow *= (musicData[index] * multiplier);

        characterMaterial.SetColor("_EmissionColor", characterGlow);
        instrumentMaterial.SetColor("_EmissionColor", instrumentGlow);
    }
}
