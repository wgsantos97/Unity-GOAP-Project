using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP_Demo
{
    public class CameraMover : MonoBehaviour
    {
        Camera cam = null;
        [SerializeField] float transformSpeed = 50f;
        [SerializeField] float scrollSpeed = 300f;
        [SerializeField] float rotationSpeed = 100f;

        void Awake()
        {
            cam = GetComponentInChildren<Camera>();
            cam.gameObject.transform.LookAt(transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            float XTranslation = -1 * Input.GetAxis("Vertical") * transformSpeed * Time.deltaTime;
            float ZTranslation = Input.GetAxis("Horizontal") * transformSpeed * Time.deltaTime;

            transform.Translate(XTranslation, 0, ZTranslation);

            if(Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            if(Input.GetAxis("Mouse ScrollWheel") > 0 && cam.gameObject.transform.position.y > 10f)
            {
                cam.gameObject.transform.Translate(0, 0, scrollSpeed * Time.deltaTime);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && cam.gameObject.transform.position.y < 45f)
            {
                cam.gameObject.transform.Translate(0, 0, -1 * scrollSpeed * Time.deltaTime);
            }
        }
    }
}
