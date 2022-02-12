import { Component } from '@angular/core';
import { Category, CategoryService } from '@pixeldance-materialize/recipe-lib';
import { BaseViewComponent, LayoutService } from '@pixeldance-materialize/ui-lib';

@Component({
	selector: 'pxd-category-overview',
	templateUrl: './category-overview.component.html',
	styleUrls: ['./category-overview.component.scss'],
})
export class CategoryOverviewComponent extends BaseViewComponent {
	//
	titleText = 'Kategorieübersicht';

	categoryToUpdate!: string;

	categories$ = this.categoryService.categories$;

	constructor(protected layoutService: LayoutService, private categoryService: CategoryService) {
		super(layoutService);
	}

	onCreateCategory(category: Category) {
		if (category.name == null) return;

		this.categoryService
			.createCategory(category)
			// eslint-disable-next-line @typescript-eslint/no-unused-vars
			.subscribe({ next: _ => console.log('Kategorie erfolgreich erstellt.') });
	}

	onSetCategoryEditable(category: Category) {
		category.isEditable = true;
		
		this.categoryToUpdate = category.name;
	}

	onCancelCategoryEditable(category: Category) {
		category.isEditable = false;

		this.categoryToUpdate = category.name;
	}

	onUpdateCategory(category: Category) {
		category.name = this.categoryToUpdate;

		this.categoryService
			.updateCategory(category)
			// eslint-disable-next-line @typescript-eslint/no-unused-vars
			.subscribe({ next: _ => console.log('Kategorie erfolgreich geändert.') });
	}

	onDeleteCategory(category: Category) {
		this.categoryService
			.deleteCategory(category)
			// eslint-disable-next-line @typescript-eslint/no-unused-vars
			.subscribe({ next: _ => console.log('Kategorie erfolgreich gelöscht.') });
	}
}
