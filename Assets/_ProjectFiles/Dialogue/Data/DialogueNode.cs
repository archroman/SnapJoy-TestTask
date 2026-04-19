using System.Collections.Generic;
using _ProjectFiles.Dialogue.Data;

[System.Serializable]
public class DialogueNode
{
    public string Text;
    public List<DialogueChoice> Choices;
    public bool IsEnd;
}