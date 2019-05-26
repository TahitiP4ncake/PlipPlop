using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Inputs
    public Vector2 direction;
    public bool possess;
    public bool unpossess;
    public bool jump;
    public bool talk;
    public int answerDirection;

    void Update()
    {
        GetInputs();
    }

    public void GetInputs()
    {
        // Get Movement Inputs
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Get Possess Inputs
        possess = (Input.GetMouseButtonDown(1) || Input.GetButtonDown("RightBumper"));
        unpossess = (Input.GetMouseButtonDown(0) || Input.GetButtonDown("LeftBumper"));

        // Get Jump Inputs
        jump = (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("GamepadJump"));

        talk = (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("GamepadInteract"));
        answerDirection = System.Convert.ToInt32(Input.GetKeyDown(KeyCode.RightArrow)) - System.Convert.ToInt32(Input.GetKeyDown(KeyCode.LeftArrow))
            + Mathf.RoundToInt(Input.GetAxis("GamepadAnswerChoice"));
    }
}
