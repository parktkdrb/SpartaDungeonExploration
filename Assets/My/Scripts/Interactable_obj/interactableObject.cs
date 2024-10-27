using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableObject : MonoBehaviour, IInteractable
{
    [SerializeField]public InteractableItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }
    public virtual void OnInteract()
    {
        if (!data.Use)
        {
            StartCoroutine(UseCooltime(data.CoolDown));
        }
    }
    protected virtual IEnumerator UseCooltime(float _coolDown)
    {
        data.Use = true;
        yield return new WaitForSeconds(_coolDown);
        data.Use = false;

    }
}
