using UnityEngine;

public class ResetAll : MonoBehaviour
{
    private Vector3 initialPosition;  // 改为不可变的初始位置
    private Quaternion initialRotation;
    private Rigidbody rb;

    void Awake()  // 改用Awake确保最早执行
    {
        // 只在游戏启动时记录一次初始位置
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();

        Debug.Log($"初始位置已记录：{initialPosition}");
    }

    public void ResetToStart()
    {
        // 始终重置到最初位置
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("已重置到初始位置：" + initialPosition);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToStart();
        }
    }
}