using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem_spawn : MonoBehaviour
{
    public GameObject comeon;
    public GameObject[] golem = new GameObject[3];
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            Instantiate(comeon, this.transform.position, this.transform.rotation);
            Instantiate(golem[Random.Range(0, 3)],this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(Random.Range(50.0f, 60.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
