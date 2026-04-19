using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _ProjectFiles.Player.Scripts;

namespace _ProjectFiles.Dialogue.Scripts
{
    internal sealed class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Transform _choicesContainer;
        [SerializeField] private GameObject _choicePrefab;

        private List<DialogueNode> _nodes;
        private int _index;
        private PlayerInteractor _player;
        private bool _isActive;

        private void Update()
        {
            if (!_isActive) return;

            if (Input.GetKeyDown(KeyCode.Escape))
                EndDialogue();
        }

        public void StartDialogue(List<DialogueNode> nodes, PlayerInteractor player)
        {
            _nodes = nodes;
            _index = 0;
            _player = player;

            _panel.SetActive(true);
            _isActive = true;

            player.SetDialogue(true);

            Show();
        }

        private void Show()
        {
            foreach (Transform t in _choicesContainer)
                Destroy(t.gameObject);

            var node = _nodes[_index];
            _text.text = node.Text;

            if (node.Choices == null) return;

            foreach (var c in node.Choices)
            {
                var choice = c;

                var b = Instantiate(_choicePrefab, _choicesContainer);
                b.GetComponentInChildren<TextMeshProUGUI>().text = choice.Text;

                b.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (choice.IsEnd)
                    {
                        EndDialogue();
                        return;
                    }

                    _index = choice.NextNodeIndex;
                    Show();
                });
            }
        }

        private void EndDialogue()
        {
            _panel.SetActive(false);
            _isActive = false;

            _player?.SetDialogue(false);

            _nodes = null;
            _index = 0;
            _player = null;
        }
    }
}