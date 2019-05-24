using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inhabitant : MonoBehaviour
{
    public enum TEAM { TOURIST, INHABITANT, STAFF, ANIMAL };
    public TEAM team;
    public InhabitantSheet sheet;

    NavMeshAgent navMeshAgent;
    GameObject visual;

    private void Awake()
    {
        if (sheet == null) throw new MissingComponentException("Inhabitant has no sheet : " + name);

        navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.areaMask = sheet.navMeshAreaMask;
        navMeshAgent.speed = sheet.walkingSpeed;
        navMeshAgent.angularSpeed = 720f;
        navMeshAgent.acceleration = 50f;
        visual = Instantiate(sheet.appearance.FBXPrefab, transform);
        visual.transform.localPosition = sheet.appearance.position;
        visual.transform.localEulerAngles = sheet.appearance.eulers;
        visual.transform.localScale = sheet.appearance.scale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /// debug
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                navMeshAgent.SetDestination(hit.point);
            }

        }
    }
}
