import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { RecipeEditorComponent } from '../dialogs/recipe-editor/recipe-editor.component';

@Injectable({
	providedIn: 'root',
})
export class PreventUnsavedRecipeGuard implements CanDeactivate<unknown> {
	//
	// constructor( private confirmService: ConfirmService ) {}

	canDeactivate(component: RecipeEditorComponent): Observable<boolean> | boolean {
		if (!component.formGroup.dirty) return true;
		
		return confirm('\rBist du sicher das das Rezept ohne speichern verlassen willst?\r\rNicht gespeicherte Änderungen werden verworfen.');
		// return this.confirmService.confirm()
	}
}
