import { createReducer, on } from '@ngrx/store';
import { RecipeState } from '../models/+recipe';
import * as RecipeActions from './recipe.actions';

export const recipeFeatureKey = 'Recipe';

export interface RecipeAppState {
	recipeState: RecipeState;
}

export const initialState: RecipeState = {
	recipes: [],
	selectedRecipe: null,
	searchCriteria: '',
	loading: false,
	error: undefined,
};

export const reducer = createReducer(
	initialState,

	//#region load all recipes
	on(RecipeActions.loadRecipes, state => ({ ...state, loading: true })),
	on(RecipeActions.loadRecipesSuccess, (state, action) => ({
		...state,
		recipes: action.recipes,
		filteredRecipes: action.recipes,
	})),
	on(RecipeActions.loadRecipesFailure, (state, action) => ({ ...state, error: action.error })),
	//#endregion

	//#region load single recipe
	on(RecipeActions.loadRecipeBySlug, state => ({ ...state, loading: true })),
	on(RecipeActions.loadRecipeBySlugSuccess, (state, action) => ({ ...state, selectedRecipe: action.recipe })),
	on(RecipeActions.loadRecipeBySlugFailure, (state, action) => ({ ...state, error: action.error })),
	//#endregion

	//#region create recipe
	on(RecipeActions.createRecipe, state => ({ ...state, loading: true })),
	on(RecipeActions.createRecipeSuccess, (state, action) => ({
		...state,
		recipes: [...state.recipes, action.recipe],
		loading: false,
	})),
	on(RecipeActions.createRecipeFailure, (state, action) => ({ ...state, error: action.error, loading: false })),
	//#endregion

	//#region update recipe
	on(RecipeActions.updateRecipe, state => ({ ...state, loading: true })),
	on(RecipeActions.updateRecipeSuccess, (state, action) => ({
		...state,
		recipes: state.recipes.map(r => (r.id === action.recipe.id ? action.recipe : r)),
		selectedRecipe: action.recipe.id == state.selectedRecipe?.id ? action.recipe : state.selectedRecipe,
		loading: false,
	})),
	on(RecipeActions.updateRecipeFailure, (state, action) => ({ ...state, error: action.error, loading: false })),
	//#endregion

	//#region delete recipe
	on(RecipeActions.deleteRecipe, state => ({ ...state, loading: true })),
	on(RecipeActions.deleteRecipeSuccess, (state, action) => ({
		...state,
		recipes: state.recipes.filter(r => r.id === action.recipeId),
		loading: false,
	})),
	on(RecipeActions.deleteRecipeFailure, (state, action) => ({ ...state, error: action.error, loading: false })),
	//#endregion

	//#region recipe utilities
	on(RecipeActions.filterRecipeByCategory, (state, action) => ({ ...state, searchCriteria: action.categoryName ?? '' })),
	//#endregion
);
