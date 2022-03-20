import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecipesLibConfig } from './recipes-lib.config';
import { RecipeModule } from './recipes/recipe.module';
import { CategoryModule } from './categories/category.module';

const libModules = [CategoryModule, RecipeModule];

@NgModule({
	imports: [CommonModule, libModules],
	exports: [libModules],
})
export class RecipeLibModule {
	// Setup
	static forRoot(config: RecipesLibConfig): ModuleWithProviders<RecipeLibModule> {
		return {
			ngModule: RecipeLibModule,
			providers: [{ provide: RecipesLibConfig, useValue: config }],
		};
	}
}
