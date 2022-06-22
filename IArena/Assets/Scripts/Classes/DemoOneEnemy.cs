using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

public class DemoOneEnemy : BrainBase
{
    CharacterBehaviors player;

    // Update is called once per frame
    void Update()
    {
        GetVision();
        EnemyRoutine();
    }

    void EnemyRoutine()
    {
        // Find player in range
        if(objectsInRange.Find(x => x.tag == "Player"))
        {
            player = objectsInRange.Find(x => x.tag == "Player").GetComponent<CharacterBehaviors>();
        }
        // When player gets in weapon range
        if (player && Vector2.Distance(transform.position, player.transform.position) <= chara.Weapon.Range)
        {
            // Attack player
            Attack(player);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Weapon") {
            Collect(other.GetComponent<Weapon>());
        }
    }
}
