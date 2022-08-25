using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatBarrier : MonoBehaviour
{
    public Color currColor;
    public Material currMaterial;
    public float minTransparency;
    public float maxTransparency;
    public Color minTransColor;
    public Color maxTransColor;
    public float opacityTime;
    public float speed = 1;
    public Color from;

    void Start()
    {
        currColor = minTransColor;
        from = currMaterial.color;
    }

    void Update()
    {
        opacityTime = 0;
        if (gameObject.activeSelf)
        {
            if (currMaterial.color.a < maxTransColor.a)
            {
                opacityTime += Time.deltaTime * speed;
                //currentTransparency = Mathf.Lerp(minTransparency, maxTransparency, 1);
                print("Increasing");
                print(from);
                //SetColor
                currMaterial.color = Color.Lerp(from, maxTransColor, opacityTime);
                print(currMaterial.color);
            }
            if (currMaterial.color.a > minTransColor.a)
            {
                opacityTime -= Time.deltaTime * speed;
                //currentTransparency = Mathf.Lerp(maxTransparency, minTransparency, 1);
                print("Decreasing");
                //SetColor
                currMaterial.color = Color.Lerp(from, minTransColor, opacityTime);
            }
        }
        
    }
}
