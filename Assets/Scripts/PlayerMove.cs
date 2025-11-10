using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;         // 이동 속도
    CharacterController cc;         // CharacterController 컴포넌트

    public float gravity = -20;     // 중력 가속도의 크기
    float yVelocity = 0;            // 수직 속도

    public float jumpPower = 5;     // 점프 크기

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 입력에 따라 전후좌우로 이동하고 싶다.
        // 1. 사용자의 입력을 받는다.
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");

        // 2. 방향을 만든다.
        Vector3 dir = new Vector3(h, 0, v);

        // 2-0. 사용자가 바라보는 방향으로 입력 값 변화시키기
        dir = Camera.main.transform.TransformDirection(dir);

        // 2-1. 중력을 적용한 수직 방향 추가 v = v0 + at
        // F = m(무게) * a(가속도)
        // 가속도 = 속도 * 시간
        // 속도 = 거리 / 시간
        yVelocity += gravity * Time.deltaTime;

        // 2-2. 바닥에 있을 경우, 수직 항력을 처리하기 위해 속도를 0으로 한다.
        if(cc.isGrounded)
        {
            yVelocity = 0;
        }

        dir.y = yVelocity;      // 2-1. "

        // 2-3. 사용자가 점프 버튼을 누르면 속도에 점프 크기를 할당한다.
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch))
        {
            yVelocity = jumpPower;
        }

        // 3. 이동한다.
        cc.Move(dir * speed * Time.deltaTime);
    }
}
