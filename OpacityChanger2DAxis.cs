using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OpacityChanger2DAxis : MonoBehaviour
{
    public bool blinderToggle = true;
    public bool directionTrack = true;
    public bool resizing = true;
    public bool blurred = true;
    public Material target;
    public GameObject blinder;
    public Vector3 defaultSize = new Vector3(16,13,1);
    public float transparency;
    [Range(0, 30)]
    public int movementFactor;
    [Range(0, 15)]
    public int resizingFactor;

    public void UpdateOpacity(Vector2 alphaValueBefore)
    {
        float alphaValue = alphaValueBefore.magnitude;
        Color color = target.color;
        color.a = alphaValue;
        target.color = color;
        transparency = alphaValue;
    }

    private void Update()
    {
        if (blinderToggle)
        {
            blinder.SetActive(true);
            float yy = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical");
            float xx = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickHorizontal");
            Vector2 alphaVect = new Vector2(xx, yy);
            float alphaValue = alphaVect.magnitude;
            float newRadius = 1 + (alphaValue * 5);
            
            
            //Option for turning off radius editing vs. alpha editing
            if (blurred)
            {
                int radiusID = Shader.PropertyToID("_Radius");
                target.SetFloat(radiusID, newRadius);
            } else
            {
                Color color = target.color;
                transparency = alphaValue;
                if (alphaValue > 0)
                {
                    color.a = 1;
                    target.color = color;
                } else
                {
                    color.a = 0;
                    target.color = color;
                }
            }
            
            // Option for moving the blinder according to thumbstick location
            if (directionTrack)
            {
                blinder.GetComponent<RectTransform>().transform.localPosition = new Vector3(xx * movementFactor, yy * movementFactor, 0);
            }

            // Option for resizing according to speed.
            float alph = alphaValue * resizingFactor;
            blinder.GetComponent<RectTransform>().transform.localScale = new Vector3(defaultSize.x - alph, defaultSize.y - alph, 1);
        }
        else
        {
            blinder.SetActive(false);
        }
        
        
    }
}
