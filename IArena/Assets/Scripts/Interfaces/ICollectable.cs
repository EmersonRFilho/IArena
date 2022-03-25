using Commands;
public interface ICollectable
{
    void Collect(CollectCommand _command);
    void Drop(CollectCommand _command);
}