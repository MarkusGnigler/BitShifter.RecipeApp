import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Recipe } from '../../models/recipe';

@Component({
	selector: 'pxd-recipe-card',
	templateUrl: './recipe-card.component.html',
	styleUrls: ['./recipe-card.component.scss'],
})
export class RecipeCardComponent {
	//
	@Input() recipe!: Recipe;
	@Output() updateRecipe = new EventEmitter<Recipe>();
	@Output() editRecipe = new EventEmitter<Recipe>();
	@Output() showRecipe = new EventEmitter<Recipe>();

	onLiked() {
		this.updateRecipe.emit({ ...this.recipe, liked: !this.recipe.liked });
	}

	onEditRecipe() {
		this.editRecipe.emit(this.recipe);
	}

	onShowRecipe() {
		this.showRecipe.emit(this.recipe);
	}
}
