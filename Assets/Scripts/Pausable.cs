using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausable : MonoBehaviour
{

    [SerializeField] TimeManager timeManager;

    Rigidbody2D myRigidBody;
    Rotating rotating;

    private float originalGravityScale;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        rotating = GetComponent<Rotating>();

        originalGravityScale = myRigidBody.gravityScale;

        if(timeManager == null) {
            timeManager = FindObjectOfType<TimeManager>();
        }

        RegisterWithTimeManager();
    }

    void RegisterWithTimeManager() {
        timeManager.OnPauseSet += HandlePauseChange;
    }

    void HandlePauseChange(bool isPaused) {
        if(isPaused) {
            PauseObject();
        } else {
            ResumeObject();
        }
    }

    void PauseObject() {
        myRigidBody.velocity = Vector2.zero;
        //myRigidBody.angularVelocity = 0f;
        myRigidBody.freezeRotation = true;
        myRigidBody.gravityScale = 0f;
        if(rotating != null) {
            rotating.enabled = false;
        }
    }

    void ResumeObject() {
        //myRigidBody.angularVelocity = 0f;
        myRigidBody.freezeRotation = false;
        myRigidBody.gravityScale = originalGravityScale;

        if (rotating != null) {
            rotating.enabled = true;
        }
    }
}
