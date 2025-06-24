using System.Collections;
using UnityEngine;

// Cobot의 End-effector를 내가 원하는 위치와 회전으로 정해준다.
public class AutomationController : MonoBehaviour
{
    public Transform targetToPick;       // 타겟
    public Transform targetToPlaceInCNC; // CNC머신에 위치시키기 위한 게임오브젝트
    public Transform targetHome;         // 초기 위치

    // 시퀀스 프로그램 작성
    IEnumerator MachineTendingProcess()
    {
        while(true)
        {

        }
    }
}
