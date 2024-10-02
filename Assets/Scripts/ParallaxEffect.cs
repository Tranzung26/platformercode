using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxEffect : MonoBehaviour
{
    public Camera Camera;             // Reference to the camera in the scene
    public Transform FollowTarget;    // The object that the camera is following

    Vector2 _startingPosition;        // The original position of the object
    float _startingZ;                 // The original Z value of the object

    // Get the Z distance between the object and the follow target
    float zDistanceFromTarget => transform.position.z - FollowTarget.position.z;

    // Determine which clipping plane to use (near or far) based on distance
    float clippingPlane => Camera.transform.position.z + (zDistanceFromTarget > 0 ? Camera.farClipPlane : Camera.nearClipPlane);

    // Calculate how far the camera has moved from the object's start position
    Vector2 CameraMoveSinceStart => (Vector2)Camera.transform.position - _startingPosition;

    // Parallax effect multiplier based on the object's distance from the camera
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget / clippingPlane);

    // Called once at the start of the game
    void Start()
    {
        _startingPosition = transform.position;  // Save the object's starting position
        _startingZ = transform.position.z;       // Save the object's starting Z value
    }

    // Called once per frame
    void Update()
    {
        // Calculate the new position based on how far the camera has moved
        Vector2 newPosition = _startingPosition + CameraMoveSinceStart * parallaxFactor;

        // Update the object's position with the new X and Y, but keep the original Z
        transform.position = new Vector3(newPosition.x, newPosition.y, _startingZ);
    }
}
