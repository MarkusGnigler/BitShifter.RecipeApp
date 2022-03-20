import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { BaseResolver } from '@bitshifter-webui/core-lib';
import {
	deleteRecipe,
	deleteRecipeSuccess,
	Recipe,
	RecipeState,
	updateRecipe,
} from '@bitshifter-webui/recipe-lib';
import { BaseViewComponent, LayoutService } from '@bitshifter-webui/ui-lib';

@Component({
	selector: 'bs-app-recipe-detail',
	templateUrl: './recipe-detail.component.html',
	styleUrls: ['./recipe-detail.component.scss'],
})
export class RecipeDetailComponent extends BaseViewComponent implements OnInit, BaseResolver {
	//
	recipe!: Recipe;
	override titleText!: string;

	constructor(
		public route: ActivatedRoute,
		private router: Router,
		private store: Store<RecipeState>,
		protected actions$: Actions,
		override layoutService: LayoutService,
	) {
		super(layoutService);
	}

	override ngOnInit(): void {
		this.resolveRoute();
		this.titleText = this.recipe?.title ?? 'Kein Rezept vorhanden';
		super.ngOnInit();
	}

	resolveRoute() {
		this.route.data.subscribe(data => {
			this.recipe = data['recipe'];
			// this.store.dispatch(loadRecipeBySlug({ slug: String(data.recipe.slug) }));
		});
	}

	onLiked() {
		this.recipe = { ...this.recipe, liked: !this.recipe.liked };
		this.store.dispatch(updateRecipe({ recipe: this.recipe }));
	}

	onDeleteRecipe() {
		this.store.dispatch(deleteRecipe({ recipeId: this.recipe.id }));
		// eslint-disable-next-line @typescript-eslint/no-unused-vars
		this.actions$.pipe(ofType(deleteRecipeSuccess)).subscribe({ next: _ => this.router.navigateByUrl('rezepte') });
	}
}
