using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    public Transform GunHolder;
    public Transform Camera;
    public float GunRotationSpeed = 1f;
    private Quaternion rotationMod;
    private PlayerController controller;
    public bool isShooting;
    private bool lerpBack, lerpForward;
    public Transform Forward, Back;
    private void Start() {
        controller = FindObjectOfType<PlayerController>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !isShooting){
            isShooting = true;
            StartCoroutine(Shoot());
        }

        
        if(lerpBack){
            LerpBack();
        }else{
            LerpForward();
        }
    }
    private void LateUpdate() {
        this.transform.position = GunHolder.transform.position;
        rotationMod = Quaternion.Euler(controller.rotX - 90, controller.rotY + 180, this.transform.rotation.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationMod, Time.deltaTime * GunRotationSpeed);
    }
    private IEnumerator Shoot(){
        lerpBack = true;
        lerpForward = false;
        yield return new WaitForSeconds(0.05f);
        lerpBack = false;
        lerpForward = true;
        yield return new WaitForSeconds(0.05f);
        lerpBack = false;
        lerpForward = false;
        isShooting = false;
        yield return null;
    }
    public void LerpForward(){
        GunHolder.transform.position = Vector3.Lerp(GunHolder.transform.position, Forward.transform.position, Time.deltaTime * 20f);
    }
    public void LerpBack(){
        GunHolder.transform.position = Vector3.Lerp(GunHolder.transform.position, Back.transform.position, Time.deltaTime * 20f);
    }
}
