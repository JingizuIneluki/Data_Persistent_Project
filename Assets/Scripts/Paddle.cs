using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    private float Speed ;
    public float MaxMovement = 2.0f;
    private Color playerColor;
    private Renderer playerMesh;
    private Material playerMat;
    
    // Start is called before the first frame update
    void Start()
    {
        
        playerMesh = GetComponent<Renderer>();
        playerMat = playerMesh.material;
        /*if (GameManager.Instance.TeamColor != null)
        {
            playerColor = GameManager.Instance.TeamColor;
        }
        else
        {
            playerColor = Color.red;
        }*/
        playerColor = GameManager.Instance.TeamColor;
        playerMesh.material.color = playerColor;
        playerMat.SetColor("_EmissionColor", playerColor);
        Speed = GameManager.Instance.speedPlayer;

    }

    // Update is called once per frame
    void Update()
    {
        Speed = GameManager.Instance.speedPlayer;
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }
}
