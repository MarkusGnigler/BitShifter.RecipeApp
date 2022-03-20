import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FileService, generateGuid } from '@bitshifter-webui/core-lib';
import { Observable } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { RecipesLibConfig } from '../../recipes-lib.config';
import { Recipe } from '../models/recipe';

@Injectable({
	providedIn: 'root',
})
export class RecipeStoreService {
	//
	private readonly url = `${this.config.apiUrl}recipe/`;

	constructor(protected config: RecipesLibConfig, private http: HttpClient, private fileService: FileService) {}

	loadAll(): Observable<Recipe[]> {
		return this.http.get<Recipe[]>(this.url);
	}

	loadSingle(slug: string) {
		return this.http
			.get<Recipe>(`${this.url}${slug}`)
			.pipe(
				switchMap(recipe =>
					this.fileService.downloadFile(recipe.img).pipe(map(file => ({ ...recipe, imgFile: file }))),
				),
			);
	}

	create(recipe: Recipe) {
		const recipeToCreate = { ...recipe, id: recipe.id == '' ? generateGuid() : recipe.id };

		return this.http.put<Recipe>(`${this.url}${recipeToCreate.id}`, recipeToCreate).pipe(
			map(r => ({ ...r, imgFile: recipe.imgFile })),
			tap(r => this.fileService.uploadFile(r.imgFile)),
		);
	}

	update(recipe: Recipe) {
		return this.http.patch<Recipe>(`${this.url}${recipe.id}`, recipe).pipe(
			map(r => ({ ...r, imgFile: recipe.imgFile })),
			tap(r => this.fileService.uploadFile(r.imgFile)),
		);
	}

	remove(recipeId: string) {
		return this.http.delete<string>(`${this.url}${recipeId}`);
	}
}
