using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatBarrier : MonoBehaviour
{
    public Material currMaterial;
    public Color minTransColor;
    public Color maxTransColor;
    public float duration = 1f;


    // Linearly interpolates between two values, to shift the transparenct of the combat arena to give it a pulsing effect
    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        if (gameObject.activeSelf)
        {
            currMaterial.color = Color.Lerp(minTransColor, maxTransColor, lerp);
        }
        
    }
}
