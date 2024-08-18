using System;
using System.Collections.Generic;
using UnityEngine;

public class S_Movement : MonoBehaviour{
    Rigidbody rb;

    GameObject vPathPoint; // v stands for visual
    Material vPathPointMaterial;

    List<Vector3> pathToCreate = new List<Vector3>();
    List<Vector3> pathToFollow = new List<Vector3>();
    Vector3 nowPoint = new Vector3();
    Vector3 wantedPoint = new Vector3();
    Vector3 velocity = new Vector3(0,0,0);

    public Vector3 diff;
    public Vector3 dist;
    public Vector3 direction;

    float p = 0;

    bool toggle = false;
    bool pathFollowing = false;

    void Start(){
        rb = GetComponent<Rigidbody>();
        vPathPointMaterial = new Material(Shader.Find("Standard")){
            color = Color.blue
        };
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    void FixedUpdate(){
        rb.velocity += new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * S_BNY.speed;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            toggle = !toggle;
            if(pathToCreate.Count>0)
                S_BNY.pathQueue.Add(pathToCreate);
            pathToCreate = new List<Vector3>();
        }
        createPathPoint(toggle, Input.GetMouseButtonUp(0), S_MainCamera.hoverLocation);

        if(S_BNY.pathQueue.Count != 0 && !pathFollowing){
            pathToFollow = S_BNY.pathQueue[0];
            S_BNY.pathQueue.Remove(S_BNY.pathQueue[0]);
        }
        followPath();
    }
    void followPath(){
        if(pathToFollow.Count == 0){
            pathFollowing = false;
            return;
        }
        if(!pathFollowing){
            nowPoint = transform.position;
            wantedPoint = pathToFollow[0];
            dist = wantedPoint - nowPoint;
            pathFollowing = true;
        }

        diff = new Vector3(wantedPoint.x-transform.position.x , wantedPoint.y-transform.position.y , wantedPoint.z-transform.position.z);
        direction = new Vector3(Math.Sign(diff.x),0,Math.Sign(diff.z));
        
        if(Math.Abs(diff.x) < 2 && Math.Abs(diff.z) < 2){
            if(pathToFollow.Count == 0)return;
            pathToFollow.Remove(pathToFollow[0]);
            wantedPoint = pathToFollow[0];
            nowPoint = transform.position;
            dist = wantedPoint - nowPoint;
        }
        else {
            p = (float)Math.Sqrt(diff.x*diff.x+diff.z*diff.z)/(float)Math.Sqrt((wantedPoint.x-nowPoint.x)*(wantedPoint.x-nowPoint.x)+(wantedPoint.z-nowPoint.z)*(wantedPoint.z-nowPoint.z));
        }
        // doesnt work right , jitters and doesnt steadily grow in speed or slow down
        velocity = (1 - p + 0.1f) * direction * S_BNY.speed + (p + 0.1f) * direction * S_BNY.speed;
        

        if(Math.Abs(velocity.x) > S_BNY.speed)velocity.x = S_BNY.speed * Math.Sign(velocity.x);
        if(Math.Abs(velocity.z) > S_BNY.speed)velocity.z = S_BNY.speed * Math.Sign(velocity.z);

        rb.velocity = velocity;

        Debug.Log("current velocity : " + rb.velocity);
        Debug.Log("wanted velocity : " + velocity);
        Debug.Log("current difference : " + diff);
        Debug.Log("start difference : " + dist);
        Debug.Log("p : " + p);
    }
    void createPathPoint(bool toggle, bool button, Vector3 location){
        if(toggle && button){
            if(location != null){
                vPathPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                vPathPoint.transform.SetPositionAndRotation(location, new Quaternion(0,0,0,0));
                vPathPoint.GetComponent<MeshRenderer>().material = vPathPointMaterial;
                pathToCreate.Add(vPathPoint.transform.position);
            }
        }
    }
}
