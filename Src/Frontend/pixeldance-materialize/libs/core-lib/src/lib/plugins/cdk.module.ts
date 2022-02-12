import { NgModule } from '@angular/core';

import { DragDropModule } from '@angular/cdk/drag-drop';
import { PortalModule } from '@angular/cdk/portal';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { LayoutModule } from '@angular/cdk/layout';
import { A11yModule } from '@angular/cdk/a11y';
import { ClipboardModule } from '@angular/cdk/clipboard';

const cdkModules = [
	//
	ScrollingModule,
	DragDropModule,
	PortalModule,
	LayoutModule,
	A11yModule,
	ClipboardModule,
];

@NgModule({
	imports: [cdkModules],
	exports: [cdkModules],
})
export class CdkModule {}
