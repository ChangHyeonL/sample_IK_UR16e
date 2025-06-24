using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public IK_toolkit ik_Toolkit;
    public TMP_InputField xInput;
    public bool isXPlusBtnClicking;
    public int x, y, z;


    // Update is called once per frame
    void Update()
    {
        if (isXPlusBtnClicking)
        {
            x++;
            xInput.text = x.ToString();
        }
    }

    private void UpdateEndEffector()
    {
        ik_Toolkit.ik.localPosition = new Vector3(x, y, z);
    }

    public void OnXPlusBtnUpEvent()
    {
        isXPlusBtnClicking = true;
    }
    public void OnXPlusBtnDownEvent()
    {
        isXPlusBtnClicking = false;
    }
}
