using UnityEngine;

namespace _ProjectFiles.Player.Scripts
{
    internal sealed class CameraLook : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;
        [SerializeField] private float _sensitivity = 150f;
        [SerializeField] private PlayerInteractor _interactor;

        private float _xRotation;

        private void Update()
        {
            if (_interactor != null && _interactor.IsLocked)
                return;

            float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}