using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngineInternal;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    public float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    public IInteractable curinteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curinteractable = hit.collider.GetComponent<IInteractable>();
                    //프롬프트에 출력해줘라.
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curinteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }

    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curinteractable.GetInteractPrompt();
    }
    public void OnInteractinput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curinteractable != null)
        {
            curinteractable.OnInteract();
            curInteractGameObject = null;
            curinteractable = null;
            promptText.gameObject.SetActive(false); 
        }
    }
}
