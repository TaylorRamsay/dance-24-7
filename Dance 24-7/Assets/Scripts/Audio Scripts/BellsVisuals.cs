using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BellsVisuals : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter track;
    public string csvPath;

    public GameObject currentNPC;
    private Color instrumentGlow;

    private List<float> musicData = new List<float>();
    private int currTime = 0;
    private int totalTime = 0;
    private int index = 0;
    public float multiplier = 1;

    public int materialIndex = 0;

    public Material e2Mat;
    public Material c2Mat;
    public Material a2Mat;
    public Material fMat;
    public Material e1Mat;
    public Material d1Mat;
    public Material c1Mat;
    public Material a1Mat;
    public Material gMat;
    public Material bMat;
    public Material d2Mat;

    public List<Material> melodyMaterial;

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
    public void DisplayMelody()
    {
 
    }

    void Start()
    {
        ReadCSV(ref musicData);
        track.EventDescription.getLength(out totalTime);
        melodyMaterial = new List<Material> {e2Mat, c2Mat, a2Mat, fMat, e1Mat, d1Mat, c1Mat, c2Mat, a2Mat, fMat, e1Mat, d1Mat, c1Mat, a1Mat, e2Mat, c2Mat, a2Mat, fMat, e1Mat, d1Mat, c1Mat, c2Mat, a2Mat, fMat, e1Mat, d1Mat, c1Mat, a1Mat, e1Mat, gMat, bMat, d2Mat, bMat, gMat, e1Mat, gMat, bMat, d2Mat, bMat, gMat, e1Mat, gMat, bMat, c2Mat, bMat, gMat, e1Mat, gMat, bMat, c2Mat, bMat, gMat, e1Mat, gMat, bMat, d2Mat, bMat, gMat, e1Mat, gMat, bMat, d2Mat, bMat, gMat, e1Mat, gMat, bMat, c2Mat, bMat, gMat, e1Mat, gMat, bMat, c2Mat, bMat, gMat};

    }



    void Update()
    {
        track.EventInstance.getTimelinePosition(out currTime);

        index = (int)(((float)currTime / (float)totalTime) * musicData.Count);

        instrumentGlow = melodyMaterial[materialIndex].GetColor("_Color");

        instrumentGlow *= (musicData[index] * multiplier);

        melodyMaterial[materialIndex].SetColor("_EmissionColor", instrumentGlow);

        if (musicData[index] == 5 && musicData[index + 1] == .0001)
        {
            materialIndex++;
        }

    }
}
