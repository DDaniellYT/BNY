using System.Collections.Generic;
using UnityEngine;

public class S_BNY : MonoBehaviour{
    [SerializeField] public Material pointMaterial;
    static public List<List<GameObject>> pathQueue = new List<List<GameObject>>();
    static public float health = 100f;
    static public float hunger = 100f;
    static public float speed = 10f;
}
