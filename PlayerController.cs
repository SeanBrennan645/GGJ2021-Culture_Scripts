using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//test comment
public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform player = null;
    [SerializeField] Transform playerCamera = null;
    [SerializeField] Transform holdingPosition = null;
    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] float playerSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float interactRange = 10.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;
    [SerializeField] GameObject cubeEventObject;

    public Rigidbody heldItem;

    private float cameraPitch = 0.0f;
    private float velocityY = 0.0f;
    private CharacterController controller = null;
    private bool holdingItem = false;

    private AudioSource footstep;

    //Movement Vectors
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;

    //Mouse Vectors
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    //Puzzle Cube Script
    CubePuzzle cp;

    // Start is called before the first frame update
    void Start()
    {
        cp = cubeEventObject.GetComponent<CubePuzzle>();
        controller = GetComponent<CharacterController>();
        footstep = GetComponent<AudioSource>();
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        
        if (Input.GetMouseButtonDown(0))
        {
            LeftClickPressed();
        }
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        


        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity; //using negative as mouse pos value and camera pitch are inverted to each other
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * (currentMouseDelta.x * mouseSensitivity));
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        if (targetDir != new Vector2(0, 0))
        {
            if(!footstep.isPlaying)
                footstep.Play();
        }
        else
            footstep.Stop();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if(controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * playerSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    void LeftClickPressed()
    {
        if (holdingItem)
        {
            DropItem();
            return;
        }

        RaycastHit hit;
        Ray ray = playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(ray, out hit, interactRange) && hit.transform.tag == "Holdable")
        {
            PickupItem(hit);
        }
        if (Physics.Raycast(ray, out hit, interactRange) && hit.transform.tag == "Moveable")
        {
            MoveObject(hit);
        }
        //Allowing raycast to trigger buttons in CP script
        cp.raycast(hit);
    }

    void PickupItem(RaycastHit hit)
    {
        hit.transform.SetParent(player);
        hit.transform.position = holdingPosition.position;
        holdingItem = true;

        heldItem = hit.transform.GetComponent<Rigidbody>();
        heldItem.isKinematic = true;
        //heldItem.detectCollisions = false;
        heldItem.useGravity = false;
        heldItem.constraints = RigidbodyConstraints.FreezeAll;
    }

    void DropItem()
    {
        heldItem.isKinematic = false;
        heldItem.detectCollisions = true;
        heldItem.useGravity = true;
        heldItem.constraints = RigidbodyConstraints.None;
        heldItem.transform.parent = null;
        heldItem = null;
        holdingItem = false;
    }

    void MoveObject(RaycastHit hit)
    {

        ObjectSlider drawer = hit.transform.GetComponent<ObjectSlider>();
        if(drawer.Moving)
        {
            return;
        }
        else //prevents player from opening or closing a moving drawer
        {
            drawer.Moving = true;
        }
    }
}
