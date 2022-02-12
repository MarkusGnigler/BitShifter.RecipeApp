import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories.routing';
import { CategoryOverviewComponent } from './components/category-overview/category-overview.component';
import { CategoriesLayoutComponent } from './categories-layout.component';
import { PxdLibModule } from '../../plugins/pxd-lib.module';
import { RecipeLibModule } from '@pixeldance-materialize/recipe-lib';

const components = [CategoriesLayoutComponent, CategoryOverviewComponent];

@NgModule({
	declarations: [components],
	imports: [CommonModule, PxdLibModule, CategoriesRoutingModule, RecipeLibModule],
})
export class CategoriesModule {}
