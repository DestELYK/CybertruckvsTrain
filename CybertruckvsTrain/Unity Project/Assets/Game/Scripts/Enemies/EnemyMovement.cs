/* Name: Kyle Dunn
 * Date: Dec 2, 2019
 * Purpose: Handles movement for enemies. Requires the VehicleMovement component for updating the steering and thrust for the vehicle.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(VehicleMovement), typeof(Health))]
public class EnemyMovement : MonoBehaviour
{
    const int UPDATE_PATH_INTERVAL = 2;

    private NavMeshPath path;

    private int pathIndex;

    VehicleMovement movement;

    Vector3 destination;
    Vector3 pathDestination;

    float updatePathTime;

    /// <summary>
    /// Determines if the path has been completed
    /// </summary>
    public bool PathReached { get { return path.corners.Length == 0; } }

    /// <summary>
    /// Updates the destination for the enemy to move towards.
    /// </summary>
    /// <param name="destination"></param>
    public void SetDestination(Vector3 destination)
    {
        pathIndex = 1;
        pathDestination = destination;
        updatePathTime = 0;

        // Casts a ray downwards to find a point that hits the terrain
        RaycastHit raycastHit = new RaycastHit();
        Physics.Raycast(destination, Vector3.down, out raycastHit, 20, 1 << 9);

        // Calculates a path to the destination
        NavMesh.CalculatePath(transform.position, raycastHit.point, 1 << 0, path);
    }

    /// <summary>
    /// Finds a random point between x 50 - 200 and z 50 - 200
    /// </summary>
    /// <returns></returns>
    public Vector3 FindRandomDestination()
    {
        movement.Thrust = 1.0f;

        // Casts a ray downwards to find a point that hits the terrain
        RaycastHit raycastHit = new RaycastHit();
        Physics.Raycast(new Vector3(Random.Range(50, 200), 100, Random.Range(50, 200)), Vector3.down, out raycastHit);

        return raycastHit.point;
    }

    private void Awake()
    {
        movement = GetComponent<VehicleMovement>();
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (path.corners.Length == 0)
            return;

        destination = path.corners[pathIndex]; // Sets the destination to the current path

        Vector3 direction = destination - transform.position;

        // Determines if the enemy has reached the destination
        if (direction.sqrMagnitude <= 10f)
        {
            pathIndex++;

            // If it is the final one in the path resets everything
            if (pathIndex == path.corners.Length)
            {
                pathIndex = 1;
                updatePathTime = 0;
                //movement.Thrust = 0.0f;
                path.ClearCorners();
            }
        }

        // Calculates a new path every so often
        updatePathTime += Time.deltaTime;
        if (updatePathTime >= UPDATE_PATH_INTERVAL)
        {
            pathIndex = 1;
            updatePathTime = 0;
            NavMesh.CalculatePath(transform.position, pathDestination, 1 << 0, path);
        }

        // Finds the angle between the vehicles forward direction and the destination direction
        // Clamps that value between -45 and 45 and sets the movement steer to the angle
        movement.Steer = Mathf.Clamp(Vector3.SignedAngle(transform.forward, direction.normalized, Vector3.up), -45, 45);

        DrawDebug();
    }

    /// <summary>
    /// Draws the debug
    /// </summary>
    void DrawDebug()
    {

        //Draws the path
        for (int i = pathIndex; i < path.corners.Length; i++)
        {
            Vector3 c = path.corners[i];

            if (i == pathIndex)
            {
                Debug.DrawLine(transform.position, c, Color.green);
            }
            else
            {
                Debug.DrawLine(path.corners[i - 1], c, Color.green);
            }
        }
    }
}
