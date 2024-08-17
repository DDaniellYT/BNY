using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_MainCamera : MonoBehaviour{
    [SerializeField] public Material material;
    static public Camera cam;

    float zoomAmount = 0.25f;
    float sensitivity = 1f;

    float minRotation = 20f;
    float maxRotation = 50f;

    bool mouseButtonDown = false;
    bool toggle = false;
    Vector3 mousePos;

    List<Transform> path = new List<Transform>();
    GameObject point;

    void Start(){
        cam = GetComponentInChildren<Camera>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab))
            toggle = !toggle;

        camMove(Input.GetMouseButton(0));
        createPathPoint(toggle, Input.GetMouseButtonUp(0));
        
    }
    void createPathPoint(bool toggle, bool button){
        Debug.Log(getHoverLocation());
        if(toggle && button){
            Vector3 locTemp = getHoverLocation();
            if(locTemp != null){
                point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                point.transform.SetPositionAndRotation(locTemp,new Quaternion(0,0,0,0));
                point.GetComponent<MeshRenderer>().material = material;
                path.Add(point.transform);
            }
        }
    }
    Vector3 getHoverLocation(){
        mousePos = Input.mousePosition;
        mousePos.z = 1000f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Ray ray = new Ray(cam.transform.position,mousePos-cam.transform.position);
        Debug.DrawRay(cam.transform.position, mousePos-cam.transform.position , Color.blue);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000))
            return new Vector3(hit.point.x,0,hit.point.z);
        else return new Vector3();
    }
    void camMove(bool buttonDown){
        if(buttonDown){ // check for visual mode
            transform.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0) * sensitivity;

            if(Input.GetAxis("Mouse ScrollWheel") < 0f)transform.localScale += new Vector3(1,1,1) * zoomAmount;
            if(Input.GetAxis("Mouse ScrollWheel") > 0f)transform.localScale -= new Vector3(1,1,1) * zoomAmount;
        }

        // cam lock between values
        if(transform.localScale.x < zoomAmount)transform.localScale = new Vector3(1,1,1) * zoomAmount;
        if(transform.localScale.x > zoomAmount * 10)transform.localScale = new Vector3(1,1,1) * zoomAmount * 10;
        if(transform.eulerAngles.x < minRotation)transform.eulerAngles = new Vector3(minRotation,transform.eulerAngles.y,0);
        if(transform.eulerAngles.x > maxRotation)transform.eulerAngles = new Vector3(maxRotation,transform.eulerAngles.y,0);
    }
}
