using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDraggingLevel2 : MonoBehaviour
{
    public int blockType = 0;
    private bool isScaled = false;
    private bool isDragged = false;
    private bool startedDragging = false;
    private bool isOnCorrectLane = false;
    private Vector3 initialScale;
    AudioManager audiomanager;

    public bool GetIsScaled()
    {
        return isScaled;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        audiomanager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isDragged)
            {
                // Continue to drag object
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragged)
            {
                // Ended dragging
                // Restore initial scale
                this.transform.localScale = initialScale;
                this.GetComponent<Rigidbody>().useGravity = true;
                this.GetComponent<Rigidbody>().isKinematic = false;
                isDragged = false;
            }
        }
    }

    void OnMouseDrag()
    {
        if (!startedDragging)
        {
            // Intersect only with ground
            int layerMask = 1 << 8;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check intersection with only layer 8 (Ground)
            if (Physics.Raycast(ray, out hit, 200.0f, layerMask))
            {
                Vector3 intersectionPoint = hit.point;
                Vector3 offsetFromGround = new Vector3(0.0f, 5.0f, 0.0f);
                Vector3 hoveredPosition = intersectionPoint + offsetFromGround;
                this.transform.position = hoveredPosition;
                this.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                this.transform.localScale = initialScale;

                // Reset velocities
                this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
                this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);

                isDragged = true;
                isScaled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string laneTag = collision.gameObject.tag;
        switch (blockType)
        {
            // Block = yellow
            case 1:
                if (laneTag.CompareTo("LaneYellow") == 0)
                {
                    isOnCorrectLane = true;
                }
                break;

            // Block = red
            case 2:
                if (laneTag.CompareTo("LaneRed") == 0)
                {
                    isOnCorrectLane = true;
                }
                break;

            // Block = green
            case 3:
                if (laneTag.CompareTo("LaneGreen") == 0)
                {
                    isOnCorrectLane = true;
                }
                break;

            // Block = blue
            case 4:
                if (laneTag.CompareTo("LaneBlue") == 0)
                {
                    isOnCorrectLane = true;
                }
                break;

            default:
                break;
                
        }

        if (isOnCorrectLane)
        {
            if (!isScaled)
            {
                this.transform.localScale *= 2.0f;
                isScaled = true;
                audiomanager.PlaySound("palikka_kasvaa");
            }
        } else
        {
             audiomanager.PlaySound("palikka1");
        }    
    }
}

