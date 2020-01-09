using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavRailMesh : MonoBehaviour
{
    public Vector3 hitPoint;
    public GameObject forward;
    public Vector3 playerLocation;
    public LineRenderer lineDad;
    public float lineWidth = 1.0f;
    public int pathLength;
    public Vector3 alphaVect;
    public Vector3 navPlayerPosition;
    public Vector3 navRayPosition;
    public Vector3 forwardCast = new Vector3(0,0,9f);
    public bool useRaycast = false;
    public float lineHoverHeight = 0.2f;
    public int altRange = 10;

    private NavMeshPath path;
    private Vector3 castPoint;
    private Vector3 navCastPoint;
    private Ray ray;
    private RaycastHit hit;
    private NavMeshHit navPlayLocation;
    private NavMeshHit navHit;
    private NavMeshHit navCastHit;
    private NavMeshHit navWorldCastHit;


    void Start()
    {
        lineDad.useWorldSpace = true;
        lineDad.endColor = Color.yellow;
        lineDad.startColor = Color.red;
        lineDad.endWidth = lineWidth;
        lineDad.startWidth = lineWidth;
        path = new NavMeshPath();
    }

    void Update()
    {
        float yy = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical");
        float xx = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickHorizontal");
        alphaVect = new Vector3(xx, -0.1f, yy);

        if (alphaVect.magnitude != 0.5)
        {
            playerLocation = this.transform.position;
            lineDad.gameObject.SetActive(true);


            if (useRaycast)
            {
                lineDad.useWorldSpace = true;
                alphaVect = new Vector3(xx, -0.1f, yy);
                ray = new Ray(playerLocation, forward.transform.TransformDirection(alphaVect));
                

                if (Physics.Raycast(ray, out hit))
                {
                    hitPoint = hit.point;
                    NavMesh.SamplePosition(playerLocation, out navPlayLocation, 5.0f, NavMesh.AllAreas);
                    NavMesh.SamplePosition(hitPoint, out navHit, 50.0f, NavMesh.AllAreas);

                    navPlayerPosition = navPlayLocation.position;
                    navRayPosition = navHit.position;

                    NavMesh.CalculatePath(navPlayLocation.position, navHit.position, NavMesh.AllAreas, path);

                    lineDad.positionCount = path.corners.Length;
                    lineDad.SetPositions(path.corners);

                    Debug.DrawRay(playerLocation, hitPoint, Color.green);

                    pathLength = path.corners.Length;
                }
            }
            else
            {
                //alphaVect = new Vector3(xx, 0.0f, yy);
                castPoint = forward.transform.TransformDirection(alphaVect) * altRange;
                Vector3 worldCastPoint = castPoint + forward.transform.position;
                NavMesh.SamplePosition(playerLocation, out navPlayLocation, 5.0f, NavMesh.AllAreas);
                NavMesh.SamplePosition(castPoint, out navCastHit, 5.0f, NavMesh.AllAreas);
                NavMesh.SamplePosition(worldCastPoint, out navWorldCastHit, 10.0f, NavMesh.AllAreas);

                navCastPoint = navCastHit.position;

                NavMesh.CalculatePath(playerLocation, navWorldCastHit.position, NavMesh.AllAreas, path);

                for (int i = 0; i < path.corners.Length; i++)
                {
                    path.corners[i].y += lineHoverHeight;
                }

                lineDad.positionCount = path.corners.Length;
                lineDad.SetPositions(path.corners);

                Debug.DrawRay(navPlayLocation.position, navCastHit.position, Color.green);

                pathLength = path.corners.Length;
            }
            //Alt version of the raycast
            
        }
        else
        {
            lineDad.gameObject.SetActive(false);
        }
    }
}

//The line debug works, it's the rest that doesn't. Double check the path making and the line renderer.
