using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum InputMethod
{
    KeyboardInput,
    MouseInput,
    TouchInput
}


public class InputManager : MonoBehaviour
{
    [Header("Input Settings")]
    public InputMethod inputType;


    void Update()
    {
        if (inputType == InputMethod.KeyboardInput)
            KeyboardInput();
        else if (inputType == InputMethod.MouseInput)
            MouseInput();
        else if (inputType == InputMethod.TouchInput)
            TouchInput();
    }

    #region KEYBOARD
    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Keyboard Arrow Up");
            GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Up);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Keyboard Arrow Down");
            GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Down);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Keyboard Arrow Left");
            GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Keyboard Arrow Right");
            GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Right);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Keyboard Enter");
            GameManager.GamePlayer.DoAction(ActionTypes.Jump, Directions.None);
        }
    }
    #endregion

    #region MOUSE


    Vector2 _startPressPosition;
    Vector2 _endPressPosition;
    Vector2 _currentSwipe;
    bool _startedOverUI;
    float _buttonDownPhaseStart;
    public float tapDistanceOffset;

    private bool IsPointerOverUIObject(Vector2 t)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(t.x, t.y);
        List<RaycastResult> results = new List<RaycastResult>();

        if (EventSystem.current != null)
        {

            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        }

        return results.Count > 0;
    }


    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPressPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _startedOverUI = IsPointerOverUIObject(_startPressPosition);

            _buttonDownPhaseStart = Time.time;
        }

        if (Input.GetMouseButtonUp(0) && !_startedOverUI)
        {
            _endPressPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            float distance = Vector2.Distance(_startPressPosition, _endPressPosition);

            if (distance > tapDistanceOffset)
            {

                _currentSwipe = new Vector2(_endPressPosition.x - _startPressPosition.x, _endPressPosition.y - _startPressPosition.y);
                _currentSwipe.Normalize();

                //swipe left
                if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                {
                    GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse Swipe Left");
                    GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Left);
                }
                //swipe right
                if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                {
                    GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse Swipe Rigth");
                    GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Right);
                }

                //swipe down
                if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                {
                    GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse Swipe Down");
                    GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Down);
                }

                //swipe up
                if (_currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                {
                    GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse Swipe Up");
                    GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Up);
                }
            }
            else
            {


                if (_startPressPosition.x < Screen.width / 2)
                {
                    //tap on left
                    GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse tap on left");
                    GameManager.GamePlayer.DoAction(ActionTypes.Jump, Directions.None);
                }
                else
                {


                    //tap on rigth 
                    GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse tap on rigth");
                    GameManager.GamePlayer.DoAction(ActionTypes.Jump, Directions.None);
                }

            }
        }
    }


    #endregion

    #region TOUCH
    void TouchInput()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {

                _startPressPosition = touch.position;
                _startedOverUI = IsPointerOverUIObject(_startPressPosition);
                _endPressPosition = touch.position;
                _buttonDownPhaseStart = Time.time;

            }
            if (touch.phase == TouchPhase.Stationary)
            {



            }
            if (touch.phase == TouchPhase.Moved)
            {
                _endPressPosition = touch.position;

            }

            if (touch.phase == TouchPhase.Ended && !_startedOverUI)
            {
                if (touch.tapCount > 0)
                {
                    if (_startPressPosition.x < Screen.width / 2)
                    {
                        //tap on left
                        GameManager.GameDebugger.DebugLogWithMessage("[InputManager] tap on left");
                        GameManager.GamePlayer.DoAction(ActionTypes.Jump, Directions.None);
                    }
                    else
                    {


                        //tap on rigth 
                        GameManager.GameDebugger.DebugLogWithMessage("[InputManager] tap on rigth");
                        GameManager.GamePlayer.DoAction(ActionTypes.Jump, Directions.None);
                    }
                }
                else
                {
                    _currentSwipe = new Vector2(_endPressPosition.x - _startPressPosition.x, _endPressPosition.y - _startPressPosition.y);
                    _currentSwipe.Normalize();

                    //swipe left
                    if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                    {
                        GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse Swipe Left");
                        GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Left);
                    }
                    //swipe right
                    if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                    {
                        GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse Swipe Rigth");
                        GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Right);
                    }

                    //swipe down
                    if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                    {
                        GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse Swipe Down");
                        GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Down);
                    }

                    //swipe up
                    if (_currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                    {
                        GameManager.GameDebugger.DebugLogWithMessage("[InputManager] Mouse Swipe Up");
                        GameManager.GamePlayer.DoAction(ActionTypes.Move, Directions.Up);
                    }


                }

            }

        }


    }
    #endregion
}
