using _ProjectFiles.Interaction.Interfaces;
using _ProjectFiles.Interaction.Scripts;
using _ProjectFiles.Player.Scripts;
using UnityEngine;

namespace _ProjectFiles.Items.Scripts
{
    internal sealed class ItemSocket : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _point;

        private Item _currentItem;

        public string GetPrompt()
        {
            return _currentItem == null ? "положить" : "поднять";
        }

        public void OnInteractStart(PlayerInteractor player)
        {
            if (_currentItem != null)
            {
                TakeItem(player);
                return;
            }

            var item = player.GetHeldItem();
            if (item == null) return;

            PlaceItem(item, player);
        }

        public void OnInteractHold(PlayerInteractor player) { }
        public void OnInteractEnd(PlayerInteractor player) { }

        public bool IsHoldable() => false;

        private void PlaceItem(Item item, PlayerInteractor player)
        {
            _currentItem = item;

            item.transform.SetParent(_point);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            item.GetComponent<Collider>().enabled = true;

            player.ClearHeldItem();
        }

        private void TakeItem(PlayerInteractor player)
        {
            if (_currentItem == null) return;

            var item = _currentItem;
            _currentItem = null;

            item.transform.SetParent(null);

            item.GetComponent<Collider>().enabled = false;

            player.PickupItem(item);
        }
    }
}