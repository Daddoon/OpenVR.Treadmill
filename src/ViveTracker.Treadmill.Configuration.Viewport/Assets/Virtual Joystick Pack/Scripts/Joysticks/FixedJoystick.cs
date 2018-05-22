using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    public bool IsLeftJoystick;

    [Header("Fixed Joystick")]


    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();

    private const string Xbox360LeftX = "Xbox360_LeftX";
    private const string Xbox360LeftY = "Xbox360_LeftY";
    private const string Xbox360RightX = "Xbox360_RightX";
    private const string Xbox360RightY = "Xbox360_RightY";


    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
    }

    private void Update()
    {
        float X = default(float);
        float Y = default(float);

        if (IsLeftJoystick)
        {
            X = Input.GetAxis(Xbox360LeftX);
            Y = Input.GetAxis(Xbox360LeftY);
        }
        else
        {
            X = Input.GetAxis(Xbox360RightX);
            Y = Input.GetAxis(Xbox360RightY);
        }

        handle.anchoredPosition = (new Vector2(X, Y) * background.sizeDelta.x / 2f) * handleLimit;
    }

    //public override void OnDrag(PointerEventData eventData)
    //{
    //    Vector2 direction = eventData.position - joystickPosition;
    //    inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
    //    handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    //}

    //public override void OnPointerDown(PointerEventData eventData)
    //{
    //    OnDrag(eventData);
    //}

    //public override void OnPointerUp(PointerEventData eventData)
    //{
    //    inputVector = Vector2.zero;
    //    handle.anchoredPosition = Vector2.zero;
    //}
}