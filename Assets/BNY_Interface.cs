using System.Collections.Generic;
using UnityEngine;

public class BNY_Interface : MonoBehaviour{
    static public List<List<GameObject>> pathQueue = new List<List<GameObject>>();
    static public float queueNumber = 0f;
    static public float health = 100f;
    static public float hunger = 100f;
    static public float speed = 10f;
    static public float accel = 5f;
    static public float rotationSpeed = 180f;
    static public float maxDistPerc = 0.8f;
}
