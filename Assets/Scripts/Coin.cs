using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotationY = Quaternion.AngleAxis(1,Vector3.up);
        transform.rotation *= rotationY;
        //transform.Rotate(0, 40*Time.deltaTime, 0);
    }
}
