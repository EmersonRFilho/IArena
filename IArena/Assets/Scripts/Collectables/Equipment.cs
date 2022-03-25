using BaseCharacter;
using Commands;

public class Equipment : Collectable
{

    public override void Collect(CollectCommand _command)
    {
        gameObject.SetActive(false);
    }

    public override void Drop(CollectCommand _command)
    {
        transform.position = _command.Self.transform.position;
        gameObject.SetActive(true);
    }
}