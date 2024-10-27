using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Laser_Trap : interactableObject
{
    [SerializeField] private GameObject laserPre;
    GameObject laser;
    [SerializeField] private float maxCheckDistance;
    public float laserLength = 5f; // 레이저 길이
    private RaycastHit hitInfo;
    public LayerMask layerMask;
    private void Start()
    {
        laser = Instantiate(laserPre, transform.position, Quaternion.identity);
        laser.transform.SetParent(gameObject.transform);
        laser.transform.localPosition = new Vector3(0, 10f, 0); 
        laser.transform.rotation = Quaternion.Euler(0, 0, 90);
        laser.transform.localScale = new Vector3(0.1f, 10, 0.1f);
        // 레이저 오브젝트 생성

    }
}
