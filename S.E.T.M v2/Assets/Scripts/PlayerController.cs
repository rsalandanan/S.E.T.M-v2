using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private Transform playerCamera;
  [SerializeField] private float mouseSens;
  [SerializeField] [Range(0.0f, 0.05f)] private float cameraSmooth = 0.3f;
  [SerializeField] private bool lockCursor = true;
  Vector2 _currentMouseDelta = Vector2.zero;
  private Vector2 _currentMouseDeltaVelocity = Vector2.zero;
  private CharacterController _controller;
  private float _cameraPitch;

  private void Start()
  {
    _controller = GetComponent<CharacterController>();
    if (lockCursor)
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
    
  }

  private void Update()
  {
    MouseLook();
  }

  void MouseLook()
  {
    Vector2 targetMouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity,cameraSmooth);
    _cameraPitch -= _currentMouseDelta.y * mouseSens;
    _cameraPitch = Mathf.Clamp(_cameraPitch, -75.0f, 75.0f);
    playerCamera.localEulerAngles = Vector3.right * _cameraPitch;
    transform.Rotate(Vector3.up *(_currentMouseDelta.x * mouseSens));
  }
}
