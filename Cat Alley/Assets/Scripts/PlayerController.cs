using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 1.0f;
    public float JumpForce = 15f;
    public float moveSpeed = 2.0f;
    public float minTurnAngle = -60.0f;
    public float JumpTime = 1.5f;
    public float maxTurnAngle = 60.0f;
    public Collider playerCollider;
    public GameStateManager state;
    private float rotX;
    private float rotY;
    private float jumpTimer;
    public int fails = 0;

    // Gravity Scale editable on the inspector
    // providing a gravity scale per object
 
    public float GravityScale = 2.0f;
 
    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.
 
    private static float globalGravity = -9.81f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GetComponent<Rigidbody>().useGravity = false;
    }
    void Update(){
        Aim();
        Jump();
    }
    private void FixedUpdate() {
        Vector3 gravity = globalGravity * GravityScale * Vector3.up;
        GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
    }
    private void Aim(){
        rotY += Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * -turnSpeed;
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        rotY = Mathf.Clamp(rotY, minTurnAngle, maxTurnAngle);
        transform.eulerAngles = new Vector3(-rotX, rotY, 0);

        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        float rayLength = 500f;
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && hit.transform.gameObject.CompareTag("cat")) {
                Destroy(hit.transform.gameObject);
                state.addScore();
            }
        }
    }
    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && jumpTimer <= 0){
            jumpTimer = JumpTime;
            GetComponent<Rigidbody>().velocity = new Vector3 (0.0f, JumpForce, 0.0f);
        }
        if(jumpTimer > 0){
            jumpTimer -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "hazard")
        {
            Debug.Log("oof");
            this.fails += 1;
            state.minusLive();
        }
    }
}
