using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Platform : MonoBehaviour
{
    [SerializeField] private float jumpPower; 
    /*
    - **OnCollisionEnter** Ʈ���Ÿ� ����� ĳ���Ͱ� �����뿡 ����� �� **ForceMode.Impulse**�� ����� �������� ���� ����.
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
