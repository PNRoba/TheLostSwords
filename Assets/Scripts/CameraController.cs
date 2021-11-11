using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Tilemap map;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        //target = PlayerController.instance.transform;
        target = FindObjectOfType<PlayerController>().transform;

        // gets current height of camera, calculates half(for boundaries)
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect; // calculates halfWidth with aspect ratio

        bottomLeftLimit = map.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = map.localBounds.max - new Vector3(halfWidth, halfHeight, 0f);

        PlayerController.instance.SetBoundaries(map.localBounds.min, map.localBounds.max);
    }

    // LateUpdate is called once per frame after update (smoother for camera)
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // keeps camera inside boundaries
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z
        );
    }
}
