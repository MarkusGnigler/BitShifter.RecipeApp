import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Category } from '../../../categories/models/category';
import { Recipe } from '../../models/recipe';

export const RECIPE_FORMGROUP_NAMES = {
	id: 'id',
	slug: 'slug',
	title: 'title',
	img: 'img',
	category: 'category',
	preparation: 'preparation',
	description: 'description',
	ingredients: 'ingredients',
};

@Component({
	selector: 'pxd-recipe-form',
	templateUrl: './recipe-form.component.html',
	styleUrls: ['./recipe-form.component.scss'],
})
export class RecipeFormComponent {
	//
	readonly FORMGROUP_NAMES = RECIPE_FORMGROUP_NAMES;

	@Input() group?: FormGroup;
	@Input() recipe?: Recipe;
	@Input() availableCategories?: Category[] | null;

	getControlByName = (name: string) => this.group?.get(name) as FormControl;

	getErrorMessage(control: FormControl) {
		if (control.hasError('required')) {
		  return 'Du must einen Wert eingeben';
		}

		return control.hasError('required') ? 'Du must einen Wert eingeben' : '';
	}
}
