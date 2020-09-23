using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowZoom : MonoBehaviour {

    public CharacterManager cm;
    public float minSize;
    public float maxSize;
    public float lerpSpeed;

    //Cinemachine.CinemachineVirtualCamera cvc;
    float shouldSize;

    void Start()
    {
        //cvc = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    void Update()
    {
        //shouldSize = (Mathf.Abs(cm.characterController.rb.velocity.magnitude) > 0.1f) ? maxSize : minSize;
        //cvc.m_Lens.OrthographicSize = Mathf.Lerp(cvc.m_Lens.OrthographicSize, shouldSize, lerpSpeed * Time.deltaTime);
    }
}
