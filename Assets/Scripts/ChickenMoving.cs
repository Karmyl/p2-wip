using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "chome")
        {
            Debug.Log("Paastiin kotiin!");
            Destroy(col.gameObject);
        }
    }

}

