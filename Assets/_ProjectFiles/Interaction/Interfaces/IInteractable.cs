using _ProjectFiles.Player.Scripts;

namespace _ProjectFiles.Interaction.Interfaces
{
    public interface IInteractable
    {
        string GetPrompt();

        void OnInteractStart(PlayerInteractor player);
        void OnInteractHold(PlayerInteractor player);
        void OnInteractEnd(PlayerInteractor player);

        bool IsHoldable();
    }
}