using System;
using Falah;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;


public class FalahSteamVREventBehaviour : MonoBehaviour
{
    public SteamVR_Input_Sources HandType;
    
    public SteamVR_Action_Boolean AButtonClick;

    public SteamVR_Action_Boolean XButtonClick;
    
    public SteamVR_Action_Boolean BButtonClick;

    public SteamVR_Action_Boolean YButtonClick;
    
    public SteamVR_Action_Boolean R1ButtonClick;

    public SteamVR_Action_Boolean R2ButtonClick;
    
    public SteamVR_Action_Boolean L1ButtonClick;

    public SteamVR_Action_Boolean L2ButtonClick;
    
    public SteamVR_Action_Boolean RingFingerRightClick;

    public SteamVR_Action_Boolean RingFingerLeftClick;
    
    public SteamVR_Action_Boolean R2ButtonTouch;
    
    public SteamVR_Action_Boolean L2ButtonTouch;
    
    public SteamVR_Action_Boolean AnalogRightTouch;
    
    public SteamVR_Action_Boolean AnalogLeftTouch;

    [SerializeField] private FalahSteamVREvent _falahSteamVrEvent;
    
    public delegate void ControllerInteractionEventHandler(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType);
    
    private void Start()
    {
        //_falahSteamVrEvent = new FalahSteamVREvent();
    }

