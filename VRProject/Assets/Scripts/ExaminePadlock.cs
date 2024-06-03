using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminePadlock : Interactable
{
    [SerializeField] private GameObject PadlockScreen;
    [SerializeField] private GameObject firstDigitGameObject;
    [SerializeField] private GameObject secondDigitGameObject;
    [SerializeField] private GameObject thirdDigitGameObject;

    private readonly uint[] digits = new uint[3] { 1, 1, 1 };
    private readonly uint[] solution = new uint[3] { 1, 2, 3 };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        GetComponent<Animator>().SetBool("Unlocked", true);
    }

    public void ExitScreen()
    {
        PadlockScreen.SetActive(false);
        Settings.padlockOn = false;
        CursorManager.HideCursor();
    }

    public override void Interact()
    {
        PadlockScreen.SetActive(true);
        Settings.padlockOn = true;
        CursorManager.ShowCursor();
    }

    public override string GetLabel()
    {
        return "Interact";
    }

    public void FirstDigitUp()
    {
        firstDigitGameObject.transform.localEulerAngles =
            new(firstDigitGameObject.transform.localEulerAngles.x, firstDigitGameObject.transform.localEulerAngles.y, firstDigitGameObject.transform.localEulerAngles.z + 40f);
        digits[0] = (digits[0] + 1) == 10 ? 1 : (digits[0] + 1);
    }

    public void SecondDigitUp()
    {
        secondDigitGameObject.transform.localEulerAngles =
            new(secondDigitGameObject.transform.localEulerAngles.x, secondDigitGameObject.transform.localEulerAngles.y, secondDigitGameObject.transform.localEulerAngles.z + 40f);
        digits[1] = (digits[1] + 1) == 10 ? 1 : (digits[1] + 1);
    }

    public void ThirdDigitUp()
    {
        thirdDigitGameObject.transform.localEulerAngles =
            new(thirdDigitGameObject.transform.localEulerAngles.x, thirdDigitGameObject.transform.localEulerAngles.y, thirdDigitGameObject.transform.localEulerAngles.z + 40f);
        digits[2] = (digits[2] + 1) == 10 ? 1 : (digits[2] + 1);
    }

    public void FirstDigitDown()
    {
        firstDigitGameObject.transform.localEulerAngles =
            new(firstDigitGameObject.transform.localEulerAngles.x, firstDigitGameObject.transform.localEulerAngles.y, firstDigitGameObject.transform.localEulerAngles.z - 40f);
        digits[0] = (digits[0] - 1) == 0 ? 1 : (digits[0] - 1);
    }

    public void SecondDigitDown()
    {
        secondDigitGameObject.transform.localEulerAngles =
            new(secondDigitGameObject.transform.localEulerAngles.x, secondDigitGameObject.transform.localEulerAngles.y, secondDigitGameObject.transform.localEulerAngles.z - 40f);
        digits[1] = (digits[1] - 1) == 0 ? 1 : (digits[1] - 1);
    }

    public void ThirdDigitDown()
    {
        thirdDigitGameObject.transform.localEulerAngles =
            new(thirdDigitGameObject.transform.localEulerAngles.x, thirdDigitGameObject.transform.localEulerAngles.y, thirdDigitGameObject.transform.localEulerAngles.z - 40f);
        digits[2] = (digits[2] - 1) == 0 ? 1 : (digits[2] - 1);
    }
}
