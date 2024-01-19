using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isRunning = false;
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float runSpeed = 8f;

    [Space(5), SerializeField, Range(0f, 1f)]
    private float staminaGauge = 1f; // �����

    [SerializeField]
    private float staminaUsagePerSec = 0.1f;

    private Rigidbody rigid;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    /*
    private Vector3 moveDir = Vector3.zero;
    private Vector3 lookingDir = Vector3.zero;
    */

    [Header("Looking Angle"), SerializeField]
    private Vector2 moveDir = Vector2.zero;
    [SerializeField]
    private Vector2 lookingDir = Vector2.zero;
    [SerializeField]
    private float targetAngle;
    [SerializeField]
    private float percent = 0f;
    [SerializeField]
    private float deltaTime = 1f;

    [Space(10), SerializeField]
    private Vector3 velo = Vector3.zero;
    private void Update()
    { 
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        moveDir = moveDir.normalized;

        if (moveDir != Vector2.zero) // �̵��� �� ���� ���
        {
            //Debug.Log(moveDir + " : " + (Mathf.Atan(moveDir.y / moveDir.x) * Mathf.Rad2Deg - 90));

            targetAngle = -(Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90);

            if (Mathf.Abs(transform.eulerAngles.y - targetAngle) > 180f)
            {
                targetAngle -= 360f * Mathf.Sign(targetAngle);
            }

            lookingDir.y = targetAngle;
            transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, lookingDir, ref velo, deltaTime);
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;

            if (staminaGauge - (staminaUsagePerSec * Time.deltaTime) >= 0f) // ���¹̳ʰ� ����� ��
            {
                 
            }

            staminaGauge = Mathf.Clamp(staminaGauge - (staminaUsagePerSec * Time.deltaTime), 0f, 1f);

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {

        }
    }

    private void SightRenewal(float newSight)
    {

    }
}