    private void Update()
    {
        if (HandType == SteamVR_Input_Sources.LeftHand)
        {
            _falahSteamVrEvent.ButtonOneClicked(XButtonClick, HandType);
            _falahSteamVrEvent.ButtonOnePressed(XButtonClick, HandType);
        }

        if (HandType == SteamVR_Input_Sources.RightHand)
        {
            _falahSteamVrEvent.ButtonOneClicked(AButtonClick, HandType);
            _falahSteamVrEvent.ButtonOnePressed(AButtonClick, HandType);
        }
        
        
        
        if (HandType == SteamVR_Input_Sources.LeftHand)
        {
            _falahSteamVrEvent.ButtonTwoClicked(YButtonClick, HandType);
            _falahSteamVrEvent.ButtonTwoPressed(YButtonClick, HandType);
        }

        if (HandType == SteamVR_Input_Sources.RightHand)
        {
            _falahSteamVrEvent.ButtonTwoClicked(BButtonClick, HandType);
            _falahSteamVrEvent.ButtonTwoPressed(BButtonClick, HandType);
        }
        
        
        
        if (HandType == SteamVR_Input_Sources.LeftHand)
        {
            _falahSteamVrEvent.ButtonPrimaryIndexTriggerClicked(L1ButtonClick, HandType);
            _falahSteamVrEvent.ButtonPrimaryIndexTriggerPressed(L1ButtonClick, HandType);
        }

        if (HandType == SteamVR_Input_Sources.RightHand)
        {
            _falahSteamVrEvent.ButtonPrimaryIndexTriggerClicked(R1ButtonClick, HandType);
            _falahSteamVrEvent.ButtonPrimaryIndexTriggerPressed(R1ButtonClick, HandType);
        }



        if (HandType == SteamVR_Input_Sources.LeftHand)
        {
            _falahSteamVrEvent.ButtonPrimaryHandTriggerClicked(L2ButtonClick, HandType);
            _falahSteamVrEvent.ButtonPrimaryHandTriggerPressed(L2ButtonClick, HandType);
        }

        if (HandType == SteamVR_Input_Sources.RightHand)
        {
            _falahSteamVrEvent.ButtonPrimaryHandTriggerClicked(R2ButtonClick, HandType);
            _falahSteamVrEvent.ButtonPrimaryHandTriggerPressed(R2ButtonClick, HandType);
        }
        
        
        
        if (HandType == SteamVR_Input_Sources.LeftHand)
        {
            _falahSteamVrEvent.ButtonRingHandTriggerClicked(RingFingerLeftClick, HandType);
            _falahSteamVrEvent.ButtonRingHandTriggerPressed(RingFingerLeftClick, HandType);
        }

        if (HandType == SteamVR_Input_Sources.RightHand)
        {
            _falahSteamVrEvent.ButtonRingHandTriggerClicked(RingFingerRightClick, HandType);
            _falahSteamVrEvent.ButtonRingHandTriggerPressed(RingFingerRightClick, HandType);
        }
        
        
        if (HandType == SteamVR_Input_Sources.LeftHand)
        {
            _falahSteamVrEvent.ButtonPrimaryHandTriggerTouched(L2ButtonTouch, HandType);
            _falahSteamVrEvent.ButtonPrimaryHandTriggerHoldTouched(L2ButtonTouch, HandType);
        }

        if (HandType == SteamVR_Input_Sources.RightHand)
        {
            _falahSteamVrEvent.ButtonPrimaryHandTriggerTouched(R2ButtonTouch, HandType);
            _falahSteamVrEvent.ButtonPrimaryHandTriggerHoldTouched(R2ButtonTouch, HandType);
        }
        
        if (HandType == SteamVR_Input_Sources.LeftHand)
        {
            _falahSteamVrEvent.ButtonPrimaryHandTriggerNoLongerTouched(L2ButtonTouch, HandType);
            _falahSteamVrEvent.ButtonPrimaryHandTriggerNoLongerTouched(L2ButtonTouch, HandType);
        }

        if (HandType == SteamVR_Input_Sources.RightHand)
        {
            _falahSteamVrEvent.ButtonPrimaryHandTriggerNoLongerTouched(R2ButtonTouch, HandType);
            _falahSteamVrEvent.ButtonPrimaryHandTriggerNoLongerTouched(R2ButtonTouch, HandType);
        }
        
        
        if (HandType == SteamVR_Input_Sources.LeftHand)
        {
            _falahSteamVrEvent.ButtonPrimaryThumbstickTouched(AnalogLeftTouch, HandType);
            _falahSteamVrEvent.ButtonPrimaryThumbstickHoldTouched(AnalogLeftTouch, HandType);
        }

        if (HandType == SteamVR_Input_Sources.RightHand)
        {
            _falahSteamVrEvent.ButtonPrimaryThumbstickTouched(AnalogRightTouch, HandType);
            _falahSteamVrEvent.ButtonPrimaryThumbstickHoldTouched(AnalogRightTouch, HandType);
        }
        
        /*_falahSteamVrEvent.ButtonOnePressed(HandType);
        
        _falahSteamVrEvent.ButtonOneReleased(HandType);
        
        _falahSteamVrEvent.ButtonOneTouchReleased(HandType);
        
        _falahSteamVrEvent.ButtonOneTouched(HandType);
        
        _falahSteamVrEvent.ButtonOneHoldTouched(HandType);*/
        /*    
        _oculusEvent.ButtonTwoClicked(HandType);
        
        _oculusEvent.ButtonTwoPressed(HandType);
        
        _oculusEvent.ButtonTwoReleased(HandType);
        
        _oculusEvent.ButtonTwoTouchReleased(HandType);
        
        _oculusEvent.ButtonTwoTouched(HandType);
        
        _oculusEvent.ButtonTwoHoldTouched(HandType);
        
        
        
        _oculusEvent.ButtonPrimaryThumbstickClicked(HandType);
        
        _oculusEvent.ButtonPrimaryThumbstickPressed(HandType);
        
        _oculusEvent.ButtonPrimaryThumbstickReleased(HandType);
        
        _oculusEvent.ButtonPrimaryThumbstickTouchReleased(HandType);
        
        _oculusEvent.ButtonPrimaryThumbstickTouched(HandType);
        
        _oculusEvent.ButtonPrimaryThumbstickHoldTouched(HandType);
        
        
        
        _oculusEvent.ButtonPrimaryIndexTriggerClicked(HandType);
        
        _oculusEvent.ButtonPrimaryIndexTriggerPressed(HandType);
        
        _oculusEvent.ButtonPrimaryIndexTriggerReleased(HandType);
        
        //_oculusEvent.ButtonPrimaryIndexTriggerTouchReleased(Controller);
        
        _oculusEvent.ButtonPrimaryIndexTriggerTouched(HandType);
        
        _oculusEvent.ButtonPrimaryIndexTriggerHoldTouched(HandType);
        
        
        
        _oculusEvent.ButtonPrimaryHandTriggerClicked(HandType);
        
        _oculusEvent.ButtonPrimaryHandTriggerPressed(HandType);
        
        _oculusEvent.ButtonPrimaryHandTriggerReleased(HandType);
        */

        //_oculusEvent.ButtonPrimaryHandTriggerTouchReleased(Controller);
        

    }

