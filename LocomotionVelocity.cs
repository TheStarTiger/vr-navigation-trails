using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionVelocity : MonoBehaviour
{
    public GameObject player;
    public void updateVelocity(Vector2 vector)
    {
//        Rigidbody rigid = player.GetComponent<Rigidbody>();
        player.transform.Translate(vector.normalized);
//        if (rigid.velocity.magnitude < vector.normalized.magnitude)
//        {
//            rigid.AddRelativeForce(vector.normalized, ForceMode.Force);
//        }
    } 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
