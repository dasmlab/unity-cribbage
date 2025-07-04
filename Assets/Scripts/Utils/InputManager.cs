using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace CribbageGame.Utils
{
    public class InputManager : MonoBehaviour
    {
        [Header("Input Actions")]
        [SerializeField] private InputActionAsset inputActions;
        
        private InputAction clickAction;
        private InputAction dragAction;
        private InputAction cancelAction;
        
        private Camera mainCamera;
        private bool isDragging = false;
        private GameObject draggedObject;
        private Vector3 dragOffset;
        
        private void Awake()
        {
            mainCamera = Camera.main;
            SetupInputActions();
        }
        
        private void SetupInputActions()
        {
            if (inputActions == null)
            {
                Debug.LogWarning("Input Action Asset not assigned to InputManager!");
                return;
            }
            
            // Get actions from the asset
            clickAction = inputActions.FindAction("Click");
            dragAction = inputActions.FindAction("Drag");
            cancelAction = inputActions.FindAction("Cancel");
            
            // Subscribe to events
            if (clickAction != null)
            {
                clickAction.performed += OnClick;
                clickAction.canceled += OnClickEnd;
            }
            
            if (dragAction != null)
            {
                dragAction.performed += OnDrag;
                dragAction.canceled += OnDragEnd;
            }
            
            if (cancelAction != null)
            {
                cancelAction.performed += OnCancel;
            }
        }
        
        private void OnEnable()
        {
            clickAction?.Enable();
            dragAction?.Enable();
            cancelAction?.Enable();
        }
        
        private void OnDisable()
        {
            clickAction?.Disable();
            dragAction?.Disable();
            cancelAction?.Disable();
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
            // Check if clicking on UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;
                
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;
                
                // Check if it's a card
                CardDisplay cardDisplay = clickedObject.GetComponent<CardDisplay>();
                if (cardDisplay != null)
                {
                    HandleCardClick(cardDisplay, screenPosition);
                }
            }
        }
        
        private void HandleCardClick(CardDisplay cardDisplay, Vector2 screenPosition)
        {
            if (!isDragging)
            {
                // Start dragging
                StartDrag(cardDisplay.gameObject, screenPosition);
            }
            else
            {
                // Drop card or end drag
                EndDrag();
            }
        }
        
        private void StartDrag(GameObject obj, Vector2 screenPosition)
        {
            isDragging = true;
            draggedObject = obj;
            
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
            dragOffset = obj.transform.position - worldPosition;
            
            // Bring to front
            obj.transform.SetAsLastSibling();
        }
        
        private void EndDrag()
        {
            if (isDragging && draggedObject != null)
            {
                // Check for valid drop zones
                CheckDropZone();
                
                // Reset drag state
                isDragging = false;
                draggedObject = null;
            }
        }
        
        private void CheckDropZone()
        {
            // TODO: Implement drop zone logic
            // This would check if the card is dropped in a valid area
            // like the crib, play area, etc.
            
            Debug.Log($"Card dropped at: {draggedObject.transform.position}");
        }
    }
} 