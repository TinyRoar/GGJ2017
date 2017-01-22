using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class ControllerControls : MonoBehaviour
{

    public bool ReactToA = false;
    public bool ReactToB = false;

    private bool isAllowed = true;


    void Update()
    {
        if (isAllowed == false)
            return;

         GamePadState state = GamePad.GetState(PlayerIndex.One);

        if (ReactToA == true)
        {
            // Detect if a button was pressed/release this frame
            if (state.Buttons.A == ButtonState.Pressed)
                TriggerButton();
        }

        if (ReactToB == true)
        {
            // Detect if a button was pressed/release this frame
            if (state.Buttons.B == ButtonState.Pressed)
                TriggerButton();
        }

    }

    private void TriggerButton()
    {
        isAllowed = false;
        Timer.Instance.Add(5.0f, delegate
        {
            isAllowed = true;
        });

        Debug.Log(test);
        this.GetComponent<Button>().onClick.Invoke();
    }

}
