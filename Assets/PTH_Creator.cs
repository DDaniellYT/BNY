using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTH_Creator : MonoBehaviour{
    List<GameObject> path = new List<GameObject>();
    Material pathMaterial;

    bool toggleCreatePath = false;

    void Start(){
        pathMaterial = new Material(Shader.Find("Standard")){
            color = Color.blue
        };
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            toggleCreatePath = !toggleCreatePath;
            if(path.Count > 0){
                BNY_Interface.pathQueue.Add(path);
                BNY_Interface.queueNumber++;
            }
            path = new List<GameObject>();
        }

        if(toggleCreatePath && Input.GetMouseButtonUp(0)){
            Vector3 location = CAM_MouseLocation.hoverLocation;
            if(location != Vector3.zero){
                GameObject pathPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pathPoint.name = "Queue" + (BNY_Interface.queueNumber+1) + "Point" + (path.Count+1);
                pathPoint.transform.SetPositionAndRotation(location, new Quaternion(0,0,0,0));
                pathPoint.GetComponent<MeshRenderer>().material = pathMaterial;
                pathPoint.GetComponent<Collider>().enabled = false;
                path.Add(pathPoint);
            }
        }

        if(BNY_Interface.pathQueue.Count > 0 && BNY_Interface.pathQueue[0].Count == 0)
            BNY_Interface.pathQueue.Remove(BNY_Interface.pathQueue[0]);
        
    }
}
