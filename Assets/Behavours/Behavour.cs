using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BehavourStatus
{
    WORKING,
    DONE,
}

public interface IBehavour {

    IEnumerable<BehavourStatus> Behave();

}
