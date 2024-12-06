using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target; // 主角的 Transform 組件
    public float distance = 10.0f; // 距離主角的距離
    public float height = 5.0f; // 距離主角的高度
    public float damping = 2.0f; // 滑動速度
    public float followSpeed = 10.0f; // 鏡頭跟隨速度
    public bool lockZRotation = false; // 是否鎖定 Z 軸轉向
    public float rotationSpeed = 1.0f; // 鏡頭旋轉速度
    public float minVerticalAngle = -60.0f; // 最小垂直旋轉角度
    public float maxVerticalAngle = 60.0f; // 最大垂直旋轉角度

    

    void LateUpdate () {
        float targetRotationAngle = target.eulerAngles.y; // 獲取主角的水平旋轉角度
        float currentRotationAngle = transform.eulerAngles.y; // 獲取鏡頭的水平旋轉角度
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, followSpeed * Time.deltaTime); // 平滑過渡鏡頭水平旋轉角度

        Quaternion rotation;
        if (lockZRotation) {
            rotation = Quaternion.Euler(transform.eulerAngles.x, currentRotationAngle, 0); // 只旋轉水平方向
        } else {
            rotation = Quaternion.Euler(transform.eulerAngles.x, currentRotationAngle, transform.eulerAngles.z); // 垂直和水平方向都旋轉
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, damping * Time.deltaTime); // 平滑過渡鏡頭旋轉角度

        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height); // 計算目標位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime); // 平滑過渡鏡頭位置
    }

    void Update () {
    float horizontalRotation = Input.GetAxis("Horizontal") * rotationSpeed; // 根據輸入和旋轉速度計算水平旋轉角度
    float verticalRotation = -Input.GetAxis("Vertical") * rotationSpeed; // 根據輸入和旋轉速度計算垂直旋轉角度，注意要加負號

    Vector3 euler = transform.eulerAngles; // 獲取當前旋轉角度
    euler.y += horizontalRotation; // 水平旋轉
    euler.x = Mathf.Clamp(euler.x + verticalRotation, minVerticalAngle, maxVerticalAngle); // 垂直旋轉並限制角度範圍
    transform.eulerAngles = euler; // 設置新的旋轉角度
    }
}

