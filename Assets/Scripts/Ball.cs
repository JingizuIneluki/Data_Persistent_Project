using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float velocityBall = .001f;
    private Rigidbody m_Rigidbody;
   
    private Color ballColor;
    private Renderer ballMesh;
    private Material ballMat;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        ballMesh = GetComponent<Renderer>();
        ballMat = ballMesh.material;
      
        ballColor = GameManager.Instance.TeamColor;
        ballMesh.material.color = ballColor;
        ballMat.SetColor("_EmissionColor", ballColor);
    }
    
    private void OnCollisionExit(Collision other)
    {

        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * velocityBall;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > GameManager.Instance.ballMaxVelocity)
        {
            velocity = velocity.normalized * GameManager.Instance.ballMaxVelocity;
        }

        m_Rigidbody.velocity = velocity;
    }
}
