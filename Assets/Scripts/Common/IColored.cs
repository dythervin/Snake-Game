using Sirenix.OdinInspector;

public interface IColored
{
    [ShowInInspector, ReadOnly]
    int ColorId { get; set; }
}
