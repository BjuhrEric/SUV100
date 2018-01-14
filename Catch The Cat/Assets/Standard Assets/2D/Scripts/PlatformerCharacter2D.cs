using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 20f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private Transform animatedObject;
        [SerializeField] private List<Rigidbody> rigidBodies;

        private Animator animator;            // Reference to the player's animator component.
        private Rigidbody rigidBody;
        private bool facingRight = true;  // For determining which way the player is currently facing.

        private void Awake()
        {
            animator = animatedObject.GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
        }

        public void Move(float move)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            animator.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            rigidBody.velocity = new Vector3(move*maxSpeed, 0, 0);
            /*Vector3 v = new Vector3(move * maxSpeed, 0, 0);
            foreach (Rigidbody rb in rigidBodies)
            {
                rb.velocity = v;
            }*/

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !facingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = animatedObject.localScale;
            theScale.x *= -1;
            animatedObject.localScale = theScale;
        }
    }
}
