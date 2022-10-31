using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Charactor : MonoBehaviour
{
    CharacterController characterController;
    public Animator animator;
    Transform transform;
    int ani_iswalkHash;
    int ani_isfireHash;
    public float moveSpeed;
    public float rotateSpeed;
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;
    Vector3 dir = Vector3.zero;
    Vector2 playerRotate;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        ani_iswalkHash = Animator.StringToHash("isWalk");
        ani_isfireHash = Animator.StringToHash("isFire");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveInput);
        Rotate(lookInput);
    }
    private void Move(Vector2 moveInput)
    {
        dir = new Vector3(moveInput.x,0, moveInput.y);
        if (dir == Vector3.zero)
        {
            animator.SetBool(ani_iswalkHash,false);
        }
        else
        {
            animator.SetBool(ani_iswalkHash, true);
        }
        dir =transform.TransformDirection(dir);
        characterController.SimpleMove(dir * moveSpeed);
    }
    private void Rotate(Vector2 lookInput)
    {
        if (lookInput.sqrMagnitude < 0.01)
        {
            return;
        }
        playerRotate.y += lookInput.x * rotateSpeed*Time.deltaTime;
        playerRotate.x =Mathf.Clamp(playerRotate.x-lookInput.y * rotateSpeed * Time.deltaTime,-89,90);
        transform.localEulerAngles = playerRotate;


    }
    public void GetMoveInput(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
        //Debug.Log("shh");
    }
    public void GetLookInput(InputAction.CallbackContext callbackContext)
    {
        lookInput = callbackContext.ReadValue < Vector2>();
    }
}
