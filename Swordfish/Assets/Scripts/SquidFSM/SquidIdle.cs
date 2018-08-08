using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidIdle : SquidBaseFSM
{
    float x, y;
    // How many cicles will it have to wait in order to change diretion.
    // Decided at random between min and max values.
    public int minWaitCycles = 100;
    public int maxWaitCycles = 100;
    int waitCycles;

    private void GetNewDiretion()
    {
        waitCycles = Random.Range(minWaitCycles, maxWaitCycles);
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);        
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        waitCycles = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waitCycles == 0)
        {
            controller.Impulse();
            GetNewDiretion();
        }

        Vector3 dir = new Vector3(x, y);
        squid.transform.up = Vector3.Slerp(squid.transform.up, (squid.transform.position + dir) - squid.transform.position, rotationSpeed * Time.deltaTime);        

        waitCycles--;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
