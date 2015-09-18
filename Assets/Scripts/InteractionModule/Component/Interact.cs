using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component To Handle All Interaction Behaviour
/// </summary>
namespace InteractionModule
{
	public enum EBehaviourClasses
	{
		SelectOne,
		DebugLogMSG,
		DestoryInteraction,
		LerpObjectFromTo,
		PickupItem
	}

    /// <summary>
    /// Component Class For Object Interactions
    /// </summary>
    [System.Serializable]
    public class Interact : MonoBehaviour, I_Interactable
    {
        // Text To Edit To UI
        [SerializeField] private string interactEventText;
        [SerializeField] private Sprite interactEventImage;
        [SerializeField] private KeyCode interactEventKey;

        // UI To Edit With Information
        [SerializeField] private Text textUI;
        [SerializeField] private Image imageUI;
        [SerializeField] private GameObject parentUI;

        // This Way We Can Call Any Interaction
        [SerializeField] private A_InteractBehaviour myBehaviour;

		public EBehaviourClasses behaviourClassInspector;

		private bool canInteract;

        // Constructor For Custom Inspector
        public void InitializeComponent(string text, Sprite sprite, KeyCode key, Text uiText, Image uiImage, GameObject uiParent, A_InteractBehaviour ib)
        {
            InteractEventText = text;
            InteractEventImage = sprite;
            InteractEventKey = key;

            TextUI = uiText;
            ImageUI = uiImage;
            ParentUI = uiParent;

            myBehaviour = ib;
        }

        // Edit and Show UI
        public void ShowInteractableUI()
        {
            TextUI.text = InteractEventText;
            ImageUI.sprite = InteractEventImage;

            ParentUI.SetActive(true);

			canInteract = true;
        }

        // Close UI
        public void HideInteractableUI()
        {
			canInteract = false;
            ParentUI.SetActive(false);
        }

        // Call Specific Event Behaviour
        public void InteractableEvent()
        {
            if (ParentUI.activeInHierarchy && Input.GetKeyDown(InteractEventKey))
            {
				myBehaviour.OnEventTrigger();
            }
        }

		// Initialize All Components
		private void Start()
		{
			A_InteractBehaviour aib = (A_InteractBehaviour)this.gameObject.AddComponent(myBehaviour.GetType()) ;
			aib.Initialize( myBehaviour );
			aib.interactParent = this;

			myBehaviour = aib;
		}

		public void Update()
		{
			if(canInteract)
				InteractableEvent();
		}



        #region Properties

        public string InteractEventText
        {
            get
            {
                return interactEventText;
            }

            set
            {
                interactEventText = value;
            }
        }

        public Sprite InteractEventImage
        {
            get
            {
                return interactEventImage;
            }

            set
            {
                interactEventImage = value;
            }
        }

        public KeyCode InteractEventKey
        {
            get
            {
                return interactEventKey;
            }

            set
            {
                interactEventKey = value;
            }
        }

        public Text TextUI
        {
            get
            {
                return textUI;
            }

            set
            {
                textUI = value;
            }
        }

        public Image ImageUI
        {
            get
            {
                return imageUI;
            }

            set
            {
                imageUI = value;
            }
        }

        public GameObject ParentUI
        {
            get
            {
                return parentUI;
            }

            set
            {
                parentUI = value;
            }
        }

        public A_InteractBehaviour Behaviour
        {
            get
            {
                return myBehaviour;
            }

            set
            {
                myBehaviour = value;
            }
        }

        #endregion
    }
}