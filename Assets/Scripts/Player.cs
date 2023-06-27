using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] TimeManager timeManager;

    Rigidbody2D myRigidBody;
    LineRenderer myLineRenderer;
    
    bool newPress = false;
    bool isPressed = false;
    Vector3 pressPosition = Vector2.zero;
    Vector3 releasePosition = Vector2.zero;
    Camera mainCamera;

    private void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myLineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
    }

    private void Update() {
        if(isPressed) {
            myLineRenderer.positionCount = 2;
            myLineRenderer.SetPosition(0, releasePosition);
            myLineRenderer.SetPosition(1, pressPosition);
        }
    }


    void OnTouchInput(InputValue iv) {
        print("On touch. Is pressed: " + iv.isPressed);
        if(iv.isPressed == true) {
            isPressed = true;
        } else {
            isPressed = false;
            newPress = true;
            RemoveLine();
            MovePlayer();
        }
    }

    void OnTouchPosition(InputValue iv) {
        if(isPressed) {
            Vector2 touchPos = iv.Get<Vector2>();
            print("On Touch Pos: " + touchPos);
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, mainCamera.nearClipPlane));
            worldPos.z = 0f;

            print("World Pos: " + worldPos);
            if (newPress) {
                pressPosition = worldPos;
                releasePosition = worldPos;
                newPress = false;
            }
            else {
                releasePosition = worldPos;
            }
        }
        
    }

    void RemoveLine() {
        myLineRenderer.positionCount = 0;
    }

    void MovePlayer() {
        timeManager.OnPlayerLaunched();
        Vector3 distance = pressPosition - releasePosition;
        print("Distance of drag = " + distance);
        myRigidBody.AddForce(distance, ForceMode2D.Impulse);
    }

}
