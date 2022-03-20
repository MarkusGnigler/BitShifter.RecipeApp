import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RecipesRoutingModule } from './recipes.routing';

import { RecipesLayoutComponent } from './recipes-layout.component';
import { RecipeDialogLayoutComponent } from './recipe-dialog-layout.component';
import { RecipesOverviewComponent } from './components/recipes-overview/recipes-overview.component';

import { RecipeLibModule } from '@bitshifter-webui/recipe-lib';
import { BsLibModule } from '../../plugins/bs-lib.module';
import { RecipeDetailComponent } from './components/recipe-detail/recipe-detail.component';
import { RecipeHeaderComponent } from './components/recipe-header/recipe-header.component';

const components = [
	RecipesLayoutComponent,
	RecipeDialogLayoutComponent,
	RecipesOverviewComponent,
	RecipeDetailComponent,
];

@NgModule({
	declarations: [components, RecipeHeaderComponent],
	imports: [CommonModule, BsLibModule, RecipesRoutingModule, RecipeLibModule],
})
export class RecipesModule {}
