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

    void Start()
    {

    }

    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        if (gameObject.activeSelf)
        {
            currMaterial.color = Color.Lerp(minTransColor, maxTransColor, lerp);
        }
        
    }
}
