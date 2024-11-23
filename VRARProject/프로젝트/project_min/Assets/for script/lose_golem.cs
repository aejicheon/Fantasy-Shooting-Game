using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lose_golem : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("die",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
