using UnityEngine;
using System.Collections;

namespace InteractionModule.Behaviours
{
	public class LerpObjectFromTo : A_InteractBehaviour 
	{
		[SerializeField] private Vector3 startingPosition;
		[SerializeField] private Vector3 endingPosition;

		[SerializeField] private float transitionTime = 0;

		private float startTime = 0;
		private float currentTime = 0;

		private Transform myTransform;

		private void Start()
		{
			myTransform = this.GetComponent<Transform>();
		}

		private void Update()
		{
			if(EventTrigger)
			{
				if(startTime == 0)
					startTime = Time.time;

				if( currentTime < (startTime + transitionTime) )
				{
					currentTime += Time.deltaTime;
					myTransform.position = Vector3.Lerp(startingPosition, endingPosition, currentTime);
				}else EventTrigger = false;
			}
		}

		public override void Initialize (A_InteractBehaviour behaviourParameters)
		{
			LerpObjectFromTo obj = (LerpObjectFromTo)behaviourParameters;

			this.startingPosition = obj.StartingPosition;
			this.endingPosition = obj.EndingPosition;

			this.transitionTime = obj.TransitionTime;
 		}

		public override void OnEventTrigger ()
		{
			EventTrigger = true;
		}

		public override void OnEventEnd(){}


		#region Properties
		public Vector3 StartingPosition 
		{
			get { return this.startingPosition; }
			set { startingPosition = value; }
		}

		public Vector3 EndingPosition 
		{
			get { return this.endingPosition; }
			set { endingPosition = value; }
		}

		public float TransitionTime 
		{
			get { return this.transitionTime; }
			set { transitionTime = value; }
		}
		#endregion
	}
}