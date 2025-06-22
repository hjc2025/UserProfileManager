namespace UserProfileManager.Strategies
{
    public interface ISavingStrategyFactory
    {
        ISavingStrategy GetStrategy(string strategyName);
        IEnumerable<string> GetAvailableStrategyNames();
    }
}