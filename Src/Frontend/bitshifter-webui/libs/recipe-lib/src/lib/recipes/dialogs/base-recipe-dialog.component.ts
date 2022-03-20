import { Component } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Actions } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { BaseFormComponent, FileService } from '@bitshifter-webui/core-lib';
import { createRecipe, updateRecipe } from '../+domain/recipe.actions';
import { CategoryService } from '../../categories/services/category.service';
import { RecipeState } from '../models/+recipe';
import { Ingredient } from '../models/ingredient';
import { Recipe } from '../models/recipe';
import { INGREDIENT_FORMGROUP_NAMES } from '../components/ingredient-selector/ingredient-selector.component';
import { RECIPE_FORMGROUP_NAMES } from './recipe-form/recipe-form.component';

@Component({
	selector: 'bs-base-recipe-dialog',
	template: '',
	styles: [
		`
			.liked {
				color: red;
			}
		`,
	],
})
export abstract class BaseRecipeDialogComponent extends BaseFormComponent {
	//
	resolvedRecipe?: Recipe;

	categories$ = this.categoryService.categories$;

	get ingredients(): FormArray {
		return this.formGroup?.get('ingredients') as FormArray;
	}

	constructor(
		protected router: Router,
		public route: ActivatedRoute,
		protected actions$: Actions,
		protected store: Store<RecipeState>,
		protected categoryService: CategoryService,
		protected fileService: FileService,
	) {
		super();
	}

	//#region [ Build Form ]
	override buildFormGroup(): FormGroup {
		const ingredientFormArray = this.resolvedRecipe?.ingredients
			? this.resolvedRecipe?.ingredients.map(this.buildIngredientFormGroup)
			: [this.buildIngredientFormGroup()];

		return new FormGroup({
			id: new FormControl(
				this.resolvedRecipe?.id ?? '',
				this.resolvedRecipe === undefined ? [] : [Validators.required],
			),
			slug: new FormControl(this.resolvedRecipe?.slug ?? '', [Validators.required]),
			title: new FormControl(this.resolvedRecipe?.title ?? '', [Validators.required]),
			img: new FormControl(this.resolvedRecipe?.imgFile ?? {}, [Validators.required]),
			category: new FormControl(this.resolvedRecipe?.categoryId ?? '', [Validators.required]),
			preparation: new FormControl(this.resolvedRecipe?.preparation ?? '', [Validators.required]),
			description: new FormControl(this.resolvedRecipe?.description ?? '', [Validators.required]),
			ingredients: new FormArray(ingredientFormArray),
		});
	}

	protected buildIngredientFormGroup(ingredient?: Ingredient): FormGroup {
		return new FormGroup({
			title: new FormControl(ingredient?.title ?? '', [Validators.required]),
			quantity: new FormControl(ingredient?.quantity ?? 0, [Validators.required]),
			unit: new FormControl(ingredient?.unit ?? '', [Validators.required]),
		});
	}
	//#endregion

	onSubmit() {
		const actionType = !this.resolvedRecipe ? createRecipe : updateRecipe;
		const storeAction = actionType({ recipe: this.createRecipeFromForm() });

		this.store.dispatch(storeAction);

		// this.actions$.pipe(ofType(actionType)).subscribe({ next: _ => this.onSubmitingFinished() });
		this.onSubmitingFinished();
	}

	protected onSubmitingFinished() {
		this.formGroup.reset();
		this.router.navigate(['/rezepte']);
	}

	//#region [ Create Recipe ]
	protected createRecipeFromForm(): Recipe {
		const image = this.getField(RECIPE_FORMGROUP_NAMES.img)?.value;

		return {
			id: this.getField(RECIPE_FORMGROUP_NAMES.id)?.value ?? '',
			slug: this.getField(RECIPE_FORMGROUP_NAMES.slug)?.value ?? '',
			title: this.getField(RECIPE_FORMGROUP_NAMES.title)?.value ?? '',
			img: image.name,
			imgFile: image,
			categoryId: this.getField(RECIPE_FORMGROUP_NAMES.category)?.value ?? '',
			preparation: this.getField(RECIPE_FORMGROUP_NAMES.preparation)?.value ?? '',
			description: this.getField(RECIPE_FORMGROUP_NAMES.description)?.value ?? '',
			liked: false,
			position: 1,
			priority: 0,
			ingredients: this.createIngredientFromForm() ?? [],
		};
	}

	protected createIngredientFromForm(): Ingredient[] {
		return this.ingredients?.controls.map(formgroup => ({
			title: formgroup.get(INGREDIENT_FORMGROUP_NAMES.title)?.value ?? '',
			quantity: formgroup.get(INGREDIENT_FORMGROUP_NAMES.quantity)?.value ?? 0,
			unit: formgroup.get(INGREDIENT_FORMGROUP_NAMES.unit)?.value ?? '',
			priority: 0,
		}));
	}
	//#endregion
}
