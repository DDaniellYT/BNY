using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM_MouseLocation : MonoBehaviour{
    static public Camera cam;
    public static Vector3 hoverLocation;
    public static GameObject hoverObject;
    Vector3 mousePos;

    void Start(){
        cam = Camera.main;
    }

    void Update(){
        getHover();
    }
    public void getHover(){
        mousePos = Input.mousePosition;
        mousePos.z = 1000f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Ray ray = new Ray(cam.transform.position,mousePos-cam.transform.position);
        Debug.DrawRay(cam.transform.position, mousePos-cam.transform.position , Color.blue);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000)){
            hoverLocation = new Vector3(hit.point.x,0,hit.point.z);
            hoverObject = hit.collider.gameObject;
        }
        else {
            hoverLocation = Vector3.zero;
            hoverObject = null;
        }

    }
}
