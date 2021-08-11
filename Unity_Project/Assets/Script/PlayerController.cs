using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //이동
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private float currentSpeed;

    private Rigidbody playerRigid;

    private bool isRun = false;

    //회전
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float rotationLimit;

    private float currentCameraRotationX = 0;

    [SerializeField]
    private Camera playerCamera;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }


    void Update()
    {
        RotationY();
        RotationX();
    }

    void FixedUpdate()
    {
        Move();
        Run();
    }

    //이동
    private void Move()
    {
        float _moveX = Input.GetAxisRaw("Horizontal");
        float _moveZ = Input.GetAxisRaw("Vertical");
        

        Vector3 _moveHorizontal = transform.right * _moveX;
        Vector3 _moveVertical = transform.forward * _moveZ;

        Vector3 _moveDir = (_moveHorizontal + _moveVertical).normalized * currentSpeed;

        playerRigid.MovePosition(transform.position + _moveDir * Time.deltaTime);
    }

    //Y축 회전
    private void RotationY()
    {
        float _rotationX = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _rotationX * rotationSpeed;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -rotationLimit, rotationLimit);
        playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        
    }

    //X축 회전
    private void RotationX()
    {
        float _rotationY = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _rotationY, 0f) * rotationSpeed;
        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    //달리기
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
            currentSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            currentSpeed = walkSpeed;
        }
    }

}
