using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{

    private float Speed = 5f;

    private const float RotateSpeed = 720f;

    private Rigidbody rb;

    //[SerializeField]
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // �p�X�ŃJ�������Q�Ƃ��ď����擾���Ă���B
        camera = GameObject.Find("/Camera");

    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�{�[�h���͂�i�s�����̃x�N�g���ɕϊ����ĕԂ�
        Vector3 direction = InputToDirection();

        UpdatePosition(direction);
    }

    private Vector3 InputToDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0f, z);

        return direction.normalized;
    }

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
}
