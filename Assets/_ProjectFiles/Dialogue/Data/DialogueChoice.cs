namespace _ProjectFiles.Dialogue.Data
{
    [System.Serializable]
    public class DialogueChoice
    {
        public string Text;
        public int NextNodeIndex;
        public bool IsEnd;
    }
}