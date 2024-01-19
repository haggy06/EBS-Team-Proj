using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool canControll = true;
    [SerializeField]
    private bool isRunning = false;
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float runSpeed = 8f;
    public float MoveSpeed { get => isRunning ? runSpeed : walkSpeed; }

    [Space(5), SerializeField, Range(0f, 1f)]
    private float staminaGauge = 1f; // �����

    [SerializeField]
    private float staminaUsagePerSec = 0.1f;
    [SerializeField]
    private float staminaRecoveryPerSec = 0.1f;

    private Rigidbody rigid;
    private Animator anim;
    private CharacterController controller;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    /*
    private Vector3 moveDir = Vector3.zero;
    private Vector3 lookingDir = Vector3.zero;
    */

    [Header("Looking Angle"), SerializeField]
    private Vector3 moveDir = Vector2.zero;
    [SerializeField]
    private Vector2 lookingDir = Vector2.zero;
    [SerializeField]
    private Vector3 velo = Vector3.zero;
    private void Update()
    { 
        if (canControll)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");
            moveDir = moveDir.normalized;

            if (moveDir != Vector3.zero) // �̵��� ���� ���
            {
                Debug.Log(MoveSpeed);
                /*
                velo.x = Mathf.Clamp(rigid.velocity.x + MoveSpeed * moveDir.x * Time.deltaTime, -MoveSpeed, MoveSpeed);
                velo.y = Mathf.Clamp(rigid.velocity.y + MoveSpeed * moveDir.y * Time.deltaTime, -MoveSpeed, MoveSpeed);
                rigid.velocity = velo;
                */

                controller.Move(MoveSpeed * moveDir * Time.deltaTime);

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
                    Debug.Log(curLookingDir + "��" + lookingDir.y  + "�� �� = " + Mathf.Abs(Mathf.Abs(curLookingDir) - Mathf.Abs(lookingDir.y)) + " : " + lookingDir.y + " �� " + (lookingDir.y - 360f * Mathf.Sign(lookingDir.y)));
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
                if (Mathf.Approximately(targetAngle, transform.eulerAngles.y)) // ��ǥ ������ �������� ���
                {
                    percent = 0f; // Ÿ�̸Ӹ� 0���� �ʱ�ȭ
                }
                else //��ǥ ������ �������� ������ ���
                {
                    percent += Time.deltaTime * 0.5f; // Ÿ�̸� ����

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
                    if (!isRunning) // �ٰ� ���� ���� ���
                    {
                        StopCoroutine("StaminaRefreshLogic");
                        isRunning = true;
                    }
                    if (staminaGauge - (staminaUsagePerSec * Time.deltaTime) >= 0f) // ���¹̳ʰ� ����� ��
                    {
                        staminaGauge = Mathf.Clamp(staminaGauge - (staminaUsagePerSec * Time.deltaTime), 0f, 1f);
                    }
                    else // ���¹̳ʰ� ���ڶ� ��
                    {
                        // todo : Ż�� ����

                        isRunning = false;

                        Invoke("StaminaRefresh", 1f);
                    }
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    if (isRunning) // �ٰ� ���� ���
                    {
                        isRunning = false;

                        Invoke("StaminaRefresh", 1f);
                    }
                }
            }
            else // �������� �ʾ��� ���
            {
                if (isRunning) // �ٰ� ���� ���
                {
                    isRunning = false;

                    Invoke("StaminaRefresh", 1f);
                }
            }

            /*
            moveDir.x = Input.GetAxis("Horizontal");
            moveDir.y = rigid.velocity.y / (isRunning ? runSpeed : walkSpeed);
            moveDir.z = Input.GetAxis("Vertical");

            rigid.velocity = moveDir * (isRunning ? runSpeed : walkSpeed);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // �̵��� �����̶� ���� ���
            {
                lookingDir.y = Mathf.Atan(moveDir.z / moveDir.x) * Mathf.Rad2Deg;

                transform.eulerAngles = lookingDir;
            }
            */
        }
    }

    private void StaminaRefresh()
    {
        if (!isRunning)
        {
            StartCoroutine("StaminaRefreshLogic");
        }
    }
    private IEnumerator StaminaRefreshLogic()
    {
        while (true)
        {
            staminaGauge = Mathf.Clamp(staminaGauge + (staminaRecoveryPerSec * Time.deltaTime), 0f, 1f);

            if (staminaGauge >= 1f)
            {
                StopCoroutine("StaminaRefreshLogic");
            }

            yield return null;
        }
    }

    private void SightRenewal(float newSight)
    {

    }
}
