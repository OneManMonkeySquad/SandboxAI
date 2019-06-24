using SandboxAI;
using SandboxAI.HTN;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    public HTNGraph graph;

    public Transform backpackSocket;
    public GameObject backpackItem;
    public List<GameObject> foodInProximity = new List<GameObject>();
    public List<GameObject> robotsInProximity = new List<GameObject>();
    public List<GameObject> shotsInProximity = new List<GameObject>();

    RobotState _state;
    Collider[] scanColliders = new Collider[64];

    public bool AddItemToBackpack(GameObject item) {
        if (backpackItem != null)
            return false;
        
        backpackItem = item;
        backpackItem.transform.parent = backpackSocket;
        backpackItem.transform.localPosition = Vector3.zero;
        backpackItem.transform.localRotation = Quaternion.identity;
        return true;
    }

    void Start() {
        var navigation = GetComponent<IAgentNavigation>();
        var agent = new HTNAgent(navigation, graph);

        _state = new RobotState(agent, this) {
        };
    }

    void Update() {
        UpdateContext();

        if (_state.target != null) {
            Debug.DrawLine(transform.position, _state.target.transform.position, Color.red, 0, false);
        }

        _state.agent.UpdateAgent(_state);
    }

    readonly float sensorRange = 28;

    void UpdateContext() {
        foodInProximity.Clear();
        robotsInProximity.Clear();
        shotsInProximity.Clear();

        var numHits = Physics.OverlapSphereNonAlloc(transform.position, sensorRange, scanColliders);
        for (int i = 0; i < numHits; ++i) {
            var collider = scanColliders[i];

            var food = collider.GetComponent<Food>();
            if (food != null && food.gameObject != backpackItem) {
                foodInProximity.Add(food.gameObject);
            }

            var robot = collider.GetComponent<Robot>();
            if (robot != null && robot != this) {
                var thisToOther = robot.transform.position - transform.position;
                thisToOther.Normalize();

                var canSeeRobot = !Physics.Linecast(transform.position + thisToOther * 0.6f, robot.transform.position - thisToOther * 0.6f);
                if (canSeeRobot) {
                    robotsInProximity.Add(robot.gameObject);
                }
            }

            var shot = collider.GetComponent<Shot>();
            if (shot != null && shot != this) {
                shotsInProximity.Add(shot.gameObject);
            }
        }
    }
}
