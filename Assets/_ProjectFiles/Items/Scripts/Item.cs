using System;
using _ProjectFiles.Interaction.Interfaces;
using _ProjectFiles.Interaction.Scripts;
using _ProjectFiles.Player.Scripts;
using UnityEngine;

namespace _ProjectFiles.Items.Scripts
{
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _itemName;
        [SerializeField] [TextArea] private string _description;
        [SerializeField] private ItemType _type;

        private Vector3 _initialScale;

        public string GetPrompt() => "поднять";

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        public virtual void OnInteractStart(PlayerInteractor player)
        {
            if (player.IsInspecting(this))
                player.PickupItem(this);
            else
                player.StartInspect(this);
        }

        public virtual void OnInteractHold(PlayerInteractor player)
        {
        }

        public virtual void OnInteractEnd(PlayerInteractor player)
        {
        }

        public bool IsHoldable() => false;

        public ItemType GetItemType() => _type;

        public string GetDescription() => _description;

        public Vector3 GetInitialScale() => _initialScale;
    }
}