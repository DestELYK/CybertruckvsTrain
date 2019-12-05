using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera camera;

    public float minPitch = -10;
    public float maxPitch = 30;

    Transform cameraTarget, cameraFollow, turret;

    [SerializeField]
    float maxDistance;
    [SerializeField]
    float currentDistance;
    public float yaw, pitch;

    Vector3 dir;

    private void Awake()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        cameraTarget = transform.Find("Camera Target");
        cameraFollow = transform.Find("Camera Follow");
        turret = transform.Find("Turret");

        maxDistance = (cameraTarget.position - cameraFollow.position).magnitude;
        currentDistance = maxDistance;

        yaw = 0.0f;
        pitch = 0.0f;

        dir = (cameraFollow.position - cameraTarget.position).normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        yaw += mouseX * 100 * Time.deltaTime;
        pitch = Mathf.Clamp(pitch + mouseY * 100 * Time.deltaTime, minPitch, maxPitch);

        currentDistance = Mathf.Lerp(3.5f, maxDistance, (pitch - minPitch) / (maxPitch - minPitch));

        cameraFollow.position = cameraTarget.position + dir * currentDistance;
    }

    private void FixedUpdate()
    {
        camera.transform.position = Vector3.Slerp(cameraFollow.position, camera.transform.position, 20 * Time.deltaTime);

        camera.transform.RotateAround(cameraTarget.position, Vector3.right, pitch);
        camera.transform.RotateAround(cameraTarget.position, Vector3.up, yaw);

        camera.transform.LookAt(cameraTarget);
    }
}
