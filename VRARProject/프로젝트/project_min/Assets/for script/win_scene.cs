using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win_scene : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetTrigger("die"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
