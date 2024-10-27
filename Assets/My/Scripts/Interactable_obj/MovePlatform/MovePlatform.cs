using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public GameObject startObject; // 시작 위치를 위한 GameObject
    public GameObject endObject;   // 끝 위치를 위한 GameObject
    public float speed = 1.0f;     // 이동 속도
    private float journeyLength;    // 전체 이동 거리
    private float startTime;        // 시작 시간
    float fractionOfJourney;
    void Start()
    {
        // 초기 위치 설정
        transform.position = startObject.transform.position;
        journeyLength = Vector3.Distance(startObject.transform.position, endObject.transform.position); // 전체 거리 계산
        startTime = Time.time; // 현재 시간을 시작 시간으로 설정
    }

    void Update()
    {
        // 현재 시간에 따라 진행 비율 계산
        float distanceCovered = (Time.time - startTime) * speed;
        fractionOfJourney = distanceCovered / journeyLength;

        // 스타트 포지션에서 엔드 포지션으로 부드럽게 이동
        transform.position = Vector3.Lerp(startObject.transform.position, endObject.transform.position, fractionOfJourney);

        // 도착하면 이동을 멈춤
        if (fractionOfJourney >= 1.0f)
        {
            enabled = false; // Update() 멈춤
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
