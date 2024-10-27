using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public GameObject startObject; // ���� ��ġ�� ���� GameObject
    public GameObject endObject;   // �� ��ġ�� ���� GameObject
    public float speed = 1.0f;     // �̵� �ӵ�
    private float journeyLength;    // ��ü �̵� �Ÿ�
    private float startTime;        // ���� �ð�
    float fractionOfJourney;
    void Start()
    {
        // �ʱ� ��ġ ����
        transform.position = startObject.transform.position;
        journeyLength = Vector3.Distance(startObject.transform.position, endObject.transform.position); // ��ü �Ÿ� ���
        startTime = Time.time; // ���� �ð��� ���� �ð����� ����
    }

    void Update()
    {
        // ���� �ð��� ���� ���� ���� ���
        float distanceCovered = (Time.time - startTime) * speed;
        fractionOfJourney = distanceCovered / journeyLength;

        // ��ŸƮ �����ǿ��� ���� ���������� �ε巴�� �̵�
        transform.position = Vector3.Lerp(startObject.transform.position, endObject.transform.position, fractionOfJourney);

        // �����ϸ� �̵��� ����
        if (fractionOfJourney >= 1.0f)
        {
            enabled = false; // Update() ����
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == CharacterManager.Instance.Player.gameObject)
        {
            collision.gameObject.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == CharacterManager.Instance.Player.gameObject)
        {
            collision.gameObject.transform.SetParent(null, true);
        }
    }
}
