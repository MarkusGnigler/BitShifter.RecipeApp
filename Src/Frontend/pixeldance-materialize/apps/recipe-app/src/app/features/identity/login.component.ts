import { Component } from '@angular/core';

@Component({
	selector: 'acr-frontend-login',
	template: `
		<section class="container">
			<div>
				<auth-login></auth-login>
				<button mat-stroked-button class="register-button" [routerLink]="['/identity/register']">
					Noch nicht angemeldet?
				</button>
			</div>
		</section>
	`,
	styles: [
		`
			.container {
				height: 92vh;
				display: flex;
				flex-direction: column;
				justify-content: center;
				align-items: center;
			}

			.register-button {
				margin-top: 25px;
			}
		`,
	],
})
export class LoginComponent {}
