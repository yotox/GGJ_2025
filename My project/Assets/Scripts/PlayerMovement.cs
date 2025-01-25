using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;

    [Header("Cámara Dinámica")]
    public Transform cameraTransform;
    public float tiltAmount = 5f;
    public float bobSpeed = 5f;
    public float bobAmount = 0.05f;
    private float defaultYPos;
    private float bobTimer = 0f;

    [Header("Disparo")]
    public float fireRate = 0.2f;
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparan los proyectiles
    public float bulletSpeed = 20f;

    private CharacterController characterController;
    private Vector3 velocity;
    private float xRotation = 0f;
    private float nextTimeToFire = 0f;
    private float currentSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        currentSpeed = walkSpeed;
        defaultYPos = cameraTransform.localPosition.y;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleCameraEffects();

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && vertical > 0;
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }

        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleCameraEffects()
    {
        float targetTilt = Input.GetKey(KeyCode.LeftShift) ? -tiltAmount : 0f;
        cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, Quaternion.Euler(xRotation, 0f, targetTilt), Time.deltaTime * 5f);

        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float newY = defaultYPos + Mathf.Sin(bobTimer) * bobAmount;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, newY, cameraTransform.localPosition.z);
        }
        else
        {
            bobTimer = 0f;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, defaultYPos, cameraTransform.localPosition.z);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        // Asegurar que el proyectil tenga el script Bullet
        if (bullet.GetComponent<Bullet>() == null)
        {
            bullet.AddComponent<Bullet>();
        }
    }

}

