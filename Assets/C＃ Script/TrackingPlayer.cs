using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrackingPlayer : MonoBehaviour
{

    public GameObject goal; //これにプレイヤーを格納
    public NavMeshAgent agent; //�@敵が自動で動くために必要
    public float distance; //�Aプレイヤーと敵の距離を格納する変数(distane=距離)

    public static float moveNow = 0;   // 動いているとき1 || 止まっているとき0

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();　//�@
        goal = GameObject.Find("Cara");
        Debug.Log(moveNow); // 動いているか
    }

    // Update is called once per frame
    void Update()
    {

        //�A二者間の距離を計算してfloat　一定値いかになれば追跡する
        distance = Vector3.Distance(transform.position, goal.transform.position);
        Debug.Log(distance);

        if (distance < 20)
        {
            moveNow = 1;    
            agent.destination = goal.transform.position; //�@
        }
        else {
            moveNow = 0;
        }

   
    }
}
