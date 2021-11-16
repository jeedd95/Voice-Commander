using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class JHW_PlayerMove : Agent
{
    public override void Initialize()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.DiscreteActions.Clear();

        // A키는 0, D키는 2, 아무것도 안누른 상태 1이라고 입력 값을 전달 한다
        int rotInput = 1;

        if (Input.GetKey(KeyCode.A))
        {
            rotInput = 0;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rotInput = 2;
        }

        actionsOut.DiscreteActions.Array.SetValue(rotInput, 0);

        // W키를 누르면 1, 아무것도 안누른 상태면 0이라고 입력 값을 전달한다.
        if(Input.GetKey(KeyCode.W))
        {
            actionsOut.DiscreteActions.Array.SetValue(1, 1);
        }

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //배열 0번의 값이 0이면 왼쪽으로 회전, 2이면 오른쪽으로 회전
        int rotAction = actions.DiscreteActions[0];

        transform.Rotate(transform.up, rotAction);
        // 배열 1번의 값이 1이면 앞으로 이동
    }

    public override void OnEpisodeBegin()
    {

    }
}
