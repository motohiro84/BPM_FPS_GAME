using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
  public GameObject Camera;
  private Transform PlayerTransform;
  private Transform CameraTransform;
  private float ii;
  public float sensitivity;

  void Start()
  {
    PlayerTransform = GameObject.Find("Camera").transform.parent;
    CameraTransform = GameObject.Find("Camera").GetComponent<Transform>();
    CameraTransform.transform.Rotate(360f, 0, 0);
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update()
  {
    CameraMove();
  }
  void CameraMove()
  {
    float X_Rotation = Input.GetAxis("Mouse X") * sensitivity;
    float Y_Rotation = Input.GetAxis("Mouse Y") * sensitivity;
    PlayerTransform.transform.Rotate(0, X_Rotation, 0);

    ii = (Camera.transform.localEulerAngles.x - Y_Rotation) * Mathf.Deg2Rad;
    ii = Mathf.Sin(ii);

    if (ii > -0.6f && ii < 0.6f)
    {
      CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
    }
  }
}
