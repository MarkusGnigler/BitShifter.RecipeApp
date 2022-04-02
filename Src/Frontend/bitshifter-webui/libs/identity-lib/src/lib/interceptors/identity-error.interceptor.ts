import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
// import { NotificationService } from '@bitshifter-webui/notification-lib';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class IdentityErrorInterceptor implements HttpInterceptor {
	//
	constructor(
		private router: Router,
		private snackBar: MatSnackBar, // private notificationService: NotificationService
	) {}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		return next.handle(request).pipe(catchError((errorResponse: HttpErrorResponse) => this.handleError(errorResponse)));
	}

	private handleError(error: HttpErrorResponse) {
		console.log('http error => ', error);

		const errorMessage = this.buildErrorsMessage(error);
		// if (errorMessage != '') this.notificationService.showError(errorMessage);
		if (errorMessage != '') {
			this.snackBar.open(errorMessage, 'Ok', {
				duration: 2500,
				horizontalPosition: 'center',
				verticalPosition: 'top',
			});
		}

		if (this.isUnauthorized(error) && errorMessage.startsWith('Unberechtigt: ')) this.routeToLogin();

		return throwError(() => error);
	}

	private buildErrorsMessage(error: HttpErrorResponse) {
		return this.isClientsideError(error)
			? // is client-side error
			  `Error: ${error}`
			: // is server-side error
			  this.getServerErrorMessage(error);
	}

	private isClientsideError = (error: HttpErrorResponse) => error.error instanceof ErrorEvent;

	private getServerErrorMessage(error: HttpErrorResponse): string {
		if (!this.isUnauthorized(error)) return '';

		//prettier-ignore
		return error.error.type === ''
			? `${error.error.detail}`
			: `Unberechtigt: ${error.error.detail}`;
	}

	private isUnauthorized = (error: HttpErrorResponse) => error.status === 401;
	private routeToLogin = () => this.router.navigate(['/identity/login']);
}
