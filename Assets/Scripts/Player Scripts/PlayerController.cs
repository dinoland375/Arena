using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveInputDeadZone;
    [SerializeField] private Joystick joy;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform player;
    [SerializeField] private Slider sliderFloat;
    [SerializeField] private float radius = 35f;
    [SerializeField] private float safeDistance = 2f;
    [SerializeField] private float planeSize = 40f;

    private Vector3 moveVector;
    private Vector2 lookInput;
    private int rightfingerId;
    private float halfScreenWidth;
    private float cameraPitch;
    private Vector3 center;
    private List<GameObject> objects;

    private void Start()
    {
        center = new Vector3(0f, 1f, 0f);
        objects = new List<GameObject>();

        rightfingerId = -1;

        halfScreenWidth = Screen.width / 2;

        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }

    private void Update()
    {
        MovePlayer();
        SetCameraSensitivity();
        GetTouchinput();

        Vector3 playerPos = transform.position;
        float distance = Vector3.Distance(center, playerPos);

        if (distance > radius)
        {
            Vector3 safeSpot = FindSafeSpot();

            if (safeSpot != Vector3.zero)
            {
                transform.position = safeSpot;
                transform.LookAt(center);

            }
        }
    }

    private void FixedUpdate()
    {
        if (rightfingerId != -1)
        {
            LookAroud();
        }
    }

    private Vector3 FindSafeSpot()
    {
        int attempts = 60;
        while (attempts-- > 0)
        {
            Vector3 randomPos = new Vector3(Random.Range(-planeSize, planeSize), 3f, Random.Range(-planeSize, planeSize));

            bool isSafe = true;
            foreach (GameObject obj in objects)
            {
                float distance = Vector3.Distance(randomPos, obj.transform.position);
                if (distance < safeDistance)
                {
                    isSafe = false;
                    break;
                }
            }

            if (isSafe)
            {
                return randomPos;
            }
        }

        return Vector3.zero;
    }

    public void AddObject(GameObject obj)
    {
        objects.Add(obj);
    }

    public void RemoveObject(GameObject obj)
    {
        objects.Remove(obj);
    }

    public void SetCameraSensitivity()
    {
        cameraSensitivity = sliderFloat.value;
    }

    private void GetTouchinput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (touch.position.x > halfScreenWidth && rightfingerId == -1)
                    {
                        rightfingerId = touch.fingerId;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touch.fingerId == rightfingerId)
                    {
                        rightfingerId = -1;
                    }
                    break;
                case TouchPhase.Moved:
                    if (touch.fingerId == rightfingerId)
                    {
                        lookInput = touch.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    break;
                case TouchPhase.Stationary:
                    if (touch.fingerId == rightfingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    private void LookAroud()
    {
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 20f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        transform.Rotate(transform.up, lookInput.x);
    }
 
    private void MovePlayer()
    {
        moveVector = Vector3.zero;
        moveVector.x = joy.Horizontal;
        moveVector.z = joy.Vertical;

        moveVector = transform.right * moveVector.x + cameraTransform.forward * moveVector.z + transform.up * moveVector.y;
        transform.Translate(moveVector * speedMove * Time.fixedDeltaTime, Space.World);
    }
}