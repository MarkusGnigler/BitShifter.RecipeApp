import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Store } from '@ngrx/store';
import { CategoryService, filterRecipeByCategory, RecipeAppState } from '@pixeldance-materialize/recipe-lib';
import { tap } from 'rxjs/operators';

@Component({
	selector: 'pxd-recipe-header',
	templateUrl: './recipe-header.component.html',
	styleUrls: ['./recipe-header.component.scss'],
})
export class RecipeHeaderComponent {
	//
	categorySelectionControl = new FormControl();
	availableCategories$ = this.categoryService.categories$;

	constructor(private store: Store<RecipeAppState>, private categoryService: CategoryService) {
		this.categorySelectionControl.valueChanges
			.pipe(tap(categoryName => this.store.dispatch(filterRecipeByCategory({ categoryName }))))
			.subscribe();
	}

	onEmptyFilter() {
		this.categorySelectionControl.setValue(null);
	}
}
