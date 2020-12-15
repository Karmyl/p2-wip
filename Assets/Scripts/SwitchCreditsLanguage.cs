using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchCreditsLanguage : MonoBehaviour
{
    bool isTextFinnish = true;
    public GameObject kylttiFI;
    public GameObject kylttiEN;

    // Start is called before the first frame update
    void Start()
    {
        kylttiEN.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchLanguage()
    {
        isTextFinnish = !isTextFinnish;
        if(isTextFinnish)
        {
            TMP_Text[] compsFI = kylttiFI.GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text t in compsFI)
            {
                t.enabled = false;
            }
            kylttiFI.SetActive(false);

            TMP_Text[] compsEN = kylttiEN.GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text t in compsEN)
            {
                t.enabled = true;
            }
            kylttiEN.SetActive(true);
        }
        else
        {
            TMP_Text[] compsFI = kylttiFI.GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text t in compsFI)
            {
                t.enabled = true;
            }
            kylttiFI.SetActive(true);

            TMP_Text[] compsEN = kylttiEN.GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text t in compsEN)
            {
                t.enabled = false;
            }
            kylttiEN.SetActive(false);
        }
    }
}
