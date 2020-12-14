using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBlocksLevel1 : MonoBehaviour
{
    public GameObject hotbar;
    public List<GameObject> symbols = new List<GameObject>();
    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < hotbar.transform.childCount; i++)
        {
            int index = Random.Range(0, prefabs.Length);
            GameObject go = Instantiate(prefabs[index], hotbar.transform.GetChild(i));
            go.transform.position = hotbar.transform.GetChild(i).position;
            go.GetComponent<DragAndDrop>().canvas = FindObjectOfType<Canvas>();
            symbols.Add(go);
        }
        Debug.Log(symbols);
        symbols = Shuffle<GameObject>(symbols);
        Debug.Log(symbols);
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
}
