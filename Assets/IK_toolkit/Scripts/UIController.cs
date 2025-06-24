using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    class TeachData
    {
        public int stepNum;
        public Vector3 pos;
        public float duration;
        public bool isGripperOn;
    }

    public IK_toolkit ikToolkit;
    public TMP_InputField xInput;
    public TMP_InputField yInput;
    public TMP_InputField zInput;
    public bool isXPlusBtnClicking;
    public bool isXMinusBtnClicking;
    public bool isYPlusBtnClicking;
    public bool isYMinusBtnClicking;
    public bool isZPlusBtnClicking;
    public bool isZMinusBtnClicking;
    public float x, y, z;
    public float multiplier = 0.01f;
    public float reach = 2;

    private void Start()
    {
        x = ikToolkit.ik.position.x;
        y = ikToolkit.ik.position.y;
        z = ikToolkit.ik.position.z;

        teachDataPath = Application.persistentDataPath + "/teachingData.txt";
    }

    // Update is called once per frame
    void Update()
    {
        bool isMovable = CanMove();

        if (!isMovable)
            return;

        if (isXPlusBtnClicking)
        {
            x += multiplier;
        }

        if (isXMinusBtnClicking)
        {
            x -= multiplier;
        }

        if (isYPlusBtnClicking)
        {
            y += multiplier;
        }

        if (isYMinusBtnClicking)
        {
            y -= multiplier;
        }

        if (isZPlusBtnClicking)
        {
            z += multiplier;
        }

        if (isZMinusBtnClicking)
        {
            z -= multiplier;
        }

        UpdateEndEffector();
    }

    private void UpdateEndEffector()
    {
        xInput.text = x.ToString();
        yInput.text = y.ToString();
        zInput.text = z.ToString();

        // ikToolkit의 Base에서 end-effector의 거리가
        // 특정 거리 이상이 되면 아래 코드 실행 X

        ikToolkit.ik.position = new Vector3(x, y, z);
    }

    Vector3 currentPos;
    private bool CanMove()
    {
        Vector3 dir = ikToolkit.ik.position - ikToolkit.robot[0].position;
        float distance = dir.magnitude;

        if (distance >= reach)
        {
            Debug.LogWarning("입력값이 리치를 초과하였습니다.");

            ikToolkit.ik.position = currentPos;
            return false;
        }
        else
        {
            currentPos = ikToolkit.ik.position;
            return true;
        }
    }

    // teach버튼을 누르면 로봇의 End-effector의 위치를
    // List에 저장하고, 동시에 text 파일 teachingData.txt에 저장
    // position,duration,isSuctionOn
    // step1,3,5,6,3.5,true
    // step2,3,5,6,3.5,true
    // step3,3,5,6,3.5,true

    List<TeachData> teachDatas = new List<TeachData>();
    public int stepCnt;
    public float duration;
    public bool isGripperOn;
    public string teachDataPath;

    public void OnTeachBtnClkEvent()
    {
        TeachData teachData = new TeachData();
        teachData.stepNum = stepCnt++;
        teachData.duration = duration;
        teachData.isGripperOn = isGripperOn;
        teachData.pos = ikToolkit.ik.position;

        teachDatas.Add(teachData);

        AddLine(teachData);
    }

    private void AddLine(TeachData teachData)
    {
        if(File.Exists(teachDataPath))
        {
            using(FileStream fs = new FileStream(teachDataPath, FileMode.Append))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    string data = $"{teachData.stepNum},{teachData.pos},{teachData.duration},{teachData.isGripperOn}";
                    sw.WriteLine(data);
                    Debug.Log($"데이터 추가: {data}");
                }
            }
        }
    }

    // X
    public void OnXPlusBtnDownEvent()
    {
        isXPlusBtnClicking = true;
    }
    public void OnXPlusBtnUpEvent()
    {
        isXPlusBtnClicking = false;
    }
    public void OnXMinusBtnDownEvent()
    {
        isXMinusBtnClicking = true;
    }
    public void OnXMinusBtnUpEvent()
    {
        isXMinusBtnClicking = false;
    }
    // Y
    public void OnYPlusBtnDownEvent()
    {
        isYPlusBtnClicking = true;
    }
    public void OnYPlusBtnUpEvent()
    {
        isYPlusBtnClicking = false;
    }
    public void OnYMinusBtnDownEvent()
    {
        isYMinusBtnClicking = true;
    }
    public void OnYMinusBtnUpEvent()
    {
        isYMinusBtnClicking = false;
    }
    // Z
    public void OnZPlusBtnDownEvent()
    {
        isZPlusBtnClicking = true;
    }
    public void OnZPlusBtnUpEvent()
    {
        isZPlusBtnClicking = false;
    }
    public void OnZMinusBtnDownEvent()
    {
        isZMinusBtnClicking = true;
    }
    public void OnZMinusBtnUpEvent()
    {
        isZMinusBtnClicking = false;
    }
}
