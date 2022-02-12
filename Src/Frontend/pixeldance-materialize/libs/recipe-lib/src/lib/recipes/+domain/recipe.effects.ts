import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';

import * as RecipeActions from './recipe.actions';
import { RecipeStoreService } from '../services/recipe-store.service';

@Injectable()
export class RecipeEffects {
	//
	loadAllRecipes$ = createEffect(() =>
		this.actions$.pipe(
			ofType(RecipeActions.loadRecipes),
			// eslint-disable-next-line @typescript-eslint/no-unused-vars
			switchMap(_ =>
				this.recipeService.loadAll().pipe(
					map(recipes => RecipeActions.loadRecipesSuccess({ recipes })),
					catchError(err => of(RecipeActions.loadRecipesFailure(err))),
				),
			),
		),
	);

	loadSingleRecipe$ = createEffect(() =>
		this.actions$.pipe(
			ofType(RecipeActions.loadRecipeBySlug),
			switchMap(action =>
				this.recipeService.loadSingle(action.slug).pipe(
					map(recipe => RecipeActions.loadRecipeBySlugSuccess({ recipe })),
					catchError(err => of(RecipeActions.loadRecipeBySlugFailure(err))),
				),
			),
		),
	);

	createRecipe$ = createEffect(() =>
		this.actions$.pipe(
			ofType(RecipeActions.createRecipe),
			switchMap(action =>
				this.recipeService.create(action.recipe).pipe(
					map(recipe => RecipeActions.createRecipeSuccess({ recipe })),
					catchError(err => of(RecipeActions.createRecipeFailure(err))),
				),
			),
		),
	);

	updateRecipe$ = createEffect(() =>
		this.actions$.pipe(
			ofType(RecipeActions.updateRecipe),
			switchMap(action =>
				this.recipeService.update(action.recipe).pipe(
					map(recipe => RecipeActions.updateRecipeSuccess({ recipe })),
					catchError(err => of(RecipeActions.updateRecipeFailure(err))),
				),
			),
		),
	);

	deleteRecipe$ = createEffect(() =>
		this.actions$.pipe(
			ofType(RecipeActions.deleteRecipe),
			switchMap(action =>
				this.recipeService.remove(action.recipeId).pipe(
					map(recipeId => RecipeActions.deleteRecipeSuccess({ recipeId })),
					catchError(err => of(RecipeActions.deleteRecipeFailure(err))),
				),
			),
		),
	);

	constructor(private actions$: Actions, private recipeService: RecipeStoreService) {}
}
