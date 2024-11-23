using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_coll : MonoBehaviour
{
    // Start is called before the first frame update
    int count = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.GetComponent<Rigidbody>().AddForce(0, 0, 0.0001f);
    }
    void OnCollisionEnter(Collision coll)
    {
        count++;
        if(coll.gameObject.tag == "Enemy");
    }
}
