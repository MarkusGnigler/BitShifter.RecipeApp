import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { RecipeState } from '../models/+recipe';
import { Recipe } from '../models/recipe';
import { RecipeStoreService } from '../services/recipe-store.service';

@Injectable({
	providedIn: 'root',
})
export class LoadRecipeBySlugResolver implements Resolve<Recipe | null> {
	//
	constructor(private store: Store<RecipeState>, private recipeService: RecipeStoreService) {}

	// eslint-disable-next-line @typescript-eslint/no-unused-vars
	resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Recipe | null> {
		const slug = route.paramMap.get('slug');

		if (!slug) return of(null);

		// this.store.dispatch(loadRecipeBySlug({ slug: slug }));
		// return this.store.pipe(
		// 	select(selectSingleRecipe),
		// 	filter(x => !!x),
		// 	tap(x => console.log('subscribed', x)),
		// );

		return this.recipeService.loadSingle(slug);
	}
}
