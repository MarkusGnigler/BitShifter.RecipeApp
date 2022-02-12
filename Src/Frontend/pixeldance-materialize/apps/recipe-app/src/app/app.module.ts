import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { environment } from '../environments/environment';

import { StoreModule } from '@ngrx/store';
import { reducers, metaReducers } from './+domain';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { RouterState, StoreRouterConnectingModule } from '@ngrx/router-store';

import { AppRoutingModule } from './app.routing';

import { CoreLibModule } from '@pixeldance-materialize/core-lib';
import { UiLibModule } from '@pixeldance-materialize/ui-lib';

import { RecipesModule } from './features/recipes/recipes.module';
import { CategoriesModule } from './features/categories/categories.module';
import { RecipeLibModule } from '@pixeldance-materialize/recipe-lib';

import { AppComponent } from './app.component';
import { AppLayoutComponent } from './app-layout.component';
import { IdentityLibModule } from '@pixeldance-materialize/identity-lib';
import { StandAloneLayoutComponent } from './stand-alone-layout.component';
import { IdentityModule } from './features/identity/identity.module';

const envirnomentConfig = {
	apiUrl: environment.baseUrl,
	isProduction: environment.production,
};

const baseLibs = [
	//
	UiLibModule,
	CoreLibModule.forRoot(envirnomentConfig),
	IdentityLibModule.forRoot({
		...envirnomentConfig,
		loginRoute: environment.loginRoute,
		unauhtorizedRoute: '/',
	}),
];

const store = [
	StoreModule.forRoot(reducers, { metaReducers }),
	!environment.production ? StoreDevtoolsModule.instrument() : [],
	StoreRouterConnectingModule.forRoot({ stateKey: 'router', routerState: RouterState.Minimal }),
	EffectsModule.forRoot(),
];

@NgModule({
	declarations: [AppComponent, AppLayoutComponent, StandAloneLayoutComponent],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		HttpClientModule,
		AppRoutingModule,
		baseLibs,
		CategoriesModule,
		IdentityModule,
		RecipesModule,
		RecipeLibModule.forRoot(envirnomentConfig),
		store,
	],
	bootstrap: [AppComponent],
})
export class AppModule {}
