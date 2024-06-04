using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed = 3f;
    private Camera _mainCamera;
    private Vector3 _mouseInput = Vector3.zero;

    private void Initialize()
    {
        _mainCamera = Camera.main;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Initialize();
    }

    private void Update()
    {
        if (!IsOwner || !Application.isFocused) return;
        //Movement
        _mouseInput.x = Input.mousePosition.x;
        _mouseInput.y = Input.mousePosition.y;
        _mouseInput.z = _mainCamera.nearClipPlane;
        Vector3 mouseworldcoordinates = _mainCamera.ScreenToWorldPoint(_mouseInput);
        transform.position = Vector3.MoveTowards(transform.position, mouseworldcoordinates, Time.deltaTime * speed);

        //Rotate
        if (mouseworldcoordinates != transform.position)
        {
            Vector3 targetDirection = mouseworldcoordinates - transform.position;
            targetDirection.z = 0f;
            transform.up = targetDirection;

        }
    }
}
