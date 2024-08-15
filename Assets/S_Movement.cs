using UnityEngine;

public class S_Movement : MonoBehaviour{
    Rigidbody rb;
    float p = 1;
    float x_vel = 0;
    float y_vel = 0;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate(){
        rb.velocity += p * new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * S_BNY.speed;
    }
}
