    using System.Collections;
    using _ProjectFiles.Interaction.Interfaces;
    using _ProjectFiles.Items.Scripts;
    using _ProjectFiles.Player.Scripts;
    using UnityEngine;

    namespace _ProjectFiles.Interaction.Scripts
    {
        internal sealed class Chest : MonoBehaviour, IInteractable
        {
            [SerializeField] private Transform _lid;
            [SerializeField] private float _openAngle = -90f;
            [SerializeField] private float _speed = 5f;

            private bool _isOpen;

            public string GetPrompt()
            {
                if (_isOpen) return null;
                return "открыть";
            }

            public void OnInteractStart(PlayerInteractor player)
            {
                if (_isOpen) return;

                var item = player.GetHeldItem();

                if (item == null) return;

                if (item.GetItemType() != ItemType.Key) return;

                OpenChest(player, item);
            }

            public void OnInteractHold(PlayerInteractor player) { }
            public void OnInteractEnd(PlayerInteractor player) { }

            public bool IsHoldable() => false;

            private void OpenChest(PlayerInteractor player, Item key)
            {
                _isOpen = true;

                Destroy(key.gameObject);
                player.ClearHeldItem();

                StartCoroutine(OpenAnimation());
            }
            
            private IEnumerator OpenAnimation()
            {
                float current = 0f;

                while (current < _openAngle)
                {
                    current += _speed * Time.deltaTime;
                    _lid.localRotation = Quaternion.Euler(0f, 0f, current);
                    yield return null;
                }

                _lid.localRotation = Quaternion.Euler(0f, 0f, _openAngle);
            }
        }
    }