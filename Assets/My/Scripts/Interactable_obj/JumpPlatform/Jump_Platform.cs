using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Platform : interactableObject
{
    [SerializeField] private float jumpPower;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == CharacterManager.Instance.Player.gameObject && !data.Use)
        {
            StartCoroutine(UseCooltime(data.CoolDown));
            Rigidbody _rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

}
