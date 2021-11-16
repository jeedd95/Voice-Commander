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

        // AŰ�� 0, DŰ�� 2, �ƹ��͵� �ȴ��� ���� 1�̶�� �Է� ���� ���� �Ѵ�
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

        // WŰ�� ������ 1, �ƹ��͵� �ȴ��� ���¸� 0�̶�� �Է� ���� �����Ѵ�.
        if(Input.GetKey(KeyCode.W))
        {
            actionsOut.DiscreteActions.Array.SetValue(1, 1);
        }

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //�迭 0���� ���� 0�̸� �������� ȸ��, 2�̸� ���������� ȸ��
        int rotAction = actions.DiscreteActions[0];

        transform.Rotate(transform.up, rotAction);
        // �迭 1���� ���� 1�̸� ������ �̵�
    }

    public override void OnEpisodeBegin()
    {

    }
}
