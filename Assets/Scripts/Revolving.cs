using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolving : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float radius = 1f;

    private float angle;
    private Vector3 center;

    private void Start() {
        center = transform.position;    
    }

    private void Update() {
        angle += speed * Time.deltaTime;
        var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * radius;
        transform.position = center + offset;
    }
}
