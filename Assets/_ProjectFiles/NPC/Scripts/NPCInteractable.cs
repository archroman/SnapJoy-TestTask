using System.Collections.Generic;
using _ProjectFiles.Dialogue.Scripts;
using _ProjectFiles.Interaction.Interfaces;
using _ProjectFiles.Items.Scripts;
using _ProjectFiles.Player.Scripts;
using _ProjectFiles.Quests.Scripts;
using UnityEngine;

namespace _ProjectFiles.NPC.Scripts
{
    internal sealed class NPCInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private NPCType _type;

        [Header("Dialogue")]
        [SerializeField] private DialogueSystem _dialogueSystem;
        [SerializeField] private List<DialogueNode> _dialogue;

        [Header("Quest")]
        [SerializeField] private QuestManager _questManager;
        [SerializeField] private List<DialogueNode> _afterQuestDialogue;
        [SerializeField] private List<Item> _sceneItems;

        private bool _questGiven;
        private bool _afterDialoguePlayed;

        public string GetPrompt() => "разговор";

        public void OnInteractStart(PlayerInteractor player)
        {
            if (_type == NPCType.QuestGiver)
            {
                var held = player.GetHeldItem();

                if (held != null && _questManager.TryComplete(held))
                {
                    Destroy(held.gameObject);
                    player.ClearHeldItem();

                    if (!_afterDialoguePlayed)
                    {
                        _dialogueSystem.StartDialogue(_afterQuestDialogue, player);
                        _afterDialoguePlayed = true;
                        player.SetDialogue(true);
                    }

                    return;
                }

                if (_questManager.IsCompleted)
                {
                    return;
                }

                if (!_questGiven)
                {
                    GiveQuest();
                    _questGiven = true;
                }
            }

            _dialogueSystem.StartDialogue(_dialogue, player);
            player.SetDialogue(true);
        }

        private void GiveQuest()
        {
            if (_sceneItems == null || _sceneItems.Count == 0)
                return;

            foreach (var item in _sceneItems)
            {
                if (item == null) continue;

                var type = item.GetItemType();

                if (type == ItemType.Key || type == ItemType.Note)
                    continue;

                _questManager.StartQuest(type);
                return;
            }
        }

        public void OnInteractHold(PlayerInteractor player) { }
        public void OnInteractEnd(PlayerInteractor player) { }
        public bool IsHoldable() => false;
    }
}