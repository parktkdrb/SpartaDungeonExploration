using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Platform : MonoBehaviour
{
    [SerializeField] private float jumpPower; 
    /*
    - **OnCollisionEnter** 트리거를 사용해 캐릭터가 점프대에 닿았을 때 **ForceMode.Impulse**를 사용해 순간적인 힘을 가함.
    */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == CharacterManager.Instance.Player.gameObject)
        {
            Rigidbody _rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
}
