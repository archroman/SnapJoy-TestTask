using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _ProjectFiles.UI.Scripts
{
    internal sealed class InteractionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Show(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                Hide();
                return;
            }

            _text.text = $"E - {text}";
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _text.text = "";
        }
    }
}