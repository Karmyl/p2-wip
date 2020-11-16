using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenMoving : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    // Set speed to chicken
    void Update()
    {
      this.transform.position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
    }

    // Check if chicken gets home and destroy chicken when it gets home.
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Paastiin kotiin!");
       if (col.gameObject.tag == "Koti")
        {
           Score.AddScore(1);
           Destroy(gameObject);
        }

    }

}

