using _ProjectFiles.Interaction.Interfaces;
using UnityEngine;
using _ProjectFiles.Player.Scripts;

namespace _ProjectFiles.Interaction.Scripts
{
    internal sealed class Vent : MonoBehaviour, IInteractable
    {
        [Header("Vent")]
        [SerializeField] private Transform _handle;
        [SerializeField] private float _rotateSpeed = 180f;
        [SerializeField] private float _maxAngle = 360f;

        [Header("Door")]
        [SerializeField] private Transform _door;
        [SerializeField] private float _doorOpenAngle = 90f;

        private float _currentAngle;

        public string GetPrompt() => "крутить";

        public void OnInteractStart(PlayerInteractor player) { }

        public void OnInteractHold(PlayerInteractor player)
        {
            _currentAngle += _rotateSpeed * Time.deltaTime;
            _currentAngle = Mathf.Clamp(_currentAngle, 0f, _maxAngle);

            ApplyRotation();
        }

        public void OnInteractEnd(PlayerInteractor player) { }

        public bool IsHoldable() => true;

        private void Update()
        {
            if (!Input.GetKey(KeyCode.E))
            {
                ReturnBack();
            }
        }

        private void ReturnBack()
        {
            if (_currentAngle <= 0f) return;

            _currentAngle -= _rotateSpeed * Time.deltaTime;
            _currentAngle = Mathf.Clamp(_currentAngle, 0f, _maxAngle);

            ApplyRotation();
        }

        private void ApplyRotation()
        {
            _handle.localRotation = Quaternion.Euler(_currentAngle, 0f, 0f);
            
            float progress = _currentAngle / _maxAngle;
            float angle = progress * _doorOpenAngle;

            _door.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}