using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Charactor : MonoBehaviour
{
    CharacterController characterController;
    public Animator animator;
    public Transform shootPoint;
    public float shootRange;
    public AudioSource audioSource_shoot;
    public AudioClip audioClip_shoot;
    public GameObject shootHitEffect;
    public GameObject[] Enemy;
    int ani_iswalkHash;
    int ani_isfireHash;
    public float moveSpeed;
    public float rotateSpeed;
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;
    bool fireInput;
    Vector3 dir = Vector3.zero;
    Vector2 playerRotate;
    Enemy enemy ;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        ani_iswalkHash = Animator.StringToHash("isWalk");
        ani_isfireHash = Animator.StringToHash("isFire");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveInput);
        Rotate(lookInput);
        Fire(fireInput);

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
    private void Fire(bool fireInput)
    {
        if (!fireInput)
        {
            return;
        }
        animator.SetTrigger(ani_isfireHash);
        RaycastHit raycastHit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out raycastHit, shootRange))
        {
         
            audioSource_shoot.PlayOneShot(audioClip_shoot);
            GameObject effect = Instantiate(shootHitEffect);
            effect.transform.position = raycastHit.point;
            effect.transform.forward = raycastHit.normal;
            effect.SetActive(true);
            Destroy(effect, 1f);
        }
        if (raycastHit.transform.tag == "Enemy")
        {

            enemy = raycastHit.transform.GetComponent<Enemy>();
            enemy.Hit();
            ResultPanel.Instance.AddScore();
        }
        

    }
    #region
    public void GetMoveInput(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
        //Debug.Log("shh");
    }
    public void GetLookInput(InputAction.CallbackContext callbackContext)
    {
        lookInput = callbackContext.ReadValue < Vector2>();
    }
    public void GetFireInput(InputAction.CallbackContext callbackContext)
    {
        fireInput = callbackContext.ReadValueAsButton();
    }
    #endregion
}
