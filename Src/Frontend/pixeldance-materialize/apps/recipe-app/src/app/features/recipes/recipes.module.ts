import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RecipesRoutingModule } from './recipes.routing';

import { RecipesLayoutComponent } from './recipes-layout.component';
import { RecipeDialogLayoutComponent } from './recipe-dialog-layout.component';
import { RecipesOverviewComponent } from './components/recipes-overview/recipes-overview.component';

import { RecipeLibModule } from '@pixeldance-materialize/recipe-lib';
import { PxdLibModule } from '../../plugins/pxd-lib.module';
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
	imports: [CommonModule, PxdLibModule, RecipesRoutingModule, RecipeLibModule],
})
export class RecipesModule {}
