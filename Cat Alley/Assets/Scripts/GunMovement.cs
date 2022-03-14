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
    private void Start() {
        controller = FindObjectOfType<PlayerController>();
    }
    private void LateUpdate() {
        this.transform.position = GunHolder.transform.position;
        rotationMod = Quaternion.Euler(controller.rotX - 90, controller.rotY + 180, this.transform.rotation.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationMod, Time.deltaTime * GunRotationSpeed);
    }
}