    private void OnEnable()
    {
        ButtonOneClicked += _falahSteamVrEvent.ButtonOneClicked;
        
        ButtonOnePressed += _falahSteamVrEvent.ButtonOnePressed;
        
        
        ButtonTwoPressed += _falahSteamVrEvent.ButtonTwoPressed;

        ButtonTwoClicked += _falahSteamVrEvent.ButtonTwoClicked;



        ButtonPrimaryIndexTriggerPressed += _falahSteamVrEvent.ButtonPrimaryIndexTriggerPressed;

        ButtonPrimaryIndexTriggerClicked += _falahSteamVrEvent.ButtonPrimaryIndexTriggerClicked;
        

        
        ButtonPrimaryHandTriggerPressed += _falahSteamVrEvent.ButtonPrimaryHandTriggerPressed;

        ButtonPrimaryHandTriggerClicked += _falahSteamVrEvent.ButtonPrimaryHandTriggerClicked;

        
        
        ButtonRingHandTriggerPressed += _falahSteamVrEvent.ButtonRingHandTriggerPressed;
        
        ButtonRingHandTriggerClicked += _falahSteamVrEvent.ButtonRingHandTriggerClicked;
        
        
        
        ButtonPrimaryHandTriggerTouched += _falahSteamVrEvent.ButtonPrimaryHandTriggerTouched;
        
        ButtonPrimaryHandTriggerNoLongerTouched += _falahSteamVrEvent.ButtonPrimaryHandTriggerNoLongerTouched;
        
        ButtonPrimaryHandTriggerHoldTouched += _falahSteamVrEvent.ButtonPrimaryHandTriggerHoldTouched;
        
        
        
        ButtonPrimaryThumbstickTouched += _falahSteamVrEvent.ButtonPrimaryThumbstickTouched;
        
        ButtonPrimaryThumbstickHoldTouched += _falahSteamVrEvent.ButtonPrimaryThumbstickHoldTouched;
        
        
    }

