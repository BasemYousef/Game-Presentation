using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AyaOmar
{
    public class WaypointMovement : MonoBehaviour
    {
        public Transform movePoint;  // Array to hold the waypoints
        public float speed = 5f;       // Speed at which the object moves

        private void Update()
        {
            Move();
        }
        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
            Vector3 targetPosition = movePoint.position; // Get the target's position
            targetPosition.y = transform.position.y; // Restrict the target position to the same Y-axis as the object

            transform.LookAt(targetPosition);
            // Check if the object has reached the current waypoint
            
        }
    }
}
