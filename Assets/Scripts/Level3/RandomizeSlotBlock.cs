using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSlotBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject slots;
    public List<GameObject> allowedBlocks;
    private int allowedBlockType = -1;

    public int GetAllowedBlockType()
    {
        return (allowedBlockType + 1);
    }

    void Start()
    {
        // Select random block to show in slot
        int index = Random.Range(0, allowedBlocks.Count);
        allowedBlockType = index;
        Instantiate(allowedBlocks[index], this.transform);
        //for(int i = 0; i < slots.transform.childCount; i++)
        //{
        //    GameObject go = slots.transform.GetChild(i).GetChild(0).gameObject;
        //    Debug.Log("go.name: " + go.name);
        //}
    }
}