    private void OnDisable()
    {
        ButtonOneClicked -= _falahSteamVrEvent.ButtonOneClicked;
        
        ButtonOnePressed -= _falahSteamVrEvent.ButtonOnePressed;
        
        
        ButtonTwoPressed -= _falahSteamVrEvent.ButtonTwoPressed;

        ButtonTwoClicked -= _falahSteamVrEvent.ButtonTwoClicked;



        ButtonPrimaryIndexTriggerPressed -= _falahSteamVrEvent.ButtonPrimaryIndexTriggerPressed;

        ButtonPrimaryIndexTriggerClicked -= _falahSteamVrEvent.ButtonPrimaryIndexTriggerClicked;
        

        
        ButtonPrimaryHandTriggerPressed -= _falahSteamVrEvent.ButtonPrimaryHandTriggerPressed;

        ButtonPrimaryHandTriggerClicked -= _falahSteamVrEvent.ButtonPrimaryHandTriggerClicked;

        
        
        ButtonRingHandTriggerPressed -= _falahSteamVrEvent.ButtonRingHandTriggerPressed;
        
        ButtonRingHandTriggerClicked -= _falahSteamVrEvent.ButtonRingHandTriggerClicked;
        
        
        
        ButtonPrimaryHandTriggerTouched -= _falahSteamVrEvent.ButtonPrimaryHandTriggerTouched;
        
        ButtonPrimaryHandTriggerNoLongerTouched -= _falahSteamVrEvent.ButtonPrimaryHandTriggerNoLongerTouched;
        
        ButtonPrimaryHandTriggerHoldTouched -= _falahSteamVrEvent.ButtonPrimaryHandTriggerHoldTouched;
        
        
        
        ButtonPrimaryThumbstickTouched -= _falahSteamVrEvent.ButtonPrimaryThumbstickTouched;
        
        ButtonPrimaryThumbstickHoldTouched -= _falahSteamVrEvent.ButtonPrimaryThumbstickHoldTouched;
        /*ButtonOnePressed -= _oculusEvent.ButtonOnePressed;
        
        ButtonOneReleased -= _oculusEvent.ButtonOneReleased;
        
        ButtonOneTouchReleased -= _oculusEvent.ButtonOneTouchReleased;
        
        ButtonOneClicked -= _oculusEvent.ButtonOneClicked;
        
        ButtonOneTouched -= _oculusEvent.ButtonOneTouched;
        
        ButtonOneHoldTouched -= _oculusEvent.ButtonOneHoldTouched;
        
        
        ButtonTwoPressed -= _oculusEvent.ButtonTwoPressed;
        
        ButtonTwoReleased -= _oculusEvent.ButtonTwoReleased;
        
        ButtonTwoTouchReleased -= _oculusEvent.ButtonTwoTouchReleased;
        
        ButtonTwoClicked -= _oculusEvent.ButtonTwoClicked;
        
        ButtonTwoTouched -= _oculusEvent.ButtonTwoTouched;
        
        ButtonTwoHoldTouched -= _oculusEvent.ButtonTwoHoldTouched;
        
        
        ButtonPrimaryThumbstickPressed -= _oculusEvent.ButtonPrimaryThumbstickPressed;
        
        ButtonPrimaryThumbstickReleased -= _oculusEvent.ButtonPrimaryThumbstickReleased;
        
        ButtonPrimaryThumbstickTouchReleased -= _oculusEvent.ButtonPrimaryThumbstickTouchReleased;
        
        ButtonPrimaryThumbstickClicked -= _oculusEvent.ButtonPrimaryThumbstickClicked;

        ButtonPrimaryThumbstickTouched -= _oculusEvent.ButtonPrimaryThumbstickTouched;
        
        ButtonPrimaryThumbstickHoldTouched -= _oculusEvent.ButtonPrimaryThumbstickHoldTouched;
        
        
        
        ButtonPrimaryIndexTriggerPressed -= _oculusEvent.ButtonPrimaryIndexTriggerPressed;
        
        ButtonPrimaryIndexTriggerReleased -= _oculusEvent.ButtonPrimaryIndexTriggerReleased;
        
        //ButtonPrimaryIndexTriggerTouchReleased -= _oculusEvent.ButtonPrimaryIndexTriggerTouchReleased;
        
        ButtonPrimaryIndexTriggerClicked -= _oculusEvent.ButtonPrimaryIndexTriggerClicked;

        ButtonPrimaryIndexTriggerTouched -= _oculusEvent.ButtonPrimaryIndexTriggerTouched;
        
        ButtonPrimaryIndexTriggerHoldTouched -= _oculusEvent.ButtonPrimaryIndexTriggerHoldTouched;
        
        
        
        ButtonPrimaryHandTriggerPressed -= _oculusEvent.ButtonPrimaryHandTriggerPressed;
        
        ButtonPrimaryHandTriggerReleased -= _oculusEvent.ButtonPrimaryHandTriggerReleased;
        
        ButtonPrimaryHandTriggerClicked -= _oculusEvent.ButtonPrimaryHandTriggerClicked;
        */

        //ButtonPrimaryHandTriggerTouchReleased -= _oculusEvent.ButtonPrimaryHandTriggerTouchReleased;
    }

