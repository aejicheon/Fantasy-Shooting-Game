using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkball_spawn : MonoBehaviour
{
    public GameObject darkball;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(darkball, this.transform.position, this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
