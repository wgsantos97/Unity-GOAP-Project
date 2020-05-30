using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace GameDevLibrary.AI.GOAP
{
    public class WInterface : MonoBehaviour
    {
        [SerializeField] GameObject[] allResources = new GameObject[0];
        [SerializeField] NavMeshSurface surface = null;
        [SerializeField] Transform hospital = null;

        GameObject target = null;
        ResourceData targetData = null;
        GameObject newResourcePrefab = null;

        Vector3 goalPos = Vector3.zero;
        Vector3 clickOffset = Vector3.zero;
        
        bool offsetCalc = false;
        bool deleteResource = false;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!AddSelect()) return;
            }
            else if (target && Input.GetMouseButtonUp(0))
            {
                Drop();
            } 
            else if (target && Input.GetMouseButton(0))
            {
                if (!Drag()) return;
            }
            Rotate();
            
        }

        public void MouseOnHoverTrash()
        {
            deleteResource = true;
        }

        public void MouseOutHoverTrash()
        {
            deleteResource = false;
        }

        public void ActivateToilet()
        {
            newResourcePrefab = allResources[0];
        }

        public void ActivateCubicle()
        {
            newResourcePrefab = allResources[1];
        }

        bool AddSelect()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return false;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit)) return false;

            offsetCalc = false;
            clickOffset = Vector3.zero;

            Resource r = hit.transform.gameObject.GetComponent<Resource>();
            if(r != null)
            {
                target = hit.transform.gameObject;
                targetData = r.Data;
            }
            else if(newResourcePrefab != null)
            {
                goalPos = hit.point;
                target = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
                targetData = target.GetComponent<Resource>().Data;
            }

            if(target) target.GetComponent<Collider>().enabled = false;
            return true;
        }

        void Drop()
        {
            if (deleteResource == true)
            {
                DeleteTarget();
            }
            else
            {
                target.transform.parent = hospital;
                GWorld.Instance.GetQueue(targetData.ResourceQueue).AddResource(target);
                GWorld.Instance.worldStates.ModifyState(targetData.ResourceState, 1);
                target.GetComponent<Collider>().enabled = true;
            }
            surface.BuildNavMesh();
            target = null;
        }

        bool Drag()
        {
            int layerMask = 1 << 8;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) return false;
            if (!offsetCalc)
            {
                clickOffset = hit.point - target.transform.position;
                offsetCalc = true;
            }
            goalPos = hit.point - clickOffset;
            target.transform.position = goalPos;
            return true;
        }

        void Rotate()
        {
            if (target && Input.GetKeyDown(KeyCode.Z))
            {
                target.transform.Rotate(0, -90, 0);
            }
            if (target && Input.GetKeyDown(KeyCode.C))
            {
                target.transform.Rotate(0, 90, 0);
            }
        }

        void DeleteTarget()
        {
            GWorld.Instance.GetQueue(targetData.ResourceQueue).RemoveResource(target);
            GWorld.Instance.worldStates.ModifyState(targetData.ResourceState, -1);
            Destroy(target);
        }
    }
}
