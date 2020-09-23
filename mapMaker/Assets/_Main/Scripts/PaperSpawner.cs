using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSpawner : MonoBehaviour {

    public GameObject objectToInstantiate;
    float clipTime;

    void Awake()
    {
        clipTime = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Invoke("CreateObject",clipTime);
    }

    void CreateObject()
    {
        Instantiate(objectToInstantiate,transform.GetChild(0).position,Quaternion.identity);
        Destroy(gameObject);
    }
}
