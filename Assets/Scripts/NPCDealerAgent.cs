using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class NPCDealerAgent : Agent
{
    GameObject scanObject;

    public GameObject playerObject;
    public GameObject bossObject;
    NPCDealerAction npc;
    PlayerAction player;
    BossAction boss;
    int directionY = 0;
    int directionX = 0;
    Transform playerTransform;
    Vector3 dirVec;
    SpriteRenderer sprite;
    Rigidbody2D rigid;
    public float velocity;
   // int _healCnt;
    // Start is called before the first frame update
    void Awake()
    {
        player = playerObject.GetComponent<PlayerAction>();
        playerTransform = playerObject.GetComponent<Transform>();
        npc = gameObject.GetComponent<NPCDealerAction>();
        boss = bossObject.GetComponent<BossAction>();
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        // Object 레이어만 스캔
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.5f, LayerMask.GetMask("Object"));
        if (rayHit.collider)
        {
            scanObject = rayHit.collider.gameObject;
            //Debug.Log(scanObject);
        }
        else scanObject = null;
    }

    public override void OnEpisodeBegin()
    {
        // 새로운 에피소드 시작

        // 플레이어 리셋
        //player.resetPlayer();
        //npc.isBorder = false;
        //GameManager.GM.resetManager();
        npc.isDead = false;
        npc.curNPCHp = npc.maxNPCHp;
        float x = Random.Range(-8.0f, 8.0f);
        float y = Random.Range(-4.0f, 5.0f);
        transform.localPosition = new Vector3(x, y, 0);
        //_healCnt = 0;
        //Debug.Log("에피소드 시작");


    }


    public override void CollectObservations(VectorSensor sensor)
    {
        // 플레이어의 hp를 관찰
        // sensor.AddObservation(player.curPlayerHp);
        //sensor.AddObservation(npc.curNPCHp);
        //sensor.AddObservation(npc.curNPCHp);

        //npc.distance = Vector2.Distance(transform.position, player.GetComponent<Transform>().position);
        //sensor.AddObservation(npc.distance);
        //sensor.AddObservation(player.transform.localPosition);
        //sensor.AddObservation(boss.transform.localPosition);
        //sensor.AddObservation(player.rigid.velocity);
        //sensor.AddObservation(rigid.velocity);

        // npc의 힐 쿨타임을 관찰
        //sensor.AddObservation(npc.coolTime);
        //sensor.AddObservation(npc.filledTime);

        //sensor.AddObservation(transform.localPosition);
        //Debug.Log(player.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.DiscreteActions[0]);
        
        AgentAction(actions.DiscreteActions);

        if (player.isDead)
        {
            //Debug.Log("player dead");
            AddReward(-1f);
            EndEpisode();
        }

        if (npc.isDead)
        {
            //Debug.Log("npc dead");
            AddReward(-1f);
            EndEpisode();
        }

        if (boss.isDead)
        {
            SetReward(1f);
            EndEpisode();
        }

    }

    public void AgentAction(ActionSegment<int> act)
    {
        var isAttack = act[0];
        var forward = act[1];

        if (isAttack == 1)
        {
            //npc.Healing(npc.defaultHealValue);

            npc.Attack(scanObject);
            
                //_healCnt++;
                //SetReward(1f);
                //EndEpisode();
                //if (_healCnt == 3)
                //{
                //    EndEpisode();
                //}
                //AddReward(10 / StepCount);
                //EndEpisode();

            
        }


        if (forward == 0)
        {
            directionX = 0;
            directionY = 0;
        }
        if (forward == 1)
        {
            directionX = 0;
            directionY = 1;
        }
        else if (forward == 2)
        {
            directionX = 0;
            directionY = -1;
        }
        else if (forward == 3)
        {
            directionX = -1;
            directionY = 0;
        }
        else if (forward == 4)
        {
            directionX = 1;
            directionY = 0;
        }

        if (directionX != 0 || directionY != 0)
        {
            dirVec = new Vector2(directionX, directionY).normalized;
            if (directionX < 0) sprite.flipX = true;
            else sprite.flipX = false;
        }

        rigid.velocity = new Vector2(directionX, directionY).normalized * Time.deltaTime * velocity;


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut.Clear();
        if (Input.GetKey(KeyCode.H))
        {
            discreteActionsOut[0] = 1;
        }


        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("???");
            discreteActionsOut[1] = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[1] = 2;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[1] = 3;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[1] = 4;
        }


    }
}
