import { Recipe } from './recipe';

export interface RecipeState {
	recipes: Recipe[];
	selectedRecipe: Recipe | null;
	searchCriteria: string;
	loading: boolean;
	error: unknown;
}
