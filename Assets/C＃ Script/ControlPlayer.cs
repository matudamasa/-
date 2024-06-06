using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{

    private float Speed = 5f;
    private const float RotateSpeed = 720f;
    private Rigidbody rb;

    // �e�̔��˕ϐ�
    //[SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    private float shotSpeed = 1500;
    [SerializeField]
    private int shotCount = 30;
    private float shotInterval;

    private new GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //bulletPrefab = GameObject.Find("Bullet");


        // �p�X�ŃJ�������Q�Ƃ��ď����擾���Ă���B
        camera = GameObject.Find("/Camera");
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�{�[�h���͂�i�s�����̃x�N�g���ɕϊ����ĕԂ�
        Vector3 direction = InputToDirection();

        // �ړ����������߂�
        UpdatePosition(direction);

        // �e�̔���
        ShotBullet();
    }

    // �ړ�
    private Vector3 InputToDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0f, z);

        return direction.normalized;
    }

    // �J�����̌�������ړ�����������
    private void UpdatePosition(Vector3 direction)
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * direction.z + camera.transform.right * direction.x;
        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂�
        rb.velocity = moveForward * Speed + new Vector3(0, rb.velocity.y, 0);

        // �L�[��a�͂ɂ��ړ����������܂��Ă���ꍇ�ɂ́A�L�����N�^�[�̌�����i�s�����ɍ��킹��
        if (moveForward != Vector3.zero)
        {
            Quaternion from = transform.rotation;
            transform.rotation = Quaternion.RotateTowards(from, Quaternion.LookRotation(moveForward),
                RotateSpeed * Time.deltaTime);
        }
    }

    // �e�̔��ˏ���
    private void ShotBullet()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            shotInterval += 0.5f;

            if (shotInterval % 5 == 0 && shotCount > 0)
            {
                shotCount -= 1;

                GameObject bullet = Instantiate(bulletPrefab,
                    transform.position, Quaternion.Euler(transform.parent.eulerAngles.x,
                    transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);

                Destroy(bullet, 3.0f);
            }

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;
        }
    }


}