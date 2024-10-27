using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingObj : interactableObject
{

    public override void OnInteract()
    {
        Debug.Log("실행1");
        StartCoroutine(UseCooltime(data.CoolDown));
        StartCoroutine(ScaleDown());
        
    }
    private IEnumerator ScaleDown()
    {
        Debug.Log("실행2");
        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        yield return new WaitForSeconds(10f);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}