using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //�̵�
    [SerializeField]
    private float walkSpeed;

    private Rigidbody playerRigid;

    //ȸ��
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
    }


    void Update()
    {
        RotationY();
        RotationX();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float _moveX = Input.GetAxisRaw("Horizontal");
        float _moveZ = Input.GetAxisRaw("Vertical");
        

        Vector3 _moveHorizontal = transform.right * _moveX;
        Vector3 _moveVertical = transform.forward * _moveZ;

        Vector3 _moveDir = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        playerRigid.MovePosition(transform.position + _moveDir * Time.deltaTime);
    }

    private void RotationY()
    {
        float _rotationX = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _rotationX * rotationSpeed;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -rotationLimit, rotationLimit);
        playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        
    }

    private void RotationX()
    {
        float _rotationY = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _rotationY, 0f) * rotationSpeed;
        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

}
