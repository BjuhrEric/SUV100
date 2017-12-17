using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        [SerializeField] private float m_JumpPower = 2; // The force added to the ball when it jumps.

        private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody m_Rigidbody;
        private Vector3 startPosition;

        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            startPosition = gameObject.transform.position;
            // Set the maximum angular velocity.
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
        }


        public void Move(bool jump)
        {
            if (jump)
            {
                m_Rigidbody.useGravity = false;
                m_Rigidbody.AddForce(Vector3.up*m_JumpPower, ForceMode.Impulse);
            }
            else
            {
                m_Rigidbody.useGravity = true;
            }
        }

        public void ResetPosition()
        {
            gameObject.transform.position = startPosition;
        }
    }
}