    /// <summary>
    /// Emitted when button one is pressed.
    /// </summary>
    public event ControllerInteractionEventHandler ButtonOnePressed;

    /// <summary>
    /// Emitted when button one is clicked.
    /// </summary>
    public event ControllerInteractionEventHandler ButtonOneClicked;



    /// <summary>
    /// Emitted when button Two is pressed.
    /// </summary>
    public event ControllerInteractionEventHandler ButtonTwoPressed;

    /// <summary>
    /// Emitted when button Two is clicked.
    /// </summary>
    public event ControllerInteractionEventHandler ButtonTwoClicked;



    /// <summary>
    /// Emitted when button Primary Index Trigger is pressed.
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryIndexTriggerPressed;

    /// <summary>
    /// Emitted when button Primary Index Trigger is clicked.
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryIndexTriggerClicked;



    /// <summary>
    /// Emitted when button Primary Hand Trigger is pressed.
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryHandTriggerPressed;

    /// <summary>
    /// Emitted when button Primary Index Trigger is clicked.
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryHandTriggerClicked;
    
    
    /// <summary>
    /// Emitted when button Primary Index Trigger is touched
    /// </summary>
    public event ControllerInteractionEventHandler ButtonRingHandTriggerClicked;
    
    /// <summary>
    /// Emitted when button Primary Index Trigger is hold touched
    /// </summary>
    public event ControllerInteractionEventHandler ButtonRingHandTriggerPressed;
    
    
    /// <summary>
    /// Emitted when touch Primary Thumbstick is touched
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryHandTriggerTouched;
    
    /// <summary>
    /// Emitted when touch Primary Thumbstick is hold touched
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryHandTriggerHoldTouched;
    
    /// <summary>
    /// Emitted when touch Primary Thumbstick is no longer touched
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryHandTriggerNoLongerTouched;
    
    
    /// <summary>
    /// Emitted when touch Primary Thumbstick is touched
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryThumbstickTouched;
    
    /// <summary>
    /// Emitted when touch Primary Thumbstick is hold touched
    /// </summary>
    public event ControllerInteractionEventHandler ButtonPrimaryThumbstickHoldTouched;
    
    public void Debugging()
    {
        Debug.Log("ButtonOnePressed");
    }
    
    
}

[Serializable]
public class FalahSteamVREvents
{
    [Serializable]
    public sealed class ControllerInteractionEvent : UnityEvent<object> { }

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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonOneClicked.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetStateDown(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonOnePressed.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetState(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonTwoClicked.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetStateDown(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonTwoPressed.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetState(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryIndexTriggerClicked.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetStateDown(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryIndexTriggerPressed.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetState(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryHandTriggerClicked.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetStateDown(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryHandTriggerPressed.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetState(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonRingHandTriggerClicked.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetStateDown(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonRingHandTriggerPressed.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetState(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryHandTriggerTouched.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetStateDown(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryHandTriggerTouched.Invoke(handType);
            }
        }
    }
    
    public void ButtonPrimaryHandTriggerHoldTouched(SteamVR_Action_Boolean button, SteamVR_Input_Sources handType)
    {
        if (handType == SteamVR_Input_Sources.LeftHand)
        {
            if (button.GetState(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryHandTriggerHoldTouched.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetState(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryThumbstickTouched.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetStateDown(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
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
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryThumbstickHoldTouched.Invoke(handType);
            }
        }

        if (handType == SteamVR_Input_Sources.RightHand)
        {
            if (button.GetState(handType))
            {
                Debug.Log("Button "+ button.GetShortName() + " Clicked By : " + handType);
                OnButtonPrimaryThumbstickHoldTouched.Invoke(handType);
            }
        }
    }

    #endregion
    
}