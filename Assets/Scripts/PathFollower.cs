using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    // Dinosaur reference comes here
    public GameObject dinosaur = null;
    public GameObject chicken = null;
    public ParticleSystem particleSystem = null;

    // Start is called before the first frame update
    public Transform[] waypoints;

    // Total duration in seconds for traversing all waypoints
    public float totalPathDurationInSeconds;

    // Rotation speed when changing to new heading
    public float rotationSpeed = 10.0f;

    public float slowedDurationInSeconds = 1.0f;
    public float slowingCoefficient = 0.5f;
    private bool isSlowed = false;
    private float slowedAccumulator = 0.0f;

    // Total length of path
    private float totalPathLength;

    // Lengths and durations for each path segment
    private float[] segmentDurationInSeconds;
    private float[] segmentLength;
    private int currentWaypointIndex;
    private int maxWaypointIndex;
    private float accumulator;

    //Audiomanager for playing sound effects
    AudioManager audiomanager;

    void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        accumulator = 0.0f;
        currentWaypointIndex = -1;
        maxWaypointIndex = waypoints.Length;
        if(maxWaypointIndex > 1)
        {
            // Set initial position to first waypoint
            currentWaypointIndex = 0;
            this.transform.position = waypoints[0].position;

            // Calculate each segment length and total path length
            totalPathLength = 0.0f;
            segmentLength = new float[maxWaypointIndex - 1];
            for (int i = 0; i < segmentLength.Length; i++)
            {
                Vector3 segmentStart = waypoints[i].position;
                Vector3 segmentEnd = waypoints[i + 1].position;
                segmentLength[i] = Vector3.Distance(segmentEnd, segmentStart);
                totalPathLength += segmentLength[i];
            }

            // Calculate duration for each segment using total path duration
            segmentDurationInSeconds = new float[segmentLength.Length];
            for (int i = 0; i < segmentDurationInSeconds.Length; i++)
            {
                segmentDurationInSeconds[i] = (segmentLength[i] / totalPathLength) * totalPathDurationInSeconds;
            }

            // Orient GameObject toward segment end
            // Orient cube toward new waypoint
            Vector3 direction = waypoints[currentWaypointIndex + 1].position - this.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, direction, rotationSpeed, 0.0f);
            this.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaypointIndex < (maxWaypointIndex - 1))
        {
            if (isSlowed)
            {
                // Slowed down moving
                slowedAccumulator += Time.deltaTime;
                if (slowedAccumulator > slowedDurationInSeconds)
                {
                    isSlowed = false;
                }

                accumulator += Time.deltaTime * slowingCoefficient;
            }
            else
            {
                // Normal moving
                accumulator += Time.deltaTime;
            }

            float durationForSegment = segmentDurationInSeconds[currentWaypointIndex];

            if (accumulator < durationForSegment)
            {
                // Orient GameObject if not at the end of path
                if (currentWaypointIndex < (maxWaypointIndex - 1))
                {
                    // Orient cube toward new waypoint
                    Vector3 direction = waypoints[currentWaypointIndex + 1].position - this.transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, direction, rotationSpeed, 0.0f);
                    this.transform.rotation = Quaternion.LookRotation(newDirection);
                }

                // Move toward next waypoint
                Vector3 a = waypoints[currentWaypointIndex].position;
                Vector3 b = waypoints[currentWaypointIndex + 1].position;
                float t = accumulator / durationForSegment;
                this.transform.position = Vector3.Lerp(a, b, t);
            }
            else
            {
                // Check for array boundaries
                if(currentWaypointIndex < maxWaypointIndex)
                {
                    // Switch to next segment
                    currentWaypointIndex++;
                    accumulator = 0.0f;
                    this.transform.position = waypoints[currentWaypointIndex].position;
                }
            }
        }
        else
        {
            // End of path
            //Debug.Log("End of path");
        }
    }

    // Handle collisions
    void OnCollisionEnter(Collision collision)
    {
        // Dinosaur collision checking
        if (this.CompareTag("Dino"))
        {
            if (collision.other.gameObject.CompareTag("Kana"))
            {
                Debug.Log("Dino osui kanaan");

                // Dinosaur got Chicken
                //Destroy(collision.other.gameObject);

                //Feather animation code, not optimal but works
                collision.other.gameObject.GetComponent<PathFollower>().enabled = false;
                collision.other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                collision.other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                //particleSystem.Play();
                this.enabled = false;

                // Reset velocity for RigidBody
                this.GetComponent<Rigidbody>().AddForce(Vector3.zero);
                this.GetComponent<Rigidbody>().isKinematic = true;

                // Sound effect here
                audiomanager.PlaySound("chickenGetsEaten");
                // Feathers animation here 

            } else if (collision.other.gameObject.CompareTag("Block"))
            {
                Debug.Log("Dino osui palikkaan");

                //Dinosaur slowing
                if (collision.other.GetComponent<BlockDraggingLevel2>().GetIsScaled())
                {
                    if (!isSlowed)
                    {
                        Debug.Log("Dino hidastuu");
                        isSlowed = true;
                        slowedAccumulator = 0.0f;
                    }
                }

                // Sound effect here
                audiomanager.PlaySound("dino_osuu_palikkaan");
                // Feathers animation here 
            }
        }

        // Chicken collision checking
        if (this.CompareTag("Kana"))
        {
            if (collision.other.gameObject.CompareTag("Koti"))
            {
                Debug.Log("Pääsin kotiin");
                // Chicken got home
                if (chicken != null)
                {
                    Destroy(chicken);
                }

                if(dinosaur != null)
                {
                    // Stop dinosaur
                    dinosaur.GetComponent<PathFollower>().enabled = false;
                }

                // Sound effect here
                audiomanager.PlaySound("chickenGetsHome");
            }
        }
    }
}
