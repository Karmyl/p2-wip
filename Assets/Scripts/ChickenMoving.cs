using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenMoving : MonoBehaviour
{

    public GameObject Cube; //The Cube

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    //Set speed to chicken
    void Update()
    {
      Cube.transform.position += new Vector3(3f, 0f, 0f);
    }

    //Check if chicken gets home and destroy chicken when it gets home.
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

