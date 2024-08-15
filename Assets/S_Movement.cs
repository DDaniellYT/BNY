using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Movement : MonoBehaviour{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update(){
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * S_BNY.speed;
    }
}
