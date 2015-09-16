using UnityEngine;
using System;

namespace InteractionModule.Behaviours
{
    public class DebugLogMSG : A_InteractBehaviour
    {
        [SerializeField] private string msg;

		public override void Initialize(A_InteractBehaviour behaviourParameters)
		{
			msg = ((DebugLogMSG)behaviourParameters).msg;
		}

		public override void OnEventTrigger()
        {
            Debug.Log(msg);
        }

		public override void OnEventEnd(){}

		public string DebugMessage
		{
			get{ return msg; }
			set{ msg = value;}
		}
    }
}
