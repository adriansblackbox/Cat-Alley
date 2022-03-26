using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float MouseSensitivity = 1.0f;
    public float JumpForce = 8f;
    public float DuckTime = 1f;
    public float minTurnAngle = -30.0f;
    public float maxTurnAngle = 30.0f;
    public float playerHeight = 1;
    public Transform CameraTransform;
    public GameStateManager state;
    public GameObject crossHair;
    public Sprite greenCrosshair, redCrosshair;
    public LayerMask catLayer;
    private float timeDucked;
    public float rotX;
    public float rotY;
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

    private void Start() {
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
        if (transform.position.y <= _groundHeight) isGrounded = true;
        else isGrounded = false;
        
    }
    private void FixedUpdate() {
        Vector3 gravity = globalGravity * GravityScale * Vector3.up;
    }
    private void Move(){
        transform.position -= new Vector3 (0.0f, 0.0f, FindObjectOfType<GameStateManager>().AlleySpeed)* Time.deltaTime;
    }
    private void Aim(){
        MouseSensitivity = FindObjectOfType<GameStateManager>().MouseSensitivitySlider.value;
        rotY += Input.GetAxis("Mouse X") * MouseSensitivity;
        rotX += Input.GetAxis("Mouse Y") * -MouseSensitivity;
        rotX = Mathf.Clamp(rotX, minTurnAngle - 10f, maxTurnAngle);
        rotY = Mathf.Clamp(rotY, minTurnAngle, maxTurnAngle);
        CameraTransform.eulerAngles = new Vector3(rotX, rotY + 180, 0);

        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        float rayLength = 20f;
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength, catLayer))
        {
            crossHair.GetComponent<Image>().sprite = greenCrosshair;
            if(Input.GetKeyDown(KeyCode.Mouse0) && !FindObjectOfType<GunMovement>().isShooting){
                hit.transform.gameObject.GetComponent<CatScript>().State = "Satisfied";
                hit.transform.gameObject.GetComponent<Collider>().enabled = false;
                state.addScore();
            }
        }else{
            crossHair.GetComponent<Image>().sprite = redCrosshair;
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
            defaultPosition = new Vector3(transform.position.x, _groundHeight, transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, defaultPosition, Time.deltaTime * 10f);
        }
    }
    private void Jump(){
        if(isGrounded && JumpForce <= 0) 
            JumpForce = 0;
        else 
            JumpForce += GravityScale * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.W) && isGrounded){
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
