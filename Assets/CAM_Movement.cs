using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM_Movement : MonoBehaviour{
    static public Camera cam;

    float zoomAmount = 0.25f;
    float sensitivity = 1f;

    float minRotation = 20f;
    float maxRotation = 50f;

    void Start(){
        cam = Camera.main;
    }
    void Update(){
        if(Input.GetMouseButton(0)){
            transform.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0) * sensitivity;

            if(Input.GetAxis("Mouse ScrollWheel") < 0f)transform.localScale += new Vector3(1,1,1) * zoomAmount;
            if(Input.GetAxis("Mouse ScrollWheel") > 0f)transform.localScale -= new Vector3(1,1,1) * zoomAmount;
        }

        if(transform.localScale.x < zoomAmount)transform.localScale = new Vector3(1,1,1) * zoomAmount;
        if(transform.localScale.x > zoomAmount * 10)transform.localScale = new Vector3(1,1,1) * zoomAmount * 10;
        if(transform.eulerAngles.x < minRotation)transform.eulerAngles = new Vector3(minRotation,transform.eulerAngles.y,0);
        if(transform.eulerAngles.x > maxRotation)transform.eulerAngles = new Vector3(maxRotation,transform.eulerAngles.y,0);
    }
}
