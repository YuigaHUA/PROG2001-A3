using UnityEngine;

public class CarSmoothRotator : MonoBehaviour
{
    [Header("旋转设置")]
    [Tooltip("旋转速度（度/秒）")]
    public float rotationSpeed = 90f; // 默认90度/秒
    [Tooltip("旋转轴 (世界坐标系)")]
    public Vector3 rotationAxis = Vector3.up; // 默认绕Y轴旋转
    [Tooltip("启用平滑过渡")]
    public bool smoothTransition = true;
    [Tooltip("平滑过渡时间")]
    [Range(0.1f, 2f)] public float smoothTime = 0.5f;

    private Quaternion _targetRotation;
    private float _currentAngularVelocity;

    void Start()
    {
        // 初始化目标旋转为当前朝向
        _targetRotation = transform.rotation;
    }

    void Update()
    {
        UpdateRotation();
    }

    void UpdateRotation()
    {
        // 计算每帧应该旋转的角度（考虑帧率差异）
        float step = rotationSpeed * Time.deltaTime;

        if (smoothTransition)
        {
            // 使用Slerp平滑过渡
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                _targetRotation,
                smoothTime * Time.deltaTime
            );
        }
        else
        {
            // 直接旋转（无平滑）
            transform.rotation = _targetRotation;
        }

        // 更新目标旋转角度（累积旋转）
        _targetRotation *= Quaternion.AngleAxis(step, rotationAxis);
    }

    // 在编辑器中可视化旋转轴
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + rotationAxis.normalized * 2f);
        Gizmos.DrawSphere(transform.position + rotationAxis.normalized * 2f, 0.1f);
    }
}