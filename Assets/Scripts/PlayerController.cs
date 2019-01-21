using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;

    [SerializeField]
    private float crouchSpeed;



    [SerializeField]
    private float jumpForce;

    //얼마나 앉을지 결정하는 변수 
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;


    //땅착지 여부
    private CapsuleCollider CapsuleCollider;


    //상태변수
    private bool isRun;
    private bool isCrouch;
    private bool isGround;

    //민감도
    [SerializeField]
    private float lookSeneitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0f;


    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;


    // Start is called before the first frame update
    void Start()
    {
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;

        CapsuleCollider = GetComponent<CapsuleCollider>();
        applySpeed = walkSpeed;
        theCamera = FindObjectOfType<Camera>();
        myRigid = GetComponent<Rigidbody>();



    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();


        Move();
        CameraRotation();
        CharacterRotation();

    }

    private void TryCrouch() {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            Crouch();
        }

    }
    private void Crouch() {
        isCrouch = !isCrouch;

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;

        }

        //theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, applyCrouchPosY, theCamera.transform.localPosition.z);
        StartCoroutine(CrouchCoroutine());

    }

    IEnumerator CrouchCoroutine() {

        int count = 0;


        float _posY = theCamera.transform.localPosition.y;
        while (_posY != applyCrouchPosY) {
            count++;
            _posY = Mathf.Lerp(_posY,applyCrouchPosY,0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);

            if (count > 15) {
                break;
            }

            yield return null;

        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);





    }



    private void IsGround() {
        isGround=Physics.Raycast(transform.position, Vector3.down, CapsuleCollider.bounds.extents.y+0.1f);

    }

    private void TryJump() {
        

        if (Input.GetKeyDown(KeyCode.Space) && isGround == true) {
            Jump();
        }

    }
    private void Jump() {
        if (isCrouch)
        {
            Crouch();
        }

        myRigid.velocity = transform.up * jumpForce;
    }








    private void TryRun() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }
    private void Running() {
        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }




    private void Move() {
        float _MoveDirX = Input.GetAxisRaw("Horizontal");
        float _MoveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _MoveDirX;
        Vector3 _moveVertical = transform.forward * _MoveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);



    }


    private void CameraRotation() {

        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSeneitivity;
        currentCameraRotationX += _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }

    private void CharacterRotation() {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSeneitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));



    }



}
