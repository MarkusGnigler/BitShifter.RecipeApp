using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Infrastructure.Persistence.Seeding
{
    internal static class RecipeSeeder
    {
        private static string _preparation = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar sapien et ligula ullamcorper. Praesent elementum facilisis leo vel fringilla est. Volutpat commodo sed egestas egestas fringilla phasellus faucibus scelerisque. Posuere sollicitudin aliquam ultrices sagittis orci a scelerisque. Adipiscing vitae proin sagittis nisl rhoncus mattis rhoncus urna neque. Nascetur ridiculus mus mauris vitae ultricies leo integer malesuada nunc. A pellentesque sit amet porttitor eget dolor morbi non arcu. Semper risus in hendrerit gravida rutrum quisque non. Rutrum quisque non tellus orci ac.

Nec nam aliquam sem et tortor consequat id.Nisl vel pretium lectus quam id leo. Eu ultrices vitae auctor eu augue ut lectus arcu.Maecenas volutpat blandit aliquam etiam erat velit scelerisque. Nulla pharetra diam sit amet nisl. Adipiscing commodo elit at imperdiet.Rhoncus urna neque viverra justo.Et magnis dis parturient montes nascetur ridiculus mus mauris vitae. Non quam lacus suspendisse faucibus interdum. Orci a scelerisque purus semper eget duis at. Venenatis tellus in metus vulputate eu scelerisque felis imperdiet. Diam maecenas ultricies mi eget mauris.

Adipiscing elit pellentesque habitant morbi tristique senectus.Ut tristique et egestas quis ipsum. Eleifend donec pretium vulputate sapien nec sagittis.Eget nulla facilisi etiam dignissim diam. Massa enim nec dui nunc mattis enim ut. Cursus vitae congue mauris rhoncus aenean vel elit. Urna et pharetra pharetra massa massa ultricies.Libero nunc consequat interdum varius sit amet.Nisl nunc mi ipsum faucibus vitae aliquet nec ullamcorper.Vitae tempus quam pellentesque nec nam aliquam sem et tortor. Nulla aliquet enim tortor at auctor urna.Justo donec enim diam vulputate ut pharetra sit. Dolor morbi non arcu risus quis varius.Aenean et tortor at risus viverra adipiscing at. Et molestie ac feugiat sed lectus vestibulum mattis. Nunc mattis enim ut tellus elementum. Molestie a iaculis at erat pellentesque adipiscing commodo elit.

Mi in nulla posuere sollicitudin.Interdum posuere lorem ipsum dolor sit amet consectetur. Purus semper eget duis at tellus. Varius vel pharetra vel turpis nunc eget.Vitae et leo duis ut.Lectus magna fringilla urna porttitor rhoncus. Placerat duis ultricies lacus sed turpis tincidunt id. Ultricies leo integer malesuada nunc vel risus commodo. Auctor augue mauris augue neque gravida in fermentum et. Phasellus faucibus scelerisque eleifend donec pretium. Viverra ipsum nunc aliquet bibendum.Aenean sed adipiscing diam donec adipiscing tristique risus. Quis hendrerit dolor magna eget est lorem ipsum dolor.Pellentesque elit eget gravida cum sociis. Est pellentesque elit ullamcorper dignissim cras. Ut morbi tincidunt augue interdum velit euismod.Quis risus sed vulputate odio ut enim blandit volutpat.Massa sed elementum tempus egestas sed. Mattis rhoncus urna neque viverra justo nec ultrices dui.Cras fermentum odio eu feugiat pretium nibh ipsum.

Amet consectetur adipiscing elit ut aliquam purus sit. Et malesuada fames ac turpis egestas integer eget aliquet nibh. Magna eget est lorem ipsum dolor sit amet consectetur adipiscing. Neque sodales ut etiam sit amet nisl purus in mollis.Et tortor consequat id porta nibh venenatis cras. Id venenatis a condimentum vitae sapien pellentesque habitant morbi.Amet consectetur adipiscing elit pellentesque habitant. Tortor at auctor urna nunc.In hac habitasse platea dictumst quisque sagittis purus sit.Amet volutpat consequat mauris nunc.Et sollicitudin ac orci phasellus egestas tellus rutrum. Erat imperdiet sed euismod nisi porta lorem.Feugiat pretium nibh ipsum consequat nisl vel.Volutpat est velit egestas dui id ornare arcu odio.Tempus egestas sed sed risus pretium quam vulputate. Nulla aliquet enim tortor at auctor urna nunc. Tortor id aliquet lectus proin nibh nisl condimentum id.Purus in massa tempor nec feugiat nisl.Egestas congue quisque egestas diam in.";

        private static string _description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ligula ullamcorper malesuada proin libero nunc consequat interdum varius. Scelerisque in dictum non consectetur a erat. Sed egestas egestas fringilla phasellus faucibus scelerisque eleifend. Orci nulla pellentesque dignissim enim sit. Et ultrices neque ornare aenean euismod elementum. Vitae auctor eu augue ut. Vulputate odio ut enim blandit volutpat. Vestibulum lectus mauris ultrices eros in cursus turpis massa. Risus feugiat in ante metus dictum at tempor commodo ullamcorper. Pretium lectus quam id leo in vitae turpis. Vestibulum sed arcu non odio euismod lacinia at quis. Diam quam nulla porttitor massa id neque aliquam. Tincidunt arcu non sodales neque. Pretium lectus quam id leo in vitae. Blandit cursus risus at ultrices mi. Ultricies mi eget mauris pharetra et ultrices. Feugiat pretium nibh ipsum consequat nisl. In arcu cursus euismod quis viverra.

Sapien eget mi proin sed libero enim sed faucibus turpis. Neque sodales ut etiam sit. Donec adipiscing tristique risus nec feugiat in fermentum posuere urna. Nunc aliquet bibendum enim facilisis gravida. Nulla facilisi etiam dignissim diam quis. Sed euismod nisi porta lorem mollis aliquam ut porttitor leo. Sit amet nisl purus in mollis nunc sed. Blandit libero volutpat sed cras ornare. Quis vel eros donec ac odio tempor. Praesent semper feugiat nibh sed.

Vitae turpis massa sed elementum tempus egestas sed sed risus. Volutpat consequat mauris nunc congue nisi. Ipsum nunc aliquet bibendum enim facilisis gravida neque convallis. Volutpat maecenas volutpat blandit aliquam etiam erat. Neque ornare aenean euismod elementum nisi. Ipsum dolor sit amet consectetur adipiscing elit pellentesque habitant morbi. Quis risus sed vulputate odio ut enim blandit. Pulvinar proin gravida hendrerit lectus. Blandit volutpat maecenas volutpat blandit aliquam etiam erat velit scelerisque. In nulla posuere sollicitudin aliquam ultrices. Sit amet tellus cras adipiscing enim. Cursus metus aliquam eleifend mi in nulla posuere. Felis bibendum ut tristique et egestas quis ipsum suspendisse ultrices.

Integer quis auctor elit sed vulputate mi. Et tortor consequat id porta nibh. Nisl nisi scelerisque eu ultrices vitae. Enim nulla aliquet porttitor lacus luctus accumsan tortor posuere. Aliquet bibendum enim facilisis gravida neque convallis a cras. Justo eget magna fermentum iaculis eu non diam phasellus. Suspendisse interdum consectetur libero id. Pellentesque nec nam aliquam sem et tortor consequat id. Urna et pharetra pharetra massa massa ultricies. Est lorem ipsum dolor sit amet consectetur adipiscing elit.

Hac habitasse platea dictumst vestibulum rhoncus est pellentesque elit ullamcorper. Volutpat lacus laoreet non curabitur gravida arcu ac tortor. Porttitor lacus luctus accumsan tortor posuere ac ut consequat. Senectus et netus et malesuada. Ornare arcu dui vivamus arcu felis. Faucibus ornare suspendisse sed nisi. Sapien pellentesque habitant morbi tristique. Scelerisque felis imperdiet proin fermentum. Elit scelerisque mauris pellentesque pulvinar pellentesque habitant morbi tristique senectus. Nec sagittis aliquam malesuada bibendum. Tincidunt nunc pulvinar sapien et ligula ullamcorper malesuada proin. Augue mauris augue neque gravida in fermentum et sollicitudin. Aliquet sagittis id consectetur purus ut faucibus. Id porta nibh venenatis cras.";

        public async static Task SeedRecipe(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (context.Recipes.Any()) return;

            var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == CategorySeeder.CategoryToSeed);

            var recipe = new Recipe("test-rezept", "Test Rezept", "test-recipe.jpg", _preparation, _description, category);

            recipe.InsertIngredient("Ingredient", 5.0, "liter");

            await context.Recipes.AddAsync(recipe);

            await context.SaveChangesAsync();
        }
    }
}
