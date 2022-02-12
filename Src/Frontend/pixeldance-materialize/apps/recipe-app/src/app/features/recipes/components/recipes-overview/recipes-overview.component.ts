import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { updateRecipe, RecipeAppState, Recipe, loadRecipes, selectAllFilteredRecipe } from '@pixeldance-materialize/recipe-lib';
import { BaseViewComponent, LayoutService } from '@pixeldance-materialize/ui-lib';
import { Observable } from 'rxjs';

@Component({
	selector: 'pxd-app-recipes-overview',
	templateUrl: './recipes-overview.component.html',
	styleUrls: ['./recipes-overview.component.scss'],
})
export class RecipesOverviewComponent extends BaseViewComponent {
	//
	readonly titleText = 'Rezepteübersicht';

	readonly recipes$: Observable<Recipe[]> = this.store.pipe(select(selectAllFilteredRecipe));

	constructor(protected layoutService: LayoutService, private router: Router, private store: Store<RecipeAppState>) {
		super(layoutService);
		this.store.dispatch(loadRecipes());
	}

	onUpdateRecipe(recipe: Recipe) {
		this.store.dispatch(updateRecipe({ recipe }));
	}

	onLiked(recipe: Recipe) {
		recipe.liked = !recipe.liked;
		this.onUpdateRecipe(recipe);
	}

	onEditRecipe(recipe: Recipe) {
		this.router.navigate(['/rezepte/r/editor', recipe.slug]);
	}

	onShowRecipe(recipe: Recipe) {
		this.router.navigate(['/rezepte/', recipe.slug]);
	}
}
