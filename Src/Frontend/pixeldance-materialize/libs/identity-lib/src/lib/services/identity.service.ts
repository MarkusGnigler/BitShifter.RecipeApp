import { HttpClient } from '@angular/common/http';
import { Injectable, EventEmitter } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { IdentityConfig } from '../identity-config';
import { Role } from '../models/role';
import { User } from '../models/user';

@Injectable({
	providedIn: 'root',
})
export class IdentityService {
	//
	private readonly LOCALSTORAGE_KEY = 'current-user';

	private readonly BASE_URL = `${this.config.apiUrl}identity/`;

	private _user!: User | null;
	private readonly _currentUser = new ReplaySubject<User | null>(1);
	readonly currentUser$ = this._currentUser.asObservable().pipe(tap(user => (this._user = user)));

	readonly userLoggedIn = new EventEmitter<User>();

	constructor(private http: HttpClient, private config: IdentityConfig) {
		this.loadUserFromLocalStorage();
	}

	private loadUserFromLocalStorage() {
		const user = this.getCurrentUser();
		this._currentUser.next(user);
		console.log('User from localstorage => ', user);
	}

	// Allows quick snapshot access to data for ngOnInit() purposes
	getStateSnapshot() {
		return { ...{ currentUser: this._user } };
	}

	login(user: User) {
		if (user.roles === undefined) user.roles = [];
		if (!user.roles.includes(Role.Admin)) user.roles.push(Role.Admin);

		console.log('AuthenticationService.login() User logged in');

		return this.http
			.post<User>(`${this.BASE_URL}login`, user)
			.pipe(tap((responseUser: User) => this.setCurrentUser({ ...responseUser, rememberMe: user.rememberMe })));
	}

	logout() {
		const user = this.getCurrentUser();
		if (!user) return;
		user.password = 'validation-password';

		//TODO:
		this.deleteCurrentUser();

		// return this.http.post<User>(`${this.BASE_URL}logout`, user).pipe(tap(_ => this.deleteCurrentUser()));
	}

	register(user: User) {
		return this.http
			.post<User>(`${this.BASE_URL}register`, user)
			.pipe(tap((responseUser: User) => this.setCurrentUser({ ...responseUser, rememberMe: user.rememberMe })));
	}

	setExternUserError(resetLocalStorage: boolean) {
		this._currentUser.next({ ...(this._user ?? ({} as User)), blockGuard: true });
		if (!resetLocalStorage) return;
		localStorage.removeItem(this.LOCALSTORAGE_KEY);
	}

	private setCurrentUser(user: User) {
		console.log('setCurrentUser', user);
		if (!user) return;
		this._currentUser.next(user);
		if (!user.rememberMe) return;
		localStorage.setItem(this.LOCALSTORAGE_KEY, JSON.stringify(user));
	}

	private deleteCurrentUser() {
		this._currentUser.next(null);
		localStorage.removeItem(this.LOCALSTORAGE_KEY);
	}

	getCurrentUser(): User {
		const _localStorage = localStorage.getItem(this.LOCALSTORAGE_KEY);
		return _localStorage ? JSON.parse(_localStorage) : null;
	}

	private setUserRoles(user: User) {
		user.roles = [];
		if (!user.token) return;
		const roles = this.getDecodedToken(user.token).role;
		Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
	}

	private getDecodedToken(token: string) {
		return JSON.parse(atob(token.split('.')[1]));
	}
}
