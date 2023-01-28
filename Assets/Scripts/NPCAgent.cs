using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class NPCAgent : Agent
{
    public GameObject playerObject;
    NPCAction npc;
    PlayerAction player;
    // Start is called before the first frame update
    void Awake()
    {
        player = playerObject.GetComponent<PlayerAction>();
        npc = gameObject.GetComponent<NPCAction>();
    }

    public override void OnEpisodeBegin()
    {
        // ���ο� ���Ǽҵ� ����

        // �÷��̾� ����
        player.resetPlayer();
        transform.position = new Vector3(0, 0, 0);
        Debug.Log("���Ǽҵ� ����");

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // �÷��̾��� hp�� ����
        sensor.AddObservation(player.curPlayerHp);
        sensor.AddObservation(player.maxPlayerHp);

        // npc�� �� ��Ÿ���� ����
        sensor.AddObservation(npc.coolTime);
        sensor.AddObservation(npc.filledTime);

        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.DiscreteActions[0]);
        AgentAction(actions.DiscreteActions);

        if (player.isDead)
        {
            SetReward(-1f);
            Debug.Log("endepisode");
            EndEpisode();
        }
        
    }

    public void AgentAction(ActionSegment<int> act)
    {
        var isHeal = act[0];
        var forward = act[1];
        if (isHeal == 1)
        {
            //Debug.Log("?");
            float prePlayerHp = player.curPlayerHp;
            if (npc.Healing(npc.defaultHealValue))
            {
                //if (prePlayerHp + 5 - player.maxPlayerHp > 0) AddReward(0.1f);
                //else AddReward(1f);
                // ������ ȸ���� ����ŭ ����
                // ����������ŭ ����
                //AddReward(0.001f);
            }
            else // ��Ÿ�� �� �� �� �õ��ϸ� ����
            {
               // AddReward(-0.001f);
            }
            
            

        }
        //AddReward(-0.0001f);
        if(forward == 1)
        {
            transform.Translate(new Vector3(2f* Time.deltaTime, 0, 0));
            AddReward(-0.001f);
        }
        else if (forward == 2)
        {
            transform.Translate(new Vector3(-2f * Time.deltaTime, 0, 0));
            AddReward(0.001f);
        }

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut.Clear();
        if (Input.GetKey(KeyCode.C))
        {
            discreteActionsOut[0] = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[1] = 1;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[1] = 2;
        }


    }
}
