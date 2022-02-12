import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryCreatorComponent } from './components/category-creator/category-creator.component';
import { CoreLibModule } from '@pixeldance-materialize/core-lib';
import { UiLibModule } from '@pixeldance-materialize/ui-lib';

const components = [CategoryCreatorComponent];

@NgModule({
	declarations: [components],
	exports: [components],
	imports: [CommonModule, CoreLibModule, UiLibModule],
})
export class CategoryModule {}
