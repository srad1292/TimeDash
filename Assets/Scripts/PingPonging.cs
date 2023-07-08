using UnityEngine;
using System.Collections;

public class PingPonging : MonoBehaviour {
    public Transform pointA;
    public Transform pointB;
    public float speed;
    public bool movingTowardsB = true;
    Rotating rotating;

    private void Start() {
        rotating = GetComponent<Rotating>();
    }

    void Update() {
        transform.position = Vector3.Lerp(pointA.position, pointB.position, Mathf.PingPong(Time.time * speed, 1.0f));
        if (Mathf.Abs(Vector3.Distance(pointB.position, transform.position)) < 0.1 && movingTowardsB) {
            movingTowardsB = false;
            if (rotating != null) {
                rotating.SwitchDirections();
            }
        } else if (Mathf.Abs(Vector3.Distance(pointA.position, transform.position)) < 0.1 && !movingTowardsB) {
            movingTowardsB = true;
            if(rotating != null) {
                rotating.SwitchDirections();
            }
        }
    }

}
