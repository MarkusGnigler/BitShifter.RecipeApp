import { createAction, props } from '@ngrx/store';
import { Recipe } from '../models/recipe';

const domain = `[Recipes]`;

//#region load all recipes
export const loadRecipes = createAction(
  `${domain} Load Recipes`,
);
export const loadRecipesSuccess = createAction(
  `${domain} Load Recipes Success`,
  props<{ recipes: Recipe[] }>()
);
export const loadRecipesFailure = createAction(
  `${domain} Load Recipes Failure`,
  props<{ error: unknown }>()
);
//#endregion

//#region load single recipe
export const loadRecipeBySlug = createAction(
  `${domain} Load Recipe By Slug`,
  props<{ slug: string }>()
);
export const loadRecipeBySlugSuccess = createAction(
  `${domain} Load Recipe By SlugSuccess`,
  props<{ recipe: Recipe }>()
);
export const loadRecipeBySlugFailure = createAction(
  `${domain} Load Recipe By SlugFailure`,
  props<{ error: unknown }>()
);
//#endregion

//#region create recipe
export const createRecipe = createAction(
  `${domain} Create Recipe`,
  props<{ recipe: Recipe }>()
);
export const createRecipeSuccess = createAction(
  `${domain} Create Recipe Success`,
  props<{ recipe: Recipe }>()
);
export const createRecipeFailure = createAction(
  `${domain} Create Recipe Failure`,
  props<{ error: unknown }>()
);
//#endregion

//#region update recipe
export const updateRecipe = createAction(
  `${domain} Update Recipe`,
  props<{ recipe: Recipe }>()
);
export const updateRecipeSuccess = createAction(
  `${domain} Update Recipe Success`,
  props<{ recipe: Recipe }>()
);
export const updateRecipeFailure = createAction(
  `${domain} Update Recipe Failure`,
  props<{ error: unknown }>()
);
//#endregion

//#region delete recipe
export const deleteRecipe = createAction(
  `${domain} Delete Recipe`,
  props<{ recipeId: string }>()
);
export const deleteRecipeSuccess = createAction(
  `${domain} Delete Recipe Success`,
  props<{ recipeId: string }>()
);
export const deleteRecipeFailure = createAction(
  `${domain} Delete Recipe Failure`,
  props<{ error: unknown }>()
);
//#endregion

//#region recipe utilities
export const filterRecipeByCategory = createAction(
  `${domain} Filter Recipe By Category`,
  props<{ categoryName: string }>()
);
//#endregion



