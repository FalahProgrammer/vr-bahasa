using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PointerHandlerBehaviour))]
public class ButtonController : DoScaleBehaviour, iResetable
{
    [HideInInspector] public AudioSource _audioSource;

    [HideInInspector] public AudioClip _onHoverSound;

    [HideInInspector] public AudioClip _onClickSound;

    [HideInInspector] public AudioClip _onExitSound;

    [HideInInspector] public Sprite _onHoverImage;

    [HideInInspector] public Sprite _onClickImage;

    [HideInInspector] public Sprite _onIdleImage;

    [HideInInspector] public Color _hoverColor;

    [HideInInspector] public Color _clickColor;

    [HideInInspector] public Color _idleColor;
    
    [HideInInspector] public bool _usePlaceHolderText;
    
    [HideInInspector]
    [Tooltip("Adjust your pivot")]
    public float _placeHolderPivot;
    
    [HideInInspector]
    [Tooltip("Select your pivot direction")]
    public MyMode PivotMode;
    public enum MyMode
    {
        X,
        Y,
        Z
    }

    private Image _myImage;
    
    public enum ButtonMode
    {
        Unset,
        Image,
        Color
    }

    [HideInInspector] public ButtonMode ImageMode;

    

    private PointerHandlerBehaviour _pointerHandlerBehaviour;

    private DoMoveBehaviour _doMoveBehaviour;

    private void Start()
    {
        _myImage = GetComponent<Image>();
        Debug.Log(_usePlaceHolderText);
        
        if (!_usePlaceHolderText)
        {
            //Destroy(transform.GetChild(0).Equals());
        }
        else
        {
            GameObject placeholder = Resources.Load ("Prefabs/Button Controller/Place Holder Prefab") as GameObject;

            GameObject placehold = Instantiate(placeholder, transform);

            switch (PivotMode)
            {
                case MyMode.X:

                    placehold.transform.localPosition = new Vector3(
                        x: transform.localPosition.x + _placeHolderPivot,
                        y: transform.localPosition.y,
                        z: transform.localPosition.z);

                    _doMoveBehaviour = placehold.GetComponent<DoMoveBehaviour>();
                    
                    break;
                
                case MyMode.Y:

                    placehold.transform.localPosition = new Vector3(
                        x: transform.localPosition.x,
                        transform.localPosition.y + _placeHolderPivot,
                        z: transform.localPosition.z);
                    
                    _doMoveBehaviour = placehold.GetComponent<DoMoveBehaviour>();
                    
                    break;
                
                case MyMode.Z:

                    placehold.transform.localPosition = new Vector3(
                        x: transform.localPosition.x,
                        y: transform.localPosition.y,
                        z: transform.localPosition.z + _placeHolderPivot);
                    
                    _doMoveBehaviour = placehold.GetComponent<DoMoveBehaviour>();
                    
                    break;
                
            }
            

           
        }

        //_myImage.sprite = _onIdleImage;

        _pointerHandlerBehaviour = GetComponent<PointerHandlerBehaviour>();
        
        

        _pointerHandlerBehaviour.OnPointerEnterEvent.AddListener(OnHover);

        _pointerHandlerBehaviour.OnPointerClickEvent.AddListener(OnClick);

        _pointerHandlerBehaviour.OnPointerExitEvent.AddListener(OnExit);

        if (Camera.main != null) _audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void OnHover()
    {
        BeginDoScale();

        if (_usePlaceHolderText)
        {
            _doMoveBehaviour.BeginDoSelectLocalMove();
        }
    }

    public void OnClick()
    {
        switch (ImageMode)
        {
            case ButtonMode.Image:
                
                if (_onClickImage)
                {
                    _myImage.sprite = _onClickImage;
                }

                break;

            case ButtonMode.Color:

                GetComponent<Image>().color = _clickColor;
                
                break;
        }
        
        ClickSound();
        
    }

    public void ClickSound()
    {
        _audioSource.PlayOneShot(_onClickSound);
    }

    public void HoverSound()
    {
        _audioSource.PlayOneShot(_onHoverSound);
    }

    public void ExitSound()
    {
        _audioSource.PlayOneShot(_onExitSound);
    }

    public void OnExit()
    {
        ResetScale();

        if (_usePlaceHolderText)
        {
            _doMoveBehaviour.BeginDoLocalMoveInitialPosition();
        }
        
    }

    public override void BeginDoScale()
    {
        base.BeginDoScale();

        switch (ImageMode)
        {
            case ButtonMode.Image:

                GetComponent<Image>().color = Color.white;
                    
                if (_onHoverImage)
                {
                    _myImage.sprite = _onHoverImage;
                }

                break;

            case ButtonMode.Color:

                GetComponent<Image>().color = _hoverColor;
                
                break;
        }
        
    }

    public override void ResetScale()
    {
        base.ResetScale();

        switch (ImageMode)
        {
            case ButtonMode.Image:
                
                if (_onHoverImage)
                {
                    _myImage.sprite = _onIdleImage;
                }

                break;

            case ButtonMode.Color:

                GetComponent<Image>().color = _idleColor;
                
                break;
        }
    }

    public void SetButtonOff()
    {
        StartCoroutine(CoroutineButtonOff());
    }
    
    public void SetButtonOn()
    {
        StartCoroutine(CoroutineButtonOn());
    }

    IEnumerator CoroutineButtonOff()
    {
        MyTransform.GetComponent<Image>().raycastTarget = false;
        
        yield return new WaitForSeconds(0.1f);

        if (MyTransform.GetComponent<Button>())
        {
            MyTransform.GetComponent<Button>().interactable = false;
            
            if (_onHoverImage)
            {
                _myImage.sprite = _onIdleImage;
            }
        }
        
        else if (MyTransform.GetComponent<Toggle>())
        {
            MyTransform.GetComponent<Toggle>().interactable = false;

            MyTransform.GetComponent<ToggleBehaviour>().Toogle = false;
            
            if (_onHoverImage)
            {
                _myImage.sprite = _onIdleImage;
            }
        }
    }
    
    IEnumerator CoroutineButtonOn()
    {
        MyTransform.GetComponent<Image>().raycastTarget = true;
        
        yield return new WaitForSeconds(0.1f);

        if (MyTransform.GetComponent<Button>())
        {
            MyTransform.GetComponent<Button>().interactable = true;
        }
        
        else if (MyTransform.GetComponent<Toggle>())
        {
            MyTransform.GetComponent<Toggle>().interactable = true;
            
            //MyTransform.GetComponent<ToggleBehaviour>().Toogle = true;
        }
    }

    public void Reset()
    {
        ResetScale();
    }
}