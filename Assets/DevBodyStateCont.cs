using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DevBodyStateCont : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text_UpperBodyState;
    [SerializeField] private TextMeshProUGUI Text_LowerBodyState;

    public StateMachine stateMachine;

    private void Update()
    {
        if (GameObject.FindObjectOfType<HeroObject>() != null &&  stateMachine == null)
        {
            stateMachine = GameObject.FindObjectOfType<HeroObject>().StateMachine;
        }

        if (stateMachine != null)
        {
            Text_UpperBodyState.text = "UpperBodyState : " + stateMachine.GetCurrentUpperBodyState().ToString();
            Text_LowerBodyState.text = "LowerBodyState : " + stateMachine.GetCurrentLowerBodyState().ToString();
        }
    }
}
