// import { HttpClient } from '@angular/common/http';
// import { Injectable, OnDestroy } from '@angular/core';
// import { Recipe } from '../models/recipe';
// import { BehaviorSubject, Observable, of, Subscription } from 'rxjs';
// import { catchError, distinctUntilChanged, map, tap } from 'rxjs/operators';
// import { RecipeState } from '../+domain/recipe.reducer';
// import { RecipesLibConfig } from '../../recipes-lib.config';

// let _state: RecipeState = {
// 	recipes: [],
// 	selectedRecipe: null,
// 	searchCriteria: '',
// 	loading: false,
// 	error: undefined,
// };

// @Injectable({
// 	providedIn: 'root',
// })
// // export class RecipeFacadeService extends BaseService<RecipeState> {
// export class RecipeFacadeService implements OnDestroy {
// 	//
// 	private readonly BASE_URL = `${this.config.apiUrl}recipe/`;

// 	private readonly subscriptions$$: Subscription[] = [];

// 	// ------- State ------------------------

// 	private readonly _store = new BehaviorSubject<RecipeState>(_state);
// 	readonly state$ = this._store.asObservable();

// 	readonly recipes$ = this.state$.pipe(
// 		map(state => state.recipes),
// 		distinctUntilChanged(),
// 		// shareReplay()
// 	);
// 	readonly searchCriteria$ = this.state$.pipe(map(state => state.searchCriteria));
// 	readonly loading$ = this.state$.pipe(map(state => state.loading));

// 	constructor(protected config: RecipesLibConfig, protected http: HttpClient) {
// 		this.loadRecipes();
// 	}

// 	private loadRecipes() {
// 		this.updateState({ ..._state, loading: true });

// 		const subscription = this.http.get<Recipe[]>(this.BASE_URL).subscribe(recipes => {
// 			this.updateState({ ..._state, recipes, loading: false });
// 		});

// 		this.subscriptions$$.push(subscription);
// 	}

// 	ngOnDestroy(): void {
// 		this.subscriptions$$.forEach(x => x.unsubscribe());
// 	}

// 	// ------- Public Methods ------------------------

// 	// Allows quick snapshot access to data for ngOnInit() purposes
// 	getStateSnapshot(): RecipeState {
// 		return { ..._state };
// 	}

// 	// ------- Recipes ------------------------

// 	createRecipe(recipe: Recipe) {
// 		console.log(`${this.constructor.name}.createRecipe() => `, recipe);
// 		this.updateState({ ..._state, loading: true });

// 		return this.http.post<Recipe>(this.BASE_URL, recipe).pipe(
// 			tap(responseRecipe =>
// 				this.updateState({ ..._state, recipes: [..._state.recipes, responseRecipe], loading: false }),
// 			),
// 			catchError(error => this.handleError(error)),
// 		);
// 	}

// 	readRecipe(recipeName: string) {
// 		console.log(`${this.constructor.name}.readRecipe() => `, recipeName);
// 		this.updateState({ ..._state, loading: true });

// 		return this.http
// 			.get<Recipe>(`${this.BASE_URL}${recipeName}`)
// 			.pipe(tap(selectedRecipe => this.updateState({ ..._state, selectedRecipe, loading: false })));
// 	}

// 	updateRecipe(recipe: Recipe) {
// 		console.log(`${this.constructor.name}.updateRecipe() => `, recipe);
// 		this.updateState({ ..._state, loading: true });

// 		return this.http.put<Recipe>(`${this.BASE_URL}${recipe.id}`, recipe).pipe(
// 			tap(newRecipe => {
// 				this.updateState({
// 					..._state,
// 					recipes: [..._state.recipes.filter(x => x.id !== newRecipe.id), newRecipe],
// 					loading: false,
// 				});
// 			}),
// 			catchError(error => this.handleError(error)),
// 		);
// 	}

// 	deleteRecipe(recipe: Recipe) {
// 		console.log(`${this.constructor.name}.deleteRecipe() => `, recipe);
// 		this.updateState({ ..._state, loading: true });

// 		return this.http.delete<string>(`${this.BASE_URL}${recipe.id}`).pipe(
// 			tap(recipeId =>
// 				this.updateState({
// 					..._state,
// 					recipes: [..._state.recipes.filter(x => x.id !== recipeId)],
// 					loading: false,
// 				}),
// 			),
// 			catchError(error => this.handleError(error)),
// 		);
// 	}

// 	// ------- Private Methods ------------------------

// 	/** Update internal state cache and emit from store... */
// 	private updateState(state: RecipeState) {
// 		this._store.next((_state = state));
// 	}

// 	private handleError(error: any): Observable<any> {
// 		this.updateState({ ..._state, loading: false });
// 		return of(error);
// 	}
// }
