using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public Transform weaponTransform;
    [SerializeField] float shootSpeed;
    [SerializeField] Vector3 Direction;
    
    [SerializeField] Transform gunRotate;
    // Start is called before the first frame update
    void Start()
    {
        Direction = Vector3.right;
        transform.rotation = gunRotate.rotation;
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * Time.deltaTime * shootSpeed);
        
    }
    public void BulletDestroy(GameObject clone)
    {
        Destroy(clone, 2f);
    }
    
    
}
