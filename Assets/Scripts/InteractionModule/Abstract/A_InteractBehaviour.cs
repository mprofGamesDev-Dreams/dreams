using System;
using UnityEngine;

namespace InteractionModule
{
    [System.Serializable]
    public abstract class A_InteractBehaviour : MonoBehaviour, I_InteractBehaviour
    {
		public bool EventTrigger = false;
		public Interact interactParent;

		// Essentially A Copy Constructor
		public abstract void Initialize(A_InteractBehaviour behaviourParameters);

		// Triggers Event
		public abstract void OnEventTrigger();

		// Call This On End
		public abstract void OnEventEnd();
    }
}