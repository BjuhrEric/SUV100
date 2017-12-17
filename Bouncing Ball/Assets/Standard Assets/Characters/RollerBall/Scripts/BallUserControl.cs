using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class BallUserControl : MonoBehaviour
    {
        private Ball ball; // Reference to the ball controller.
        private bool jump; // whether the jump button is currently pressed


        private void Awake()
        {
            // Set up the reference.
            ball = GetComponent<Ball>();
        }


        private void Update()
        {
            jump = CrossPlatformInputManager.GetButton("Jump");
        }


        private void FixedUpdate()
        {
            ball.Move(jump);
            jump = false;
        }
    }
}
