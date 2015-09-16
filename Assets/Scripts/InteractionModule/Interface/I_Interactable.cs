/// <summary>
/// Simple Interface For Interactable Objects
/// </summary>
namespace InteractionModule
{
    public interface I_Interactable
    {
        // Show Hide Interactable UI
        void ShowInteractableUI();
        void HideInteractableUI();

        // OnInteraction Play Out And Event
        void InteractableEvent();
    }
}