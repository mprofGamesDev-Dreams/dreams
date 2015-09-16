/// <summary>
/// Interface For Creating Interactable Behaviours
/// </summary>
namespace InteractionModule
{
    public interface I_InteractBehaviour
    {
		void Initialize(A_InteractBehaviour behaviour);
		void OnEventTrigger();
		void OnEventEnd();
    }
}