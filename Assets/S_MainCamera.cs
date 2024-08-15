using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_MainCamera : MonoBehaviour{
    // Update is called once per frame

    float zoomSpeed = 0.25f;
    float sensitivity = 1f;

    float minRotation = 20f;
    float maxRotation = 50f;

    void Update(){
        if(Input.GetMouseButton(0))
            transform.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0) * sensitivity;
        if(transform.eulerAngles.x < minRotation)transform.eulerAngles = new Vector3(minRotation,transform.eulerAngles.y,0);
        if(transform.eulerAngles.x > maxRotation)transform.eulerAngles = new Vector3(maxRotation,transform.eulerAngles.y,0);

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)transform.localScale += new Vector3(1,1,1) * zoomSpeed;
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)transform.localScale -= new Vector3(1,1,1) * zoomSpeed;
        if(transform.localScale.x < zoomSpeed)transform.localScale = new Vector3(1,1,1) * zoomSpeed;
        if(transform.localScale.x > zoomSpeed * 10)transform.localScale = new Vector3(1,1,1) * zoomSpeed * 10;
    }
}
