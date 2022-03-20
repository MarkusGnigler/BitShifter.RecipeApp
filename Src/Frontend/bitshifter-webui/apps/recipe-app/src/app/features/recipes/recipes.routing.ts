import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
	LoadRecipeBySlugResolver,
	PreventUnsavedRecipeGuard,
	RecipeCreatorComponent,
	RecipeEditorComponent,
} from '@bitshifter-webui/recipe-lib';
import { RecipesLayoutComponent } from './recipes-layout.component';
import { RecipesOverviewComponent } from './components/recipes-overview/recipes-overview.component';
import { RecipeDetailComponent } from './components/recipe-detail/recipe-detail.component';
import { RecipeDialogLayoutComponent } from './recipe-dialog-layout.component';
import { AdminGuard } from '@bitshifter-webui/identity-lib';

const routes: Routes = [
	{
		path: '',
		component: RecipesLayoutComponent,
		children: [
			{
				path: '',
				component: RecipesOverviewComponent,
			},
			{
				path: ':slug',
				component: RecipeDetailComponent,
				resolve: { recipe: LoadRecipeBySlugResolver },
			},
		],
	},
	{
		path: 'r',
		canActivate: [AdminGuard],
		component: RecipeDialogLayoutComponent,
		children: [
			{
				path: 'creator',
				component: RecipeCreatorComponent,
				canDeactivate: [PreventUnsavedRecipeGuard],
			},
			{
				path: 'editor/:slug',
				component: RecipeEditorComponent,
				resolve: { recipe: LoadRecipeBySlugResolver },
				canDeactivate: [PreventUnsavedRecipeGuard],
			},
		],
	},
	{ path: '**', redirectTo: '/rezepte', pathMatch: 'full' },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RecipesRoutingModule {}
