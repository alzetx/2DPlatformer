using Atomic.Elements;

public class RestoreHealthAction : IAtomicAction<IHealingItem>
{
    private readonly HealthData _healthData;
    public RestoreHealthAction(HealthData healthData)
    {
        _healthData = healthData;
    }

    public void Invoke(IHealingItem iHealingItem)
    {
        if (iHealingItem.HealingPoints.Value <= 0 || _healthData.IsHealthFull)
        {
            return;
        }
        _healthData.RestoreHealth(iHealingItem.HealingPoints.Value);
        iHealingItem.PickUpAction?.Invoke();
    }
}