using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

/* How to use this
You may make a copy of this file and call it a different name.
This file serves as a Minimum structure for how you can implement your character's Behaviour
You can implement whatever you want in this file as long as
you don't use any library linked to the projects systems, which are:
    - Core
    - Managers
    - Commands
    - LevelElements
You also absolutely cannot use the SendMessage method anywhere in your code.

Please refer to the documentation for available actions and movement behaviours that can be implemented.
*/
public class ExampleAI : BrainBase
{
    Transform target;

    // Update is called once per frame
    /*
    GetVision() populates a list of Transform called objectsInRange
    You can check a Transform's tag by using <Transform>.tag where <Transform> is the name of the variable.
    You can also check a Transform's layer by using LayerMask.NameToLayer("LayerName")
    In order to use that, you need to make a comparison to the Transform's gameobject.layer
    for instance: target.gameObject.layer == LayerMask.NameToLayer("LayerName")
    You can check available Tags and Layers with the documentation.
    */
    void Update()
    {
        GetVision();
        print(objectsInRange.Count);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
