using System;
using System.Threading.Tasks;

namespace BitShifter.Shared.Abstractions.EfCore.Seeder
{
    public interface ISeeder
    {
        Task SeedSampleData(IServiceProvider serviceProvider);
    }
}
