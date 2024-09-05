using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BNY_Movement : MonoBehaviour{
    
    GameObject ghost = new GameObject();
    public float ghostSpeed = 15f;
    public float ghostAccel = 5f;

    Rigidbody rb;
    
    float objDistance = 0f;
    float pointDistance = 0f;
    
    float timeFollow = 0f;
    bool followToggle = false;


    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        
        Material ghostMaterial = new Material(Shader.Find("Standard")){
            color = Color.gray
        };
        
        ghost = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ghost.GetComponent<MeshRenderer>().material = ghostMaterial;
        ghost.transform.SetPositionAndRotation(transform.position, new Quaternion(0,0,0,0));
        ghost.GetComponent<Collider>().enabled = false;
        ghost.name = "Ghost";
    }
    void Update(){
        if(Input.GetKeyDown("f") && BNY_Interface.pathQueue.Count > 0){
            followToggle = !followToggle;
            timeFollow = 0f;
        }

        switch(followToggle){ // if following
            case false:{
                transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * BNY_Interface.rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * BNY_Interface.speed * Time.deltaTime);
                break;
            }
            case true:{
                FollowPath(gameObject,
                            ghost,
                            BNY_Interface.pathQueue.Count > 0 ? BNY_Interface.pathQueue[0] : null,
                            ghostSpeed, // why is it taken as if its just been made ?
                            ghostAccel);
                            // BNY_Interface.speed,
                            // BNY_Interface.accel);
                break;
            }
        }
    }
    void FollowPath(GameObject follower, GameObject strictFollower, List<GameObject> path, float strictSpeed, float strictAccel){
        Vector3 actualDirection = (strictFollower.transform.position - follower.transform.position).normalized;
        objDistance = Mathf.Sqrt(Mathf.Pow(strictFollower.transform.position.x - follower.transform.position.x,2f) + Mathf.Pow(strictFollower.transform.position.z - follower.transform.position.z,2f));
        follower.transform.Translate(actualDirection * Mathf.Min(BNY_Interface.speed, timeFollow * 5) * Time.deltaTime);

        if(path != null && path.Count > 0 && followToggle){
            Vector3 strictDirection = (path[0].transform.position - strictFollower.transform.position).normalized;
            pointDistance = Mathf.Sqrt(Mathf.Pow(path[0].transform.position.x - strictFollower.transform.position.x,2f) + Mathf.Pow(path[0].transform.position.z - strictFollower.transform.position.z,2f));
            strictFollower.transform.Translate(strictDirection * Mathf.Min(strictSpeed - objDistance, timeFollow * strictAccel) * Time.deltaTime);

            if(pointDistance < 0.2f){
                Destroy(GameObject.Find(path[0].name));
                path.Remove(path[0]);
            }
        }
        if(path == null && objDistance < 0.1f){
            followToggle = false;
        }

        timeFollow += Time.deltaTime;
        Debug.Log(strictSpeed);
        Debug.Log(strictAccel);
        Debug.Log(path.Count);
    }
}
