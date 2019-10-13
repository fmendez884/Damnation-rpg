using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParentTransform : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorMove()
    {
        //transform.parent.rotation = anim.rootRotation;
        //transform.parent.position = anim.rootPosition;
    }

}
