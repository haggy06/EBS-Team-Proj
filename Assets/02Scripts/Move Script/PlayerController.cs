using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool canControll = true;
    public bool CanCotroll => canControll;

    [SerializeField]
    private bool isRunning = false;
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float runSpeed = 8f;
    public float MoveSpeed { get => isRunning ? runSpeed : walkSpeed; }

    [Space(5), SerializeField, Range(0f, 1f)]
    private float staminaGauge = 1f; // 백분율

    [SerializeField]
    private float staminaUsagePerSec = 0.1f;
    [SerializeField]
    private float staminaRecoveryPerSec = 0.1f;
    [SerializeField]
    private float exhaustionTerm = 2f;

    private Rigidbody rigid;
    private Animator anim;
    private CharacterController controller;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        InvincibleCanvasManager.Inst.Player_UI.StaminaRenewal(staminaGauge);

        GameManager.Inst.SetPlayer(this);
    }

    /*
    private Vector3 moveDir = Vector3.zero;
    private Vector3 lookingDir = Vector3.zero;
    */

    [Header("Looking Angle"), SerializeField]
    private Vector3 moveDir = Vector2.zero;
    [SerializeField]
    private Vector2 lookingDir = Vector2.zero;
    private void Update()
    { 
        if (canControll)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");
            moveDir = moveDir.normalized;

            if (moveDir != Vector3.zero) // 이동을 했을 경우
            {
                anim.SetBool(PlayerHash.Walking, true);
                /*
                velo.x = Mathf.Clamp(rigid.velocity.x + MoveSpeed * moveDir.x * Time.deltaTime, -MoveSpeed, MoveSpeed);
                velo.y = Mathf.Clamp(rigid.velocity.y + MoveSpeed * moveDir.y * Time.deltaTime, -MoveSpeed, MoveSpeed);
                rigid.velocity = velo;
                */

                controller.Move(MoveSpeed * moveDir * Time.deltaTime);
                controller.Move(MoveSpeed * Vector3.down * Time.deltaTime);

                lookingDir.y = -(Mathf.Atan2(moveDir.z, moveDir.x) * Mathf.Rad2Deg - 90);
                transform.eulerAngles = lookingDir;

                /*if (Mathf.Abs(curLookingDir) > 180f)
                {
                    curLookingDir -= Mathf.Sign(curLookingDir) * 360f;
                }
                */
                //Debug.Log(moveDir + " : " + (Mathf.Atan(moveDir.y / moveDir.x) * Mathf.Rad2Deg - 90));

                //lookingDir.y = -(Mathf.Atan2(moveDir.z, moveDir.x) * Mathf.Rad2Deg - 90);
                //lookingDir.y += lookingDir.y < 0 ? 360f : 0f;
                /*if (Mathf.Abs(Mathf.Abs(curLookingDir) - Mathf.Abs(lookingDir.y)) > 180f)
                {
                    if (lookingDir.y < 90f)
                    {

                    }
                }*/

                /*
                if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.y) - Mathf.Abs(lookingDir.y)))
                {

                }
                */
                /*
                if (Mathf.Abs(Mathf.Abs(curLookingDir) - Mathf.Abs(lookingDir.y)) > 180f)
                {
                    Debug.Log(curLookingDir + "와" + lookingDir.y  + "의 차 = " + Mathf.Abs(Mathf.Abs(curLookingDir) - Mathf.Abs(lookingDir.y)) + " : " + lookingDir.y + " → " + (lookingDir.y - 360f * Mathf.Sign(lookingDir.y)));
                    lookingDir.y -= 360f * Mathf.Sign(lookingDir.y);
                }*/



                //lookingDir.y

                //transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, lookingDir, ref velo, deltaTime);
                //transform.eulerAngles = Vector2.MoveTowards(transform.eulerAngles, lookingDir, deltaTime);


                /*
                if (Mathf.Abs(lookingDir.y) > 180f)
                {
                    lookingDir.y -= Mathf.Sign(targetAngle) * 360f;

                    transform.eulerAngles = lookingDir;
                }*/
                //transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, lookingDir, deltaTime);
                /*
                if (Mathf.Approximately(targetAngle, transform.eulerAngles.y)) // 목표 각도에 도달했을 경우
                {
                    percent = 0f; // 타이머를 0으로 초기화
                }
                else //목표 각도에 도달하지 못했을 경우
                {
                    percent += Time.deltaTime * 0.5f; // 타이머 진행

                    lookingDir.y = Mathf.Lerp(transform.eulerAngles.y, targetAngle, percent);
                    //lookingDir.y = transform.eulerAngles.y + (transform.eulerAngles.y - targetAngle) * Time.deltaTime;

                    if (Mathf.Abs(lookingDir.y) > 180f)
                    {
                        lookingDir.y -= Mathf.Sign(targetAngle) * 360f;
                    }

                    transform.eulerAngles = lookingDir;
                }
                */

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //anim.SetBool(PlayerHash.Walking, false);

                    if (!isRunning) // 뛰고 있지 않을 경우
                    {
                        isRunning = true;

                        anim.SetBool(PlayerHash.Running, true);

                        StopCoroutine("StaminaRefreshLogic");
                    }

                    if (staminaGauge - (staminaUsagePerSec * Time.deltaTime) >= 0f) // 스태미너가 충분할 때
                    {
                        //anim.SetBool(PlayerHash.Walking, false);

                        staminaGauge = Mathf.Clamp(staminaGauge - (staminaUsagePerSec * Time.deltaTime), 0f, 1f);
                        InvincibleCanvasManager.Inst.Player_UI.StaminaRenewal(staminaGauge);
                    }
                    else // 스태미너가 모자랄 때
                    {
                        canControll = false;
                        isRunning = false;

                        anim.SetBool(PlayerHash.Walking, false);
                        anim.SetBool(PlayerHash.Running, false);
                        anim.SetBool(PlayerHash.Exhausted, true);

                        Invoke("StaminaRefresh", 1f);
                        Invoke("ExhaustionRecovery", exhaustionTerm);
                    }
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    if (isRunning) // 뛰고 있을 경우
                    {
                        isRunning = false;
                        anim.SetBool(PlayerHash.Running, false);
                        //anim.SetBool(PlayerHash.Walking, true);

                        Invoke("StaminaRefresh", 1f);
                    }
                }
            }
            else // 움직이지 않았을 경우
            {
                Invoke("WalkStop", 0.075f);

                if (isRunning) // 뛰고 있을 경우
                {
                    isRunning = false;
                    Invoke("RunStop", 0.075f);

                    Invoke("StaminaRefresh", 1f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("C키 입력");

                if (InvincibleCanvasManager.Inst.CurActTarget != null)
                {
                    InvincibleCanvasManager.Inst.CurActTarget.Interact();

                    anim.SetBool(PlayerHash.Walking, false);
                    anim.SetBool(PlayerHash.Running, false);
                    canControll = false;
                }
            }
            /*
            moveDir.x = Input.GetAxis("Horizontal");
            moveDir.y = rigid.velocity.y / (isRunning ? runSpeed : walkSpeed);
            moveDir.z = Input.GetAxis("Vertical");

            rigid.velocity = moveDir * (isRunning ? runSpeed : walkSpeed);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // 이동을 조금이라도 했을 경우
            {
                lookingDir.y = Mathf.Atan(moveDir.z / moveDir.x) * Mathf.Rad2Deg;

                transform.eulerAngles = lookingDir;
            }
            */
        }
    }
    private void WalkStop()
    {
        if (moveDir == Vector3.zero)
        {
            anim.SetBool(PlayerHash.Walking, false);
        }
    }
    private void RunStop()
    {
        if (moveDir == Vector3.zero || !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool(PlayerHash.Running, false);
        }
    }

    private void ExhaustionRecovery()
    {
        anim.SetBool(PlayerHash.Exhausted, false);

        canControll = true;

        anim.SetBool(PlayerHash.Walking, (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0));
        anim.SetBool(PlayerHash.Running, Input.GetKey(KeyCode.LeftShift));
    }

    private void StaminaRefresh()
    {
        if (!isRunning)
        {
            StopCoroutine("StaminaRefreshLogic");

            StartCoroutine("StaminaRefreshLogic");
        }
    }
    private IEnumerator StaminaRefreshLogic()
    {
        while (true)
        {
            staminaGauge = Mathf.Clamp(staminaGauge + (staminaRecoveryPerSec * Time.deltaTime), 0f, 1f);
            InvincibleCanvasManager.Inst.Player_UI.StaminaRenewal(staminaGauge);

            if (staminaGauge >= 1f)
            {
                StopCoroutine("StaminaRefreshLogic");
            }

            yield return null;
        }
    }
    public void ControllSwitch(bool mode)
    {
        canControll = mode;
    }
    private void SightRenewal(float newSight)
    {

    }
}
