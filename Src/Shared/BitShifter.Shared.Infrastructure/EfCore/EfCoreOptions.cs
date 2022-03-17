namespace BitShifter.Shared.Infrastructure.EfCore
{
    public class EfCoreOptions
    {
        public string ConnectionString { get; set; }
        public bool UseInMemoryDatabase { get; set; }
    }
}