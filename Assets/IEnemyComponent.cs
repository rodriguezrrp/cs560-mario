using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyComponent
{
    // enemies will have this function
    // that can be called by the player
    // as an event to deal damage, etc. to the enemy
    void OnWeakAreaHit();

    // change default target
    // helpful for the instantiators
    void SetDefaultTarget(GameObject targetObj);
}
