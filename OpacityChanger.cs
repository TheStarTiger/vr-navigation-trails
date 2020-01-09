using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OpacityChanger : MonoBehaviour
{
    public Material target;
    public float transparency;
    public void UpdateOpacity(float alphaValue)
    {
        Color color = target.color;
        color.a = alphaValue;
        target.color = color;
        transparency = alphaValue;
    }
}
