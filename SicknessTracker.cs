using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SicknessTracker : MonoBehaviour
{
    public GameObject headCam;
    public float motionSickness;
    public float avgMotionSickness;
    public int index;

    private int frames = 0;
    private Vector3[] headRots = new Vector3[3600];
    private float rollTotal = 0;
    private float pitchTotal = 0;
    private float rollAvg = 0;
    private float pitchAvg = 0;
    private int i = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        

        if (i > 3600)
        {
            i = 0;
        }

        if (frames % 10 == 0)
        {
            headRots[i] = headCam.transform.eulerAngles * 0.0174533f;
            float pitch = headRots[i].x;
            float roll = headRots[i].z;

            rollTotal += roll;
            pitchTotal += pitch;
            rollAvg = rollTotal/(i + 1);
            pitchAvg = pitchTotal / (i + 1);

            float rollSumProcessed = 0.0f;
            float pitchSumProcessed = 0.0f;
            foreach (Vector3 j in headRots)
            {
                pitchSumProcessed += Mathf.Pow((j.x - pitchAvg), 2);
                rollSumProcessed += Mathf.Pow((j.z - rollAvg), 2);
            }

            motionSickness = Mathf.Sqrt(rollSumProcessed + pitchSumProcessed) / (i + 1);
            avgMotionSickness = motionSickness / (i + 1);
            index = i;

            i++;

        }
    }
}
