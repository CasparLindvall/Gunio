﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject player;
    public int xMin;
    public int xMax;  
    public int yMin;
    public int yMax;

    // Start is called before the first frame update
    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GameManager.instance.GetState() == GameState.Playing && player != null)
        {
            // new camera pos
            float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
            float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }

    }
}
