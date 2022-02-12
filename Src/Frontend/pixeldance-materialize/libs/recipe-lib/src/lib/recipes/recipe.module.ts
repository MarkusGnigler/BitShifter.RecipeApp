import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreLibModule } from '@pixeldance-materialize/core-lib';
import { UiLibModule } from '@pixeldance-materialize/ui-lib';
import { RecipeCardComponent } from './components/recipe-card/recipe-card.component';
import { RouterModule } from '@angular/router';
import { RecipeFormsModule } from './dialogs/recipe-forms.module';
import { RecipeDetailComponent } from './components/recipe-detail/recipe-detail.component';
import { RecipeStoreModule } from './+domain/recipe-store.module';

const components = [RecipeCardComponent, RecipeDetailComponent];

@NgModule({
	declarations: [components],
	exports: [components],
	imports: [
		//
		CommonModule,
		CoreLibModule,
		UiLibModule,
		RouterModule,
		RecipeStoreModule,
		RecipeFormsModule,
	],
})
export class RecipeModule {}
