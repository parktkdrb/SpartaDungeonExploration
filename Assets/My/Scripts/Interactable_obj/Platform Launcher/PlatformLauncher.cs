using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLauncher : interactableObject
{
    [SerializeField] private float LaunchePower;

    private void OnCollisionEnter(Collision collision)
    {
        LaunchePower = data.InteractionPower;
        if (collision.gameObject == CharacterManager.Instance.Player.gameObject)
        {
            Debug.Log("발사대 충돌");
            StartCoroutine(Use(collision));
        }
    }
    protected virtual IEnumerator Use(Collision collision)
    {
       yield return new WaitForSeconds(2f);//
        Rigidbody _rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        _rigidbody.AddForce(Vector3.up * LaunchePower, ForceMode.Impulse);
    }
}
