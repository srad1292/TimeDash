using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] TimeManager timeManager;
    [SerializeField] LevelLoader levelLoader;

    Rigidbody2D myRigidBody;
    LineRenderer myLineRenderer;
    SpriteRenderer mySpriteRenderer;
    
    bool newPress = true;
    bool isPressed = false;
    bool moveable = true;
    Vector3 pressPosition = Vector2.zero;
    Vector3 releasePosition = Vector2.zero;
    Camera mainCamera;
    Vector3 startPosition;
    Color normalColor;
    Color deathColor;
    Color winColor;
    float normalGravityScale;
    bool collectedToken = false;

    bool timerStarted = false;
    int timeToBeat = 0;


    private void Start() {
        mainCamera = Camera.main;
        startPosition = transform.position;
        
        myRigidBody = GetComponent<Rigidbody2D>();
        myLineRenderer = GetComponent<LineRenderer>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        normalGravityScale = myRigidBody.gravityScale;

        normalColor = mySpriteRenderer.color;
        deathColor = new Color(1f, 0.2f, 0.2f);
        winColor = new Color(1f, 1f, 1f);
    }

    private void Update() {
        if (timerStarted) {
            timeToBeat += (int)(Time.deltaTime * 1000);
        }

        if (isPressed) {
            myLineRenderer.positionCount = 2;
            myLineRenderer.SetPosition(0, releasePosition);
            myLineRenderer.SetPosition(1, pressPosition);
        }
        
    }


    void OnTouchInput(InputValue iv) {
        if(moveable) {
            if(!timerStarted) {
                timerStarted = true;
            }
            print("On touch. Is pressed: " + iv.isPressed);
            if (iv.isPressed == true) {
                isPressed = true;
            }
            else {
                isPressed = false;
                newPress = true;
                RemoveLine();
                MovePlayer();
            }
        }
        
    }

    void OnTouchPosition(InputValue iv) {
        if(isPressed && moveable) {
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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag != "Podium") {
            KillPlayer();
        }
    }

    void KillPlayer() {
        moveable = false;
        mySpriteRenderer.color = deathColor;
        myRigidBody.velocity = Vector2.zero;
        myRigidBody.gravityScale = 0f;
        timeManager.StartLevel();
        StartCoroutine(Respawn());

    }
    IEnumerator Respawn() {
        yield return new WaitForSeconds(0.7f);
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        mySpriteRenderer.color = normalColor;
        myRigidBody.gravityScale = normalGravityScale;
        moveable = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Goal") {
            HandleGoalReached(other.gameObject.transform.position);
        } else if(other.gameObject.tag == "ChallengeToken") {
            collectedToken = true;
            Destroy(other.gameObject);
        }
    }

    void HandleGoalReached(Vector3 goalPosition) {
        // For now just restart same level
        timerStarted = false;
        moveable = false;
        myRigidBody.velocity = Vector2.zero;
        myRigidBody.gravityScale = 0f;
        transform.position = goalPosition;
        mySpriteRenderer.color = winColor;
        StartCoroutine(BeatLevel());
    }

    IEnumerator BeatLevel() {
        yield return new WaitForSeconds(0.7f);
        GameData.Instance.HandleLevelFinished(timeToBeat, collectedToken);
        levelLoader.LoadLevelSelect();
    }



}
