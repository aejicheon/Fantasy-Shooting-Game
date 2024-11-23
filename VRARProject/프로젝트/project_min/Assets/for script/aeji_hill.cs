using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aeji_hill : MonoBehaviour
{

    public float speed = 7.0f;
    float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2.5f) Destroy(this.gameObject);

    }
}
