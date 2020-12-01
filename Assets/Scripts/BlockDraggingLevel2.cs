using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDraggingLevel2 : MonoBehaviour
{
    public int blockID;
    private bool hasBeenUsed = false;

    private bool isDragged = false;
    private bool startedDragging = false;
    private bool endedDragging = false;
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(isDragged)
            {
                // Started dragging
                Debug.Log("1");

                // Set scale
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                startedDragging = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(isDragged)
            {
                // Ended dragging
                Debug.Log("2");

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
                isDragged = true;
                Vector3 intersectionPoint = hit.point;
                Vector3 offsetFromGround = new Vector3(0.0f, 5.0f, 0.0f);
                Vector3 hoveredPosition = intersectionPoint + offsetFromGround;
                this.transform.position = hoveredPosition;
                this.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                isDragged = true;
            }
        }
    }

    //Check for collision in the correct lane
    void OnCollisionEnter(Collision collision)
    {
        if (blockID == collision.gameObject.GetComponent<BlockDetection>().allowedBlockID && !hasBeenUsed)
        {
            hasBeenUsed = true;
            Score.AddScore(1);
        }
    }


void FixedUpdate()
    {

    }
}
