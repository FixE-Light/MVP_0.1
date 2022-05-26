using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public InputField inputField;
    public Text storeText;
    public GameObject ObjectPrefab;
    public GameObject createUI;
    public GameObject TextWindows;
    public Transform spawnPoint;
    public float speed;
    public float inputHorizontal, inputVertical;

    public VariableJoystick variableJoystick;
    PhotonView view;
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        // if (view.IsMine)
        // {
        //     Vector2 moveInput = new Vector2(JoystickMovement.instance.joystick.Horizontal, JoystickMovement.instance.joystick.Vertical);
        //     Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
        //     transform.position += (Vector3)moveAmount;
        //     // if (Input.touchCount > 0)
        //     // {
        //     //     Touch touch = Input.GetTouch(0);
        //     //     Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //     //     touchPosition.z = 0f;
        //     //     transform.position = touchPosition;
        //     // }

        //     // for (int i = 0; i < Input.touchCount; i++)
        //     // {
        //     //     Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
        //     //     Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
        //     // }
        // }
        if (!createUI.activeSelf && !TextWindows.activeSelf)
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");
            MovePlayer(inputHorizontal, inputVertical);
            JoyStick_Movement();
        }
    }
    void MovePlayer(float horizontal, float vertical)
    {
        if (horizontal > 0 || horizontal < 0)
        {
            transform.Translate(transform.right * horizontal * speed * Time.deltaTime);
        }
        else if (vertical > 0 || vertical < 0)
        {
            transform.Translate(transform.up * vertical * speed * Time.deltaTime);
        }
    }

    void JoyStick_Movement()
    {
        Vector3 direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
        transform.Translate(direction * speed * Time.deltaTime);
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    public void SpawnObject()
    {
        PhotonNetwork.Instantiate(ObjectPrefab.name, spawnPoint.position, Quaternion.identity);
        if (inputField.text != null)
        {
            ObjectPrefab.GetComponent<InstanceManager>().StoreString = inputField.text;
        }
        createUI.SetActive(false);
    }
    public void Enbale_CreateUI()
    {
        createUI.SetActive(true);
    }
    public void Disable_TextWindow()
    {
        TextWindows.SetActive(false);
    }
}
