namespace UserProfileManager.Strategies
{
    // The Factory's job is to know about all available strategies and provide
    // the requested one. It's a central point of management for our strategies.
    public class SavingStrategyFactory : ISavingStrategyFactory
    {
        private readonly IReadOnlyDictionary<string, ISavingStrategy> _strategies;

        // This is a key DI pattern: we inject an IEnumerable of the interface type.
        // The DI container will automatically find all registered implementations
        // of ISavingStrategy and pass them in as a collection.
        public SavingStrategyFactory(IEnumerable<ISavingStrategy> strategies)
        {
            // We convert the collection to a dictionary for fast lookups by name.
            _strategies = strategies.ToDictionary(s => s.StrategyName, s => s);
        }

        public ISavingStrategy GetStrategy(string strategyName)
        {
            if (_strategies.TryGetValue(strategyName, out var strategy))
            {
                return strategy;
            }
            throw new ArgumentException($"Unknown strategy: {strategyName}", nameof(strategyName));
        }

        public IEnumerable<string> GetAvailableStrategyNames()
        {
            return _strategies.Keys.OrderBy(name => name);
        }
    }
}