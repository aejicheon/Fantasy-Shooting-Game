using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aeji_test : MonoBehaviour
{
    public ImgsFillDynamic ImgsFD;
    public GameObject ll;
    void Start()
    {
        this.ImgsFD.SetValue(0, true, 0.5f);
    }
    private void Update()
    {

        this.ImgsFD.SetValue(ll.GetComponent<player_control>().aeji_hillt/ 20.0f, true, 0.5f);

    }
}
