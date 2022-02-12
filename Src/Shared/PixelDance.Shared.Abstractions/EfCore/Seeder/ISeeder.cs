using System;
using System.Threading.Tasks;

namespace PixelDance.Shared.Abstractions.EfCore.Seeder
{
    public interface ISeeder
    {
        Task SeedSampleData(IServiceProvider serviceProvider);
    }
}
