using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 1.0f;
    public float JumpForce = 8f;
    public float DuckTime = 1f;
    public float minTurnAngle = -30.0f;
    public float maxTurnAngle = 30.0f;
    public float playerHeight = 1;
    public Transform CameraTransform;
    public GameStateManager state;
    private float timeDucked;
    private float rotX;
    private float rotY;
    private float _groundHeight;
    private bool isGrounded = true;
    private Vector3 duckPosition;
    private Vector3 defaultPosition;

    // Gravity Scale editable on the inspector
    // providing a gravity scale per object
 
    public float GravityScale = -2.0f;
 
    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.
 
    private static float globalGravity = -9.81f;

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _groundHeight = this.transform.position.y;
    }
    void Update(){
        Aim();
        Duck();
        Jump();
        Move();
        GroundCheck();
    }
    private void GroundCheck(){
        if (transform.position.y - playerHeight <= _groundHeight) isGrounded = true;
        else isGrounded = false;
    }
    private void FixedUpdate() {
        Vector3 gravity = globalGravity * GravityScale * Vector3.up;
    }
    private void Move(){
        transform.position -= new Vector3 (0.0f, 0.0f, FindObjectOfType<GameStateManager>().AlleySpeed)* Time.deltaTime;
    }
    private void Aim(){
        rotY += Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * -turnSpeed;
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        rotY = Mathf.Clamp(rotY, minTurnAngle, maxTurnAngle);
        CameraTransform.eulerAngles = new Vector3(rotX, rotY + 180, 0);

        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        float rayLength = 500f;
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && hit.transform.gameObject.CompareTag("cat")) {
                hit.transform.gameObject.SetActive(false);
                state.addScore();
            }
        }
    }
    private void Duck(){
        duckPosition = new Vector3(transform.position.x, 0.3f, transform.position.z);
        if(Input.GetKeyDown(KeyCode.S)){
            timeDucked = DuckTime;
        }
        if(timeDucked > 0){
            this.transform.position = Vector3.Lerp(this.transform.position, duckPosition, Time.deltaTime * 10f);
            timeDucked -= Time.deltaTime;
        }else if(isGrounded){
            defaultPosition = new Vector3(transform.position.x, 1.2f, transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, defaultPosition, Time.deltaTime * 10f);
        }
    }
    private void Jump(){
        if(isGrounded && JumpForce <= 0) 
            JumpForce = 0;
        else 
            JumpForce += GravityScale * Time.deltaTime;

        if(Input.GetKey(KeyCode.W) && isGrounded){
            timeDucked = 0;
            JumpForce = 8f;
        }
        transform.Translate(new Vector3(0, JumpForce, 0) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "hazard")
        {
            state.minusLive();
        }
    }
}
