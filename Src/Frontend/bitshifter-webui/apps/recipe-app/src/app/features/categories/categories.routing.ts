import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// import { AdminGuard } from '@bitshifter-webui/identity-lib';
import { CategoriesLayoutComponent } from './categories-layout.component';
import { CategoryOverviewComponent } from './components/category-overview/category-overview.component';

const routes: Routes = [
	{
		path: '',
		component: CategoriesLayoutComponent,
		children: [
			{
				path: '',
				// canActivate: [AdminGuard],
				component: CategoryOverviewComponent,
			},
		],
	},
	{ path: '**', redirectTo: '/kategorie', pathMatch: 'full' },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class CategoriesRoutingModule {}
