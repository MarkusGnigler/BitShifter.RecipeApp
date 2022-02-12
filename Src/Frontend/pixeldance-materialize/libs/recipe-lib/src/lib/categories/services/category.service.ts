import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { generateGuid } from '@pixeldance-materialize/core-lib';
import { of, BehaviorSubject, Observable, Subscription } from 'rxjs';
import { catchError, distinctUntilChanged, map, shareReplay, tap } from 'rxjs/operators';
import { RecipesLibConfig } from '../../recipes-lib.config';
import { CategoryState } from '../models/+category';
import { Category } from '../models/category';

let _state: CategoryState = {
	categories: [],
	loading: false,
};

@Injectable({
	providedIn: 'root',
})
export class CategoryService {
	//
	private readonly BASE_URL = `${this.config.apiUrl}category/`;

	private readonly subscriptions$$: Subscription[] = [];

	// ------- State ------------------------

	private readonly _store = new BehaviorSubject<CategoryState>(_state);
	readonly state$ = this._store.asObservable();

	readonly categories$ = this.state$.pipe(
		map(state => state.categories),
		distinctUntilChanged(),
		shareReplay(),
	);
	readonly loading$ = this.state$.pipe(map(state => state.loading));

	constructor(protected config: RecipesLibConfig, protected http: HttpClient) {
		this.loadRecipes();
	}

	private loadRecipes() {
		this.updateState({ ..._state, loading: true });

		const subscription = this.http.get<Category[]>(this.BASE_URL).subscribe(categories => {
			this.updateState({ ..._state, categories, loading: false });
		});

		this.subscriptions$$.push(subscription);
	}

	// ------- Public Methods ------------------------

	// Allows quick snapshot access to data for ngOnInit() purposes
	getStateSnapshot(): CategoryState {
		return { ..._state };
	}

	// ------- Category ------------------------

	createCategory(category: Category) {
		this.updateState({ ..._state, loading: true });

		category.id = category.id == '' ? generateGuid() : category.id;

		return this.http.put<Category>(`${this.BASE_URL}${category.id}`, category).pipe(
			tap(newCategory =>
				this.updateState({ ..._state, categories: [..._state.categories, newCategory], loading: false }),
			),
			catchError(error => this.handleError(error)),
		);
	}

	updateCategory(category: Category) {
		this.updateState({ ..._state, loading: true });

		return this.http.patch<Category>(`${this.BASE_URL}${category.id}`, category).pipe(
			tap(newCategory => {
				this.updateState({
					..._state,
					categories: [..._state.categories.filter(x => x.id !== newCategory.id), newCategory],
					loading: false,
				});
			}),
			catchError(error => this.handleError(error)),
		);
	}

	deleteCategory(category: Category) {
		this.updateState({ ..._state, loading: true });

		return this.http.delete<string>(`${this.BASE_URL}${category.id}`).pipe(
			tap(categoryId =>
				this.updateState({
					..._state,
					categories: [..._state.categories.filter(x => x.id !== categoryId)],
					loading: false,
				}),
			),
			catchError(error => this.handleError(error)),
		);
	}

	// ------- Private Methods ------------------------

	/** Update internal state cache and emit from store... */
	private updateState(state: CategoryState) {
		this._store.next((_state = state));
	}

	private handleError(error: unknown): Observable<unknown> {
		this.updateState({ ..._state, loading: false });
		return of(error);
	}
}
