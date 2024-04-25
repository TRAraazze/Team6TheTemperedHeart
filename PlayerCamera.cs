using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DevionGames;

namespace CGP
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public PlayerManager player;
        public Camera cameraObject;

        public Toggle invertXToggle;
        public Toggle invertYToggle;
        [SerializeField] Transform cameraPivotTransform;
        PlayerInputManager inputManager;

        // CHANGE THESE TO TWEAK CAMERA PERFORMANCE
        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 5; // THE BIGGER THIS NUMBER, THE LONGER FOR THE CAMERA TO REACH ITS POSITION DURING MOVEMENT
        [SerializeField] float upAndDownRotationSpeed = 220;
        [SerializeField] float leftAndRightRotationSpeed = 220;
        [SerializeField] float minimumPivot = -30; // THE LOWEST POINT YOU ARE ABLE TO LOOK DOWN
        [SerializeField] float maximumPivot = 60; // THE HIGHEST POINT YOU ARE ABLE TO LOOK UP
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask collideWithLayers;

        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition; // USED FOR CAMERA COLLISIONS (MOVES THE CAMERA OBJECT TO THIS POSITION UPON COLLIDING)
        [SerializeField] float leftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;
        private float cameraZPosition; // VALUES USED FOR CAMERA COLLISIONS FORWARDS AND BACKWARDS
        private float targetCameraZPosition; // VALUES USED FOR CAMERA COLLISIONS

        [Header("Dropdown Settings")]
        public Dropdown sensitivityDropdown; // Reference to the dropdown UI element
        private float[] sensitivityValues = { 0.1f, 0.5f, 0.75f, 1f, 1.5f, 2f, 3f, 5f }; // Array of sensitivity values
        private float currentSensitivity = 1f; // Default sensitivity value

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(instance);

            inputManager = GameObject.Find("PlayerInputManager").GetComponent<PlayerInputManager>();
            invertXToggle = GameObject.Find("HUD").FindChild("ToggleX", true).GetComponent<Toggle>();
            invertYToggle = GameObject.Find("HUD").FindChild("ToggleY", true).GetComponent<Toggle>();
            sensitivityDropdown = GameObject.Find("HUD").FindChild("Dropdown (Legacy)", true).GetComponent<Dropdown>();
        }

        private void Start()
        {
            //DontDestroyOnLoad(gameObject  );
            cameraZPosition = cameraObject.transform.localPosition.z;

            if (sensitivityDropdown != null)
            {
                /*sensitivityDropdown.ClearOptions();
                sensitivityDropdown.AddOptions(new List<string>(new string[] { "25%", "50%", "75%", "100%", "125%", "150%", "175%", "200%" }));
                int defaultIndex = System.Array.IndexOf(sensitivityValues, 1f); // Find the index of 1.0f (100%) in the sensitivityValues array
                if (defaultIndex != -1)
                {
                    sensitivityDropdown.value = defaultIndex; // Set the dropdown value to the index of 100%
                }*/
                //sensitivityDropdown.onValueChanged.AddListener(delegate { OnSensitivityChanged(sensitivityDropdown); });
            }
        }

        /*private void OnSensitivityChanged(Dropdown dropdown)
        {
            Debug.Log("Sensitivity changed");
            currentSensitivity = sensitivityValues[dropdown.value];
        }*/

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                HandleFollowPlayer();
                HandleRotations();
                HandleCollisions();
            }
        }

        private void HandleFollowPlayer()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.localPosition, player.transform.localPosition, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.localPosition = targetCameraPosition;
        }

        private void HandleRotations()
        {
            // if right mouse button is not held down, don't rotate the camera (unlock the cursor)
            //if (Input.GetMouseButton(1))
            //{
            if (!inputManager.isMouseFree)
            {
                currentSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);

                float horizontalInput = PlayerInputManager.instance.cameraHorizontalInput;
                float verticalInput = PlayerInputManager.instance.cameraVerticalInput;

                // Invert X and Y axis if the corresponding toggles are active
                if (invertXToggle.isOn)
                {
                    horizontalInput *= -1f;
                }
                if (invertYToggle.isOn)
                {
                    verticalInput *= -1f;
                }
                // IF LOCKED ON, FORCE ROTATION TOWARDS TARGET
                // ELSE ROTATE REGULARLY

                // NORMAL ROTATIONS
                // ROTATE LEFT AND RIGHT BASED ON THE HORIZONTAL MOVEMENT ON THE MOUSE
                leftAndRightLookAngle += (horizontalInput * (leftAndRightRotationSpeed * currentSensitivity)) * Time.deltaTime;
                //Debug.Log(leftAndRightLookAngle);
                // ROTATE UP AND DOWN BASED ON THE VERTICAL MOVEMENT ON THE MOUSE
                upAndDownLookAngle -= (verticalInput * (upAndDownRotationSpeed * currentSensitivity)) * Time.deltaTime;
                // CLAMP THE UP AND DOWN LOOK ANGLE BETWEEN A MIN AND MAX VALUE
                upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

                Vector3 cameraRotation = Vector3.zero;
                Quaternion targetRotation;

                // ROTATE THIS GAMEOBJECT LEFT AND RIGHT
                cameraRotation.y = leftAndRightLookAngle; // IN TERMS OF ROTATION Y IS LEFT AND RIGHT
                targetRotation = Quaternion.Euler(cameraRotation);
                transform.rotation = targetRotation;

                // ROTATE THIS GAMEOBJECT UP AND DOWN
                cameraRotation = Vector3.zero;
                cameraRotation.x = upAndDownLookAngle;
                targetRotation = Quaternion.Euler(cameraRotation);
                cameraPivotTransform.localRotation = targetRotation;
                //}
            }
        }

        private void HandleCollisions()
        {
            targetCameraZPosition = cameraZPosition;

            RaycastHit hit;
            // DIRECTION FOR COLLISION CHECK
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            // WE CHECK IF THERE IS AN OBJECT IN FRONT OF OUR DESIRED DIRECTION ^ (SEE ABOVE)
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                // IF THERE IS, WE GET OUR DISTANCE FROM IT
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                // WE THEN EQUATE OUR TARGET Z POSITION TO THE FOLLOWING
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            // IF OUR TARGET POSITION IS LESS THAN OUR COLLISION RADIUS, WE SUBTRACT OUR COLLISION RADIUS (MAKING IT SNAP BACK)
            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            // WE THEN APPLY OUR FINAL POSITION USING A LERP OVER A TIME OF 0.2F
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    }
}