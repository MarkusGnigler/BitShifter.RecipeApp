import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Recipe } from '../../models/recipe';

@Component({
	selector: 'pxd-recipe-detail',
	templateUrl: './recipe-detail.component.html',
	styleUrls: ['./recipe-detail.component.scss'],
})
export class RecipeDetailComponent implements OnInit {
	//
	@Input() recipe!: Recipe;

	@Output() likeRecipe = new EventEmitter();
	@Output() deleteRecipe = new EventEmitter();

	ngOnInit(): void {
		if (!this.recipe) throw new Error(`No recipe was given in ${this.constructor.name}`);
	}
}
