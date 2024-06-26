using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject shellPrefab;
    private int count;

    float moveEnemy = TrackingPlayer.moveNow;   // 動いているとき(1) || 止まっているとき(0)

    void Update()
    {
        count += 1;

        if (TrackingPlayer.moveNow == 1)
        {

            // （ポイント）
            // ６０フレームごとに砲弾を発射する
            if (count % 360 == 0)
            {
                GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
                Rigidbody shellRb = shell.GetComponent<Rigidbody>();

                // 弾速は自由に設定
                shellRb.AddForce(transform.forward * 500);


                // 1秒後に砲弾を破壊する
                Destroy(shell, 1.0f);
            }
        }
        else
        {
            TrackingPlayer.moveNow = 0;
        }
    }
}