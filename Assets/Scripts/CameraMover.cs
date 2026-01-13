using UnityEngine;

public class CameraMover : MonoBehaviour
{

    //���������� ��� ����� Input System ���������� ��� ��� �������� ���� ���� �������� ����������� ���������. ��������� �����������������.
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minZoom = 5f;
    [SerializeField] private float _maxZoom = 25f;

    [SerializeField] private Rigidbody _rigidbody;

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void Update()
    {
        ZoomCamera();
    }

    private void MoveCamera()
    {
        float h = Input.GetAxis("Horizontal"); // A / D
        float v = Input.GetAxis("Vertical");   // W / S

        Vector3 move = new Vector3(h, 0f, v).normalized * _moveSpeed;
        _rigidbody.linearVelocity = new Vector3(move.x, _rigidbody.linearVelocity.y, move.z);
    }

    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll == 0f) return;

        // ��� ������������� ������
        if (!_mainCamera.orthographic)
        {
            _mainCamera.fieldOfView -= scroll * _zoomSpeed * 10f;
            _mainCamera.fieldOfView = Mathf.Clamp(
                _mainCamera.fieldOfView,
                _minZoom,
                _maxZoom
            );
        }
        // ��� ��������������� ������
        else
        {
            _mainCamera.orthographicSize -= scroll * _zoomSpeed;
            _mainCamera.orthographicSize = Mathf.Clamp(
                _mainCamera.orthographicSize,
                _minZoom,
                _maxZoom
            );
        }
    }
}