﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class UserInput : MonoBehaviour
    {
        public PlayerController player;

        // Update is called once per frame
        void Update()
        {
            // Get input axis from Unity Input Manager
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            // Tell player to move in those directions
            player.Move(inputH, inputV);
        }
    }
}

