using UnityEngine;

namespace _ProjectFiles.Player.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    internal sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private PlayerInteractor _interactor;

        private CharacterController _controller;
        private Vector3 _velocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_interactor != null && _interactor.IsLocked)
                return;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            _controller.Move(move * (_speed * Time.deltaTime));

            if (_controller.isGrounded && _velocity.y < 0)
                _velocity.y = -2f;

            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}