using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    // https://pressstart.vip/tutorials/2018/11/9/78/perspective-camera-panning.html

    private Vector3 touchStart;
    public Camera cam;
    public float groundZ = 0;

    [Header("카메라 고정 위치 변수")]
    public Transform[] fixedTransformArr;

    private const float SLIDE_SENSITIVITY = 10f;
    private bool knowsDir;
    private bool isHorizontal;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            knowsDir = false;
            touchStart = GetWorldPosition(groundZ);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - GetWorldPosition(groundZ);

            // 이동 처리
            checkHorizontal(direction);
            if (isHorizontal)
                direction.y = 0;
            else
                direction.x = 0;

            // 카메라 이동
            cam.transform.position += direction;
        }
        if (Input.GetMouseButtonUp(0))
        {
            knowsDir = false;
            Vector3 nearPos = findNearPos(cam.transform);
            Vector3 destination = new Vector3(nearPos.x, nearPos.y, cam.transform.position.z);
            transform.DOMove(destination, 0.5f);
        }
    }

    private Vector3 findNearPos(Transform camPos)
    {
        Vector3 nearPos = new Vector3();
        float minDistance = 999999f;

        // 카메라와 근접한 위치 찾기
        foreach (Transform fixedTransform in fixedTransformArr)
        {
            float distance = Vector3.Distance(camPos.position, fixedTransform.position);

            if (distance < minDistance)
            {
                nearPos = fixedTransform.position;
                minDistance = distance;
            }
        }
        return nearPos;
    }


    private void checkHorizontal(Vector3 dir)
    {
        if (!knowsDir)
        {
            // x값과 y값을 서로 비교하여 값이 큰 좌표를 기준으로 한다
            // 상대적으로 작은 좌표는 0으로 바꾼다
            float x = Mathf.Abs(dir.x);
            float y = Mathf.Abs(dir.y);

            // 좌우
            if (x > y && x > SLIDE_SENSITIVITY)
            {
                isHorizontal = true;
                knowsDir = true;
            }
            // 상하
            else if (x < y && y > SLIDE_SENSITIVITY)
            {
                isHorizontal = false;
                knowsDir = true;
            }
        }
    }


    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(mousePos);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}
