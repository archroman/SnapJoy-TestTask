using _ProjectFiles.Interaction.Interfaces;
using _ProjectFiles.Interaction.Scripts;
using _ProjectFiles.Items.Scripts;
using _ProjectFiles.UI.Scripts;
using UnityEngine;

namespace _ProjectFiles.Player.Scripts
{
    public sealed class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private float _distance = 3f;
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private InteractionUI _interactionUI;
        [SerializeField] private ItemDescriptionUI _descriptionUI;

        private PlayerState _state = PlayerState.Free;

        private Item _heldItem;
        private Item _inspectingItem;
        private IInteractable _current;

        public bool IsLocked => _state != PlayerState.Free;
        public bool IsInspectingMode => _state == PlayerState.Inspect;

        private void Update()
        {
            Detect();
            HandleInput();

            if (_current != null && _current.IsHoldable())
            {
                if (Input.GetKey(KeyCode.E))
                    _current.OnInteractHold(this);

                if (Input.GetKeyUp(KeyCode.E))
                    _current.OnInteractEnd(this);
            }
            
            if (_state == PlayerState.Inspect && _inspectingItem != null)
            {
                if (Input.GetMouseButton(0))
                {
                    float x = Input.GetAxis("Mouse X");
                    float y = Input.GetAxis("Mouse Y");

                    _inspectingItem.transform.Rotate(Vector3.up, -x * 5f, Space.World);
                    _inspectingItem.transform.Rotate(Vector3.right, y * 5f, Space.World);
                }
            }
        }

        private void Detect()
        {
            if (IsLocked)
            {
                _interactionUI.Hide();
                _current = null;
                return;
            }

            Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, _distance))
                _current = hit.collider.GetComponent<IInteractable>();
            else
                _current = null;

            if (_current != null)
                _interactionUI.Show(_current.GetPrompt());
            else
                _interactionUI.Hide();
        }

        private void HandleInput()
        {
            if (_state == PlayerState.Inspect)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    PickupItem(_inspectingItem);

                return;
            }

            if (IsLocked) return;
            if (_current == null) return;

            if (Input.GetKeyDown(KeyCode.E))
                _current.OnInteractStart(this);
        }

        public void StartInspect(Item item)
        {
            if (_heldItem != null) return;

            _state = PlayerState.Inspect;
            _inspectingItem = item;

            item.GetComponent<Collider>().enabled = false;

            Vector3 offset =
                _cam.transform.forward * 1.5f +
                _cam.transform.right * -0.5f +
                _cam.transform.up * -0.2f;

            item.transform.position = _cam.transform.position + offset;
            item.transform.SetParent(_cam.transform);

            _descriptionUI?.Show(item.GetDescription());
        }

        public void PickupItem(Item item)
        {
            _state = PlayerState.Free;

            _inspectingItem = null;
            _heldItem = item;

            item.transform.SetParent(_holdPoint);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            item.GetComponent<Collider>().enabled = false;

            _descriptionUI?.Hide();
        }

        public void SetDialogue(bool active)
        {
            _state = active ? PlayerState.Dialogue : PlayerState.Free;
        }

        private void LateUpdate()
        {
            Cursor.lockState = _state == PlayerState.Free
                ? CursorLockMode.Locked
                : CursorLockMode.None;

            Cursor.visible = _state != PlayerState.Free;
        }

        public Item GetHeldItem() => _heldItem;
        public void ClearHeldItem() => _heldItem = null;

        public bool IsInspecting(Item item)
            => _state == PlayerState.Inspect && _inspectingItem == item;
    }
}