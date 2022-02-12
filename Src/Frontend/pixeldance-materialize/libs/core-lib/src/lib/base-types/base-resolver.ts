import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

export interface BaseResolver extends OnInit {
	route: ActivatedRoute;
	resolveRoute(): void;
}