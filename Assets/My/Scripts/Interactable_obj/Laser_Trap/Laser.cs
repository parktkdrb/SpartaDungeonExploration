using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] public int Damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == CharacterManager.Instance.Player.gameObject)
        {
            CharacterManager.Instance.Player.condition.TakePgsicalDamage(Damage);
            StartCoroutine(UseCooltime());
        }
    }
    protected IEnumerator UseCooltime()
    {
        for (int i = 0; i < 2; i++)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}
