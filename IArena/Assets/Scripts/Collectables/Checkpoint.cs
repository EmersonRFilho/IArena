using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;

public class Checkpoint : Collectable
{

    private bool activated = false;
    [SerializeField] private SpriteRenderer sprRenderer;
    // [SerializeField] private Collider2D col;

    protected override void Awake() {
        base.Awake();
        setType(CollectableType.other);
    }

    public override void Collect(CollectCommand _command)
    {
        if(!_command.Item.GetComponent<Collider2D>().IsTouching(_command.Self.GetComponent<Collider2D>())) return;
        sprRenderer.color = Color.gray;
        hitbox.enabled = false;
        activated = true;
    }

    public override void Drop(CollectCommand _command)
    {
        sprRenderer.color = Color.white;
        hitbox.enabled = true;
        activated = false;
    }
}
