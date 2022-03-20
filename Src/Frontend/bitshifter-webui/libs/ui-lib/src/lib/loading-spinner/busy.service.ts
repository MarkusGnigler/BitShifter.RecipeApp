import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root',
})
export class BusyService {
	//
	private busyRequestCount = 0;

	get isActive(): boolean {
		return this.busyRequestCount > 0;
	}

	busy() {
		this.busyRequestCount++;
	}

	idle() {
		this.busyRequestCount--;
		if (this.busyRequestCount > 0) return;
		this.busyRequestCount = 0;
	}
}
