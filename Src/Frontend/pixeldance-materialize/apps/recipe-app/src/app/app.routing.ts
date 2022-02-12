import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from '@pixeldance-materialize/ui-lib';
import { AppLayoutComponent } from './app-layout.component';
import { StandAloneLayoutComponent } from './stand-alone-layout.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'rezepte',
		pathMatch: 'full',
	},
	{
		path: '',
		component: AppLayoutComponent,
		children: [
			{
				path: 'rezepte',
				loadChildren: () => import('./features/recipes/recipes.module').then(m => m.RecipesModule),
			},
			{
				path: 'kategorien',
				loadChildren: () => import('./features/categories/categories.module').then(m => m.CategoriesModule),
			},
		],
	},
	{
		path: '',
		component: StandAloneLayoutComponent,
		children: [
			{
				path: 'identity',
				loadChildren: () => import('./features/identity/identity.module').then(m => m.IdentityModule),
			},
		],
	},
	{
		path: 'error',
		component: StandAloneLayoutComponent,
		children: [
			{
				path: 'seite-nicht-gefunden',
				component: PageNotFoundComponent,
			},
			{ path: '**', redirectTo: '/error/seite-nicht-gefunden', pathMatch: 'full' },
		],
	},
	{ path: '**', redirectTo: '/error/seite-nicht-gefunden', pathMatch: 'full' },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
