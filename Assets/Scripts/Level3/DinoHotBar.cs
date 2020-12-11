using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoHotBar : MonoBehaviour
{
    public GameObject hotbar;
    public List<GameObject> symbols = new List<GameObject>();
    public List<Vector3> savePos;
    public Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("APUA!");
        for (int i = 0; i < hotbar.transform.childCount; i++)
        {
            GameObject go = hotbar.transform.GetChild(i).GetChild(0).gameObject;
            symbols.Add(go);
        }

        symbols = Shuffle<GameObject>(symbols);
        for (int t = 0; t < symbols.Count; t++)
        {
            Debug.Log(symbols[t]);
        }

        BlockInstantiate();
        StartCoroutine(TimerDelay());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Shuffle the list when this method called.
    public static List<T> Shuffle<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }
        return _list;
    }

    IEnumerator TimerDelay()  //  <-  its a standalone method
    {
       
        while (true)
        {
            
            if (symbols.Count > 0)
            {
                if (this.transform.childCount > 0)
                {
                    Destroy(this.transform.GetChild(0).gameObject);
                    symbols.RemoveAt(0);

                    if (symbols.Count > 0)
                    {
                        BlockInstantiate();
                        this.gameObject.transform.localScale = new Vector3(23,23,23);
                    }
                }
            }
            //yield return new WaitForSeconds(3f);
            this.gameObject.transform.localScale -= new Vector3(3f, 3f, 0f);
            yield return new WaitForSeconds(1.5f);
            this.gameObject.transform.localScale -= new Vector3(6f, 6f, 0f);
            yield return new WaitForSeconds(1.5f);
            this.gameObject.transform.localScale -= new Vector3(9f, 9f, 0f);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public int GetBlockType()
    {
        if (this.transform.childCount > 0)
        {
            return this.transform.GetChild(0).GetComponent<BlockDraggingLevel3>().GetBlockType();
        }
        return 0;
    }

    void BlockInstantiate()
    {
        GameObject go1 = Instantiate(symbols[0], this.transform);
        go1.GetComponent<BlockDraggingLevel3>().enabled = false;
        go1.GetComponent<MeshCollider>().enabled = false;
        go1.GetComponent<SoundTrigger>().enabled = false;
    }

}
