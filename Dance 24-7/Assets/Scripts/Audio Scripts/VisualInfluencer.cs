using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    // Reads audio data stored in .csv file created from .wav-file-extractor python scripts, audio is stored csvList to influence visual appearance of
    // in-game objects
    void ReadCSV(ref List<float> csvList)
    {
        if (File.Exists(Application.streamingAssetsPath + csvPath))
        {
            var lines = File.ReadLines(Application.streamingAssetsPath + csvPath);
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
    }

    // This is where the processing of audio data happens, the data read from csvList is used to change the emission (glow) intensity of a game object
    // Index is based on a ratio calculated on current track time over total track time
    void Update()
    {
        track.EventInstance.getTimelinePosition(out currTime);

        index = (int)(((float)currTime / (float)totalTime) * musicData.Count);

        characterGlow = characterMaterial.GetColor("_Color");
        instrumentGlow = instrumentMaterial.GetColor("_Color");

        characterGlow *= (musicData[index] * multiplier);
        instrumentGlow *= (musicData[index] * multiplier);

        characterMaterial.SetColor("_EmissionColor", characterGlow);
        instrumentMaterial.SetColor("_EmissionColor", instrumentGlow);
    }
}
