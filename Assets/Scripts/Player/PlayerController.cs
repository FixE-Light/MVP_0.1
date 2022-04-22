using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine)
        {
            Vector2 moveInput = new Vector2(JoystickMovement.instance.joystick.Horizontal, JoystickMovement.instance.joystick.Vertical);
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;
            // if (Input.touchCount > 0)
            // {
            //     Touch touch = Input.GetTouch(0);
            //     Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            //     touchPosition.z = 0f;
            //     transform.position = touchPosition;
            // }

            // for (int i = 0; i < Input.touchCount; i++)
            // {
            //     Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            //     Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
            // }
        }
    }
}
