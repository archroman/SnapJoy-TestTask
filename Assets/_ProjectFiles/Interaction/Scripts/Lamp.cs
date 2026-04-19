using _ProjectFiles.Interaction.Interfaces;
using _ProjectFiles.Player.Scripts;
using UnityEngine;

namespace _ProjectFiles.Interaction.Scripts
{
    internal sealed class Lamp : MonoBehaviour, IInteractable
    {
        [SerializeField] private Light _light;
        [SerializeField] private MeshRenderer _meshRenderer;

        [Header("Emission")]
        [SerializeField] private Color _onColor = Color.yellow;
        [SerializeField] private Color _offColor = Color.black;

        private bool _isOn;

        public string GetPrompt() => _isOn ? "выключить" : "включить";

        private void Awake()
        {
            _light.enabled = false;
        }

        public void OnInteractStart(PlayerInteractor player)
        {
            Toggle();
        }

        public void OnInteractHold(PlayerInteractor player) { }
        public void OnInteractEnd(PlayerInteractor player) { }

        public bool IsHoldable() => false;

        private void Toggle()
        {
            _isOn = !_isOn;

            if (_light != null)
                _light.enabled = _isOn;

            if (_meshRenderer != null)
            {
                _meshRenderer.material.EnableKeyword("_EMISSION");
                _meshRenderer.material.SetColor("_EmissionColor", _isOn ? _onColor : _offColor);
            }
        }
    }
}