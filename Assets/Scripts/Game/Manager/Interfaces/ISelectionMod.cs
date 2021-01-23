namespace RTSEngine.Manager
{
    public interface ISelectionMod<T, ST>
    {
        bool Active { get; set; }
        ST Type { get; set; }

        ISelectionArgsXP<T, ST> Apply(ISelectionArgsXP<T, ST> args);
    }
}
