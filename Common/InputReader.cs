using UnityEngine;

public class InputReader : MonoBehaviour
{
    private bool fireButton1;
    private bool fireButton2;
    private bool moveButton;
    private bool rotateRightButton;
    private bool rotateLeftButton;
    private bool settingsButton;
    private bool startButton;

    private void Update()
    {
        InputReaderLoop();
    }
    private void InputReaderLoop()
    {
        ReadButton();

        if (fireButton1)
        {
            EventBus.OnPressFireButton1(1);
        }

        if (fireButton2)
        {
            EventBus.OnPressFireButton2(2);
        }

        if (moveButton)
        {
            EventBus.OnPressMoveButton(Input.GetAxis("Vertical"));
        }
        else
        {
            EventBus.OnPressMoveButton(0f);
        }

        if (rotateRightButton)
        {
            EventBus.OnPressRotateButton(Input.GetAxis("Horizontal"));
        }

        if (rotateLeftButton)
        {
            EventBus.OnPressRotateButton(Input.GetAxis("Horizontal"));
        }

        if (settingsButton)
        {
            EventBus.OnPressSettingsButton();
        }

        if (startButton)
        {
            if (!SceneController.GameStarted)
            {
                EventBus.OnStartGame();
            }
        }
    }

    private void ReadButton()
    {
        fireButton1 = Input.GetKeyDown(KeyCode.Space);
        fireButton2 = Input.GetKeyDown(KeyCode.E);
        moveButton  = Input.GetAxis("Vertical") > 0;
        rotateRightButton = Input.GetAxis("Horizontal") > 0;
        rotateLeftButton  = Input.GetAxis("Horizontal") < 0;
        settingsButton    = Input.GetKeyDown(KeyCode.Escape);
        startButton = Input.GetKeyDown(KeyCode.Return);
    }
}
