using System.Collections.Generic;
using UnityEngine;

public class S_Movement : MonoBehaviour{
    Rigidbody rb;
    Material pointMaterial;
    GameObject point;
    List<GameObject> path = new List<GameObject>();

    float p = 1;
    float x_vel = 0;
    float y_vel = 0;

    bool toggle = false;

    void Start(){
        rb = GetComponent<Rigidbody>();
        pointMaterial = new Material(Shader.Find("Standard")){
            color = Color.blue
        };
    }

    void FixedUpdate(){
        rb.velocity += new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * S_BNY.speed;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            toggle = !toggle;
            if(path.Count>0)
                S_BNY.pathQueue.Add(path);
            path = new List<GameObject>();
        }
        createPathPoint(toggle, Input.GetMouseButtonUp(0), S_MainCamera.hoverLocation);
    }

    void createPathPoint(bool toggle, bool button, Vector3 location){
        if(toggle && button){
            if(location != null){
                point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                point.transform.SetPositionAndRotation(location,new Quaternion(0,0,0,0));
                point.GetComponent<MeshRenderer>().material = pointMaterial;
                path.Add(point);
            }
        }
    }
}
