using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoHotBar : MonoBehaviour
{
    public List<string> symbols = new List<string>() {"palikka1", "palikka2", "palikka3", "palikka4", "palikka5", "palikka6", "palikka7", "palikka8", "palikka9", "palikka10" };

    // Start is called before the first frame update
    void Start()
    {
        symbols = Shuffle<string>(symbols);
        Debug.Log(symbols[0]);
        Debug.Log(symbols[1]);
        Debug.Log(symbols[2]);
        Debug.Log(symbols[3]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  List<T> Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }

}
