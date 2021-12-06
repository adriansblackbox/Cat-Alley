using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public float moveSpeed = 2.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    public GameStateManager state;
    private float rotX;
    private float rotY;
    public int fails = 0;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update(){
        Aim();
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
        //RaycastHit raycast_hit;
        //var hit = Physics.Raycast(ray, out raycast_hit);
        //var theTag = raycast_hit.collider.gameObject.tag;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && hit.transform.gameObject.CompareTag("cat")) {
                Destroy(hit.transform.gameObject);
                state.addScore();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "hazard")
        {
            Debug.Log("oof");
            this.fails += 1;
        }
    }
}
