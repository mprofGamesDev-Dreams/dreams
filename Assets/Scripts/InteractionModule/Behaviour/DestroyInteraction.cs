using UnityEngine;
using System;
using InteractionModule.Behaviours;

namespace InteractionModule.Behaviours
{
    public class DestroyInteraction : A_InteractBehaviour
    {
        [SerializeField] private float timeToLive = 0;

		private float startTime = 0;

        public void Update()
        {
            if (EventTrigger)
            {
				if( (startTime+timeToLive) < Time.time )
				{
					OnEventEnd();
					Destroy(this.gameObject);
				}
			}
        }

		public override void Initialize(A_InteractBehaviour behaviourParameters)
		{
			timeToLive = ((DestroyInteraction)behaviourParameters).TimeToLive;
		}

		public override void OnEventTrigger()
        {
            EventTrigger = true;
			startTime = Time.time;
        }

		public override void OnEventEnd()
		{
			base.interactParent.HideInteractableUI();
		}

    	public float TimeToLive 
		{
			get { return this.timeToLive; }
    		set { timeToLive = value; }
    	}
    }
}
