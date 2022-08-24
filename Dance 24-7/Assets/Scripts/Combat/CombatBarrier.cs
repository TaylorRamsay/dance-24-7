using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatBarrier : MonoBehaviour
{
    private Color currColor;

    public bool isTransparent;
    public float currentTransparency;
    public float minTransparency;
    public float maxTransparency;
    public Color minTransColor;
    public Color maxTransColor;

    void Start()
    {
        currColor = gameObject.GetComponent<Material>().color;
        currentTransparency = 25;
        minTransColor = new Color(currColor.r, currColor.g, currColor.b, minTransparency);
        maxTransColor = new Color(currColor.r, currColor.g, currColor.b, maxTransparency);

    }

    void Update()
    {
        if (gameObject.GetComponent<NavMeshObstacle>().enabled == true)
        {
            while (currentTransparency < maxTransparency)
            {
                gameObject.GetComponent<Material>().color = Color.Lerp(minTransColor, maxTransColor, 1);
            }
            while (currentTransparency > minTransparency)
            {
                gameObject.GetComponent<Material>().color = Color.Lerp(maxTransColor, minTransColor, 1);
            }
        }
    }
}
