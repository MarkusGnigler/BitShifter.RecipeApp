import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreLibModule } from '@pixeldance-materialize/core-lib';
import { UiLibModule } from '@pixeldance-materialize/ui-lib';
import { RecipeCreatorComponent } from './recipe-creator/recipe-creator.component';
import { RecipeEditorComponent } from './recipe-editor/recipe-editor.component';
import { RecipeFormComponent } from './recipe-form/recipe-form.component';
import { IngredientSelectorComponent } from '../components/ingredient-selector/ingredient-selector.component';
import { RouterModule } from '@angular/router';

const components = [IngredientSelectorComponent, RecipeFormComponent, RecipeEditorComponent, RecipeCreatorComponent];

@NgModule({
	declarations: [components],
	exports: [components],
	imports: [CommonModule, CoreLibModule, UiLibModule, RouterModule],
})
export class RecipeFormsModule {}
