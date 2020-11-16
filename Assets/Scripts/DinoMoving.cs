using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoMoving : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //Set speed to dino
    void Update()
    {
      this.transform.position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
    }

    //Check if collision has happened
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Kana")
        {
            Debug.Log("Tormays tapahtunut!");
            Destroy(collision.gameObject);
        }
    }
}