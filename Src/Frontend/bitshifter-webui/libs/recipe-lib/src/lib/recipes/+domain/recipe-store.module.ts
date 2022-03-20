import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import * as fromRecipe from './recipe.reducer';
import { EffectsModule } from '@ngrx/effects';
import { RecipeEffects } from './recipe.effects';

const store = [
	StoreModule.forFeature(fromRecipe.recipeFeatureKey, fromRecipe.reducer),
	EffectsModule.forFeature([RecipeEffects]),
];

@NgModule({
	imports: [store],
	exports: [store],
})
export class RecipeStoreModule {}
