using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

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
        //Debug.Log("Reading CSV");
        //Debug.Log("CSV Path:" + Application.streamingAssetsPath + csvPath);
        //Debug.Log("File Exists: " + File.Exists(Application.streamingAssetsPath + csvPath));
        if (File.Exists(Application.streamingAssetsPath + csvPath))
        {
            //Debug.Log("File Exists");
            var lines = File.ReadLines(Application.streamingAssetsPath + csvPath);
            foreach (var line in lines)
            {
                //Debug.Log("Reading Lines");
                if (!string.IsNullOrWhiteSpace(line))
                {
                    //Debug.Log("Line read");
                    csvList.Add(float.Parse(line));
                }
            }   
        }
        //Debug.Log("CSV read successfully");
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
        track.EventInstance.getTimelinePosition(out currTime);

        index = (int)(((float)currTime / (float)totalTime) * musicData.Count);

        /*if (characterMaterial.name == "playerTestGlow")
        {
            Debug.Log("List Size: " + musicData.Count);
            Debug.Log("Current Track time: " + currTime);
            Debug.Log("Total track time: " + totalTime);
            Debug.Log("Current Index: " + index * 2);
            Debug.Log("Character Emission color: " + characterMaterial.GetColor("_EmissionColor"));
        }
        */
        characterGlow = characterMaterial.GetColor("_Color");
        instrumentGlow = instrumentMaterial.GetColor("_Color");

        characterGlow *= (musicData[index] * multiplier);
        instrumentGlow *= (musicData[index] * multiplier);

        characterMaterial.SetColor("_EmissionColor", characterGlow);
        instrumentMaterial.SetColor("_EmissionColor", instrumentGlow);
        
    }
}
