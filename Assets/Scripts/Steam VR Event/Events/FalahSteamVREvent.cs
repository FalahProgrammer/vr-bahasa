using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

namespace Falah
{
    [Serializable]
    public class FalahSteamVREvent
    {
        [Serializable]
        public sealed class ControllerInteractionEvent : UnityEvent<object>
        {
        }

        public ControllerInteractionEvent OnButtonOneClicked = new ControllerInteractionEvent();

        public ControllerInteractionEvent OnButtonOnePressed = new ControllerInteractionEvent();

        /*public ControllerInteractionEvent OnButtonOneReleased = new ControllerInteractionEvent();
    
        public ControllerInteractionEvent OnButtonOneTouched = new ControllerInteractionEvent();
    
        public ControllerInteractionEvent OnButtonOneTouchReleased = new ControllerInteractionEvent();
    
        public ControllerInteractionEvent OnButtonOneHoldTouched = new ControllerInteractionEvent();*/


        public ControllerInteractionEvent OnButtonTwoClicked = new ControllerInteractionEvent();

        public ControllerInteractionEvent OnButtonTwoPressed = new ControllerInteractionEvent();


        public ControllerInteractionEvent OnButtonPrimaryIndexTriggerClicked = new ControllerInteractionEvent();

        public ControllerInteractionEvent OnButtonPrimaryIndexTriggerPressed = new ControllerInteractionEvent();


        public ControllerInteractionEvent OnButtonPrimaryHandTriggerClicked = new ControllerInteractionEvent();

        public ControllerInteractionEvent OnButtonPrimaryHandTriggerPressed = new ControllerInteractionEvent();


        public ControllerInteractionEvent OnButtonRingHandTriggerClicked = new ControllerInteractionEvent();

        public ControllerInteractionEvent OnButtonRingHandTriggerPressed = new ControllerInteractionEvent();


        public ControllerInteractionEvent OnButtonPrimaryHandTriggerTouched = new ControllerInteractionEvent();
        
        public ControllerInteractionEvent OnButtonPrimaryHandTriggerNoLongerTouched = new ControllerInteractionEvent();

        public ControllerInteractionEvent OnButtonPrimaryHandTriggerHoldTouched = new ControllerInteractionEvent();


        public ControllerInteractionEvent OnButtonPrimaryThumbstickTouched = new ControllerInteractionEvent();

        public ControllerInteractionEvent OnButtonPrimaryThumbstickHoldTouched = new ControllerInteractionEvent();

        #region Button One

        public void ButtonOneClicked(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonOneClicked.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonOneClicked.Invoke(handType);
                }
            }

            // return _button.GetStateDown(HandType);
        }

        public void ButtonOnePressed(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonOnePressed.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonOnePressed.Invoke(handType);
                }
            }
        }

        #endregion


        #region Button Two

        public void ButtonTwoClicked(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonTwoClicked.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonTwoClicked.Invoke(handType);
                }
            }

            // return _button.GetStateDown(HandType);
        }

        public void ButtonTwoPressed(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonTwoPressed.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonTwoPressed.Invoke(handType);
                }
            }
        }

        #endregion

        #region Button Primary Index Trigger

        public void ButtonPrimaryIndexTriggerClicked(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryIndexTriggerClicked.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryIndexTriggerClicked.Invoke(handType);
                }
            }
        }

        public void ButtonPrimaryIndexTriggerPressed(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryIndexTriggerPressed.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryIndexTriggerPressed.Invoke(handType);
                }
            }
        }

        #endregion

        #region Button Primary Hand Trigger

        public void ButtonPrimaryHandTriggerClicked(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryHandTriggerClicked.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryHandTriggerClicked.Invoke(handType);
                }
            }
        }

        public void ButtonPrimaryHandTriggerPressed(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryHandTriggerPressed.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryHandTriggerPressed.Invoke(handType);
                }
            }
        }

        #endregion

        #region Button Ring Hand Trigger

        public void ButtonRingHandTriggerClicked(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonRingHandTriggerClicked.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonRingHandTriggerClicked.Invoke(handType);
                }
            }
        }

        public void ButtonRingHandTriggerPressed(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonRingHandTriggerPressed.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonRingHandTriggerPressed.Invoke(handType);
                }
            }
        }

        #endregion

        #region Button Primary Hand Trigger Touched

        public void ButtonPrimaryHandTriggerTouched(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryHandTriggerTouched.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryHandTriggerTouched.Invoke(handType);
                }
            }
        }
        
        public void ButtonPrimaryHandTriggerNoLongerTouched(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetStateUp(handType))
                {
                    Debug.Log("Touch " + button.GetShortName() + " Not Touched By : " + handType);
                    OnButtonPrimaryHandTriggerNoLongerTouched.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetStateUp(handType))
                {
                    Debug.Log("Touch " + button.GetShortName() + " Not Touched By : " + handType);
                    OnButtonPrimaryHandTriggerNoLongerTouched.Invoke(handType);
                }
            }
        }

        public void ButtonPrimaryHandTriggerHoldTouched(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryHandTriggerHoldTouched.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryHandTriggerHoldTouched.Invoke(handType);
                }
            }
        }

        #endregion

        #region Button Primary Thumbstick Touched

        public void ButtonPrimaryThumbstickTouched(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryThumbstickTouched.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetStateDown(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryThumbstickTouched.Invoke(handType);
                }
            }
        }

        public void ButtonPrimaryThumbstickHoldTouched(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
        {
            if (handType == SteamVR_Input_Sources.LeftHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryThumbstickHoldTouched.Invoke(handType);
                }
            }

            if (handType == SteamVR_Input_Sources.RightHand)
            {
                if (button.GetState(handType))
                {
                    Debug.Log("Button " + button.GetShortName() + " Clicked By : " + handType);
                    OnButtonPrimaryThumbstickHoldTouched.Invoke(handType);
                }
            }
        }

        #endregion
    }
}