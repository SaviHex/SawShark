using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManagerAI : MonoBehaviour
{
    public GameObject unitPrefab;    
    public Unit[] units;

    [Header("Creation")]
    public int unitAmount = 3;
    public float range = 5;

    [Header("Goal")]
    public bool inDanger;
    private Transform goal;
    private Transform player;
    public float stepSize = 30f;
    public float stepDelay = 3f;

    private Vector3 unitPos;
    private Vector3 average;

    void Start()
    {
        goal = new GameObject(name + " Goal").transform;
        player = GameObject.FindGameObjectWithTag("Shark").transform;
        units = new Unit[unitAmount];      

        for (int i = 0; i < unitAmount; i++)
        {
            unitPos = (Vector3)Random.insideUnitCircle * range;
            units[i] = Instantiate(unitPrefab, transform.position + unitPos, Quaternion.identity).GetComponent<Unit>();
            units[i].manager = this;
            units[i].name += " " + i;
        }
                      
        StartCoroutine(ChangeGoal());
    }

    private void OnDrawGizmosSelected()
    {
        if (goal != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(goal.transform.position, stepSize);
        }
    }

    private void Update()
    {
        average = Vector3.zero;

        foreach (Unit u in units)
        {
            average += u.transform.position;
        }

        transform.position = average / units.Length;

        inDanger = (Vector3.Distance(transform.position, player.position) < range);

        if (inDanger)
        {
            goal.position = ((transform.position - player.position) + transform.position);            
            Debug.DrawLine(transform.position, player.position);
            Debug.DrawLine(transform.position, goal.position);
        }
    }

    public Transform GetGoal()
    {
        return goal;
    }

    public void BroadcastDanger()
    {
        foreach (Unit u in units)
        {
            u.SpeedUp();
        }
    }

    private IEnumerator ChangeGoal()
    {
        Vector3 newPos = goal.position;        
        while (!inDanger)
        {                    
            newPos = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1));
            newPos = newPos.normalized * stepSize;
            goal.position += newPos;

            yield return new WaitForSeconds(stepDelay);
        }
    }    
}
