using MVVM;
using UniRx;
using Zenject;

public class PlayerParametersViewModel: ITickable
{
    private readonly Player _player;
    
    [Data("ParameterPositionX")] 
    public readonly ReactiveProperty<string> PositionX = new();
    
    [Data("ParameterPositionY")] 
    public readonly ReactiveProperty<string> PositionY = new();
    
    [Data("ParameterRotation")] 
    public readonly ReactiveProperty<string> Rotation = new();
    
    [Data("ParameterVelocity")] 
    public readonly ReactiveProperty<string> Velocity = new();

    public PlayerParametersViewModel(Player player)
    {
        _player = player;
    }
    
    public void Tick()
    {
        PositionX.Value = $"X: {_player.transform.position.x:0.0};";
        PositionY.Value = $"Y: {_player.transform.position.y:0.0};";
        Rotation.Value = $"R: {_player.transform.rotation.eulerAngles.z:0.0};";
        Velocity.Value = $"V: {_player.Physics.Velocity.magnitude:0.0};";
    }
}
