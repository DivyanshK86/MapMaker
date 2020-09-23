using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenerator : MonoBehaviour {

    int i = 0;

    void Start()
    {
        StartCoroutine(lvlGenLoop());
    }

    IEnumerator lvlGenLoop()
    {
        yield return new WaitForSeconds(0.01f);

        if (i < transform.childCount)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            i++;
            StartCoroutine(lvlGenLoop());
        }
        else
            StartCoroutine(UpdateSprLoop());

    }

    IEnumerator UpdateSprLoop()
    {
        yield return new WaitForSeconds(0.01f);

        if(i < transform.childCount)
        {
            transform.GetChild(i).GetComponent<AutoSetBlock>().CheckUpdateAndUpdateOthers();
            i++;
            StartCoroutine(lvlGenLoop());
        }

    }
}
