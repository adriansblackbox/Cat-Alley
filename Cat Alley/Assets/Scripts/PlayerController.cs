using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public float JumpForce = 2f;
    public float moveSpeed = 2.0f;
    public float minTurnAngle = -90.0f;
    public float JumpTime = 1.5f;
    public float maxTurnAngle = 90.0f;
    public Collider playerCollider;
    public GameStateManager state;
    private float rotX;
    private float rotY;
    private float jumpTimer;
    private float gravity = -9.8f;
    public int fails = 0;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update(){
        Aim();
        Jump();
    }
    private void Aim(){
        rotY += Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * -turnSpeed;
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        rotY = Mathf.Clamp(rotY, minTurnAngle, maxTurnAngle);
        transform.eulerAngles = new Vector3(-rotX, rotY, 0);

        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        float rayLength = 500f;
        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        // debug Ray
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
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
        if(Input.GetKeyDown(KeyCode.W) && jumpTimer <= 0){
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
