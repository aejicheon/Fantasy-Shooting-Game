using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_move2 : MonoBehaviour
{
    float time = 0.0f;
    float speed = 25.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1.25f)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (time > 4.0f) Destroy(this.gameObject);
    }
}
