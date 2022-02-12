import { Component } from '@angular/core';

@Component({
	selector: 'acr-frontend-login',
	template: `
		<section class="container">
			<auth-register></auth-register>
		</section>
	`,
	styles: [
		`
			.container {
				height: 92vh;
				display: flex;
				justify-content: center;
				align-items: center;
			}
		`,
	],
})
export class RegisterComponent {}
