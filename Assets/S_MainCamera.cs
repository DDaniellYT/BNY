using UnityEditor;
using UnityEngine;

public class S_MainCamera : MonoBehaviour{
    static public Camera cam;

    float zoomAmount = 0.25f;
    float sensitivity = 1f;

    float minRotation = 20f;
    float maxRotation = 50f;

    bool mouseButtonDown = false;
    Vector3 mousePos;

    void Start(){
        cam = GetComponentInChildren<Camera>();
    }

    void Update(){
        if(Input.GetMouseButton(0))
            mouseButtonDown = true;
        else mouseButtonDown = false;


    }
    void FixedUpdate(){
        camMove(mouseButtonDown);
        Debug.Log("location of hit : " + getHitLocation());
    }
    
    Vector3 getHitLocation(){
        mousePos = Input.mousePosition;
        mousePos.z = 100f; // size of the ray
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Ray ray = cam.ScreenPointToRay(mousePos);
        Physics.Raycast(ray, out RaycastHit hit, 100);
        return hit.point;
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
