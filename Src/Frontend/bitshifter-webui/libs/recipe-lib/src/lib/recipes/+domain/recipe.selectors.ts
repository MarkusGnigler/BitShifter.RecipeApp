import { createFeatureSelector, createSelector } from '@ngrx/store';
import { RecipeState } from '../models/+recipe';
import { recipeFeatureKey } from './recipe.reducer';

const selectRecipeState = createFeatureSelector<RecipeState>(recipeFeatureKey);

// Select all recipes
const selectAllRecipe = createSelector(selectRecipeState, state => state.recipes);

// Select all filtered recipes
// const selectAllFilteredRecipe = createSelector(selectRecipeState, state => state.filteredRecipes);

// Select all filtered recipes
const selectAllFilteredRecipe = createSelector(selectRecipeState, state => {
	// prettier-ignore
	return state.searchCriteria === '' 
		? state.recipes 
		: state.recipes.filter(x => x.category === state.searchCriteria);
});

// Select selected recipe
const selectSingleRecipe = createSelector(selectRecipeState, state => state.selectedRecipe);

// Select is loading
const selectRecipesAreLoading = createSelector(selectRecipeState, state => state.loading);

export {
	//
	selectAllRecipe,
	selectAllFilteredRecipe,
	selectSingleRecipe,
	selectRecipesAreLoading,
};
