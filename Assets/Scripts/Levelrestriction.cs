using System;
using UnityEngine;

public class Levelrestriction : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraVM;

    private Vector3 oldPosition;

    private void Start()
    {
        oldPosition = cameraVM.transform.position;
    }

    private void Update()
    {
        if(cameraVM.transform.position.x > oldPosition.x)
        {
            gameObject.transform.position += new Vector3(Math.Abs(cameraVM.transform.position.x - oldPosition.x), 0, 0);
            oldPosition.x = cameraVM.transform.position.x;
        }
    }
}
