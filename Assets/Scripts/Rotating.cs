using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public enum Direction { left, right };
    
    [SerializeField] float rotationSpeed;

    [SerializeField] Direction direction;


    private void Start() {
        if(direction == Direction.left) {
            rotationSpeed = rotationSpeed * -1;
        }    
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public void SwitchDirections() {
        direction = direction == Direction.left ? Direction.right : Direction.left;
        rotationSpeed *= -1;
    }

}
