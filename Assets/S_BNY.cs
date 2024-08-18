using System.Collections.Generic;
using UnityEngine;

public class S_BNY : MonoBehaviour{
    static public List<List<Vector3>> pathQueue = new List<List<Vector3>>();
    static public float health = 100f;
    static public float hunger = 100f;
    static public float speed = 10f;
    static public float maxDistPerc = 0.8f;
}
