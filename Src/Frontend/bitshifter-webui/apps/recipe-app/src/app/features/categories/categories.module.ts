import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories.routing';
import { CategoryOverviewComponent } from './components/category-overview/category-overview.component';
import { CategoriesLayoutComponent } from './categories-layout.component';
import { BsLibModule } from '../../plugins/bs-lib.module';
import { RecipeLibModule } from '@bitshifter-webui/recipe-lib';

const components = [CategoriesLayoutComponent, CategoryOverviewComponent];

@NgModule({
	declarations: [components],
	imports: [CommonModule, BsLibModule, CategoriesRoutingModule, RecipeLibModule],
})
export class CategoriesModule {}
