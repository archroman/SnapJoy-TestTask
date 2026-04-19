using _ProjectFiles.Items.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectFiles.Quests.Scripts
{
    internal sealed class QuestManager : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _check;

        private ItemType _target;
        private bool _active;
        private bool _completed;

        public bool IsCompleted => _completed;
        public bool IsActive => _active;

        public void StartQuest(ItemType target)
        {
            _target = target;
            _active = true;
            _completed = false;

            _panel.SetActive(true);
            _check.enabled = false;

            _text.text = $"Принеси: {_target}";
        }

        public bool TryComplete(Item item)
        {
            if (!_active) return false;

            if (item.GetItemType() == _target)
            {
                Complete();
                return true;
            }

            return false;
        }

        private void Complete()
        {
            _active = false;
            _completed = true;

            _text.text = "Квест выполнен!";
            _check.enabled = true;
        }
    }
}