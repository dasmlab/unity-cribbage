using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private Camera mainCamera;
    private bool isDragging = false;
    private GameObject draggedObject;
    private Vector3 dragOffset;

    private void Awake()
    {
        mainCamera = Camera.main;
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += OnClick;
        inputActions.Player.Click.canceled += OnClickEnd;
        inputActions.Player.Drag.performed += OnDrag;
        inputActions.Player.Drag.canceled += OnDragEnd;
        inputActions.Player.Cancel.performed += OnCancel;
    }

    private void OnDisable()
    {
        inputActions.Player.Click.performed -= OnClick;
        inputActions.Player.Click.canceled -= OnClickEnd;
        inputActions.Player.Drag.performed -= OnDrag;
        inputActions.Player.Drag.canceled -= OnDragEnd;
        inputActions.Player.Cancel.performed -= OnCancel;
        inputActions.Player.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        HandleClick(mousePosition);
    }

    private void OnClickEnd(InputAction.CallbackContext context)
    {
        if (isDragging)
        {
            EndDrag();
        }
    }

    private void OnDrag(InputAction.CallbackContext context)
    {
        if (isDragging && draggedObject != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
            draggedObject.transform.position = worldPosition + dragOffset;
        }
    }

    private void OnDragEnd(InputAction.CallbackContext context)
    {
        EndDrag();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        EndDrag();
    }

    private void HandleClick(Vector2 screenPosition)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;
            var cardDisplay = clickedObject.GetComponent<CribbageGame.UI.CardDisplay>();
            if (cardDisplay != null)
            {
                HandleCardClick(cardDisplay, screenPosition);
            }
        }
    }

    private void HandleCardClick(CribbageGame.UI.CardDisplay cardDisplay, Vector2 screenPosition)
    {
        if (!isDragging)
        {
            StartDrag(cardDisplay.gameObject, screenPosition);
        }
        else
        {
            EndDrag();
        }
    }

    private void StartDrag(GameObject obj, Vector2 screenPosition)
    {
        isDragging = true;
        draggedObject = obj;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        dragOffset = obj.transform.position - worldPosition;
        obj.transform.SetAsLastSibling();
    }

    private void EndDrag()
    {
        if (isDragging && draggedObject != null)
        {
            CheckDropZone();
            isDragging = false;
            draggedObject = null;
        }
    }

    private void CheckDropZone()
    {
        Debug.Log($"Card dropped at: {draggedObject.transform.position}");
    }
} 