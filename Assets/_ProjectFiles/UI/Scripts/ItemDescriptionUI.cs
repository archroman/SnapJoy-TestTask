using TMPro;
using UnityEngine;

namespace _ProjectFiles.UI.Scripts
{
    internal sealed class ItemDescriptionUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;

        public void Show(string description)
        {
            _panel.SetActive(true);
            _text.text = description;
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }
    }
}