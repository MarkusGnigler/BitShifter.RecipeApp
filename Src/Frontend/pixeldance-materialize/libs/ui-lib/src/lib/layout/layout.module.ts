import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { MaterializeModule } from '../plugins/materialize.module';
import { FlexLayoutModule } from '@angular/flex-layout';

import { FormsModule } from '@angular/forms';
import { TabBarComponent } from './components/tab-bar/tab-bar.component';
import { TopbarComponent } from './components/topbar/topbar.component';
import { CoreLibModule } from '@pixeldance-materialize/core-lib';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { BaseViewComponent } from './components/base-view.component';
import { BackButtonDirective } from './directives/back-button.directive';
import { ThemeContainerComponent } from './components/theme-container/theme-container.component';

const layoutComponents = [
	AdminPanelComponent,
	SidenavComponent,
	TopbarComponent,
	TabBarComponent,
	BaseViewComponent,
	ThemeContainerComponent,
];

@NgModule({
	declarations: [layoutComponents, BackButtonDirective],
	exports: [layoutComponents],
	imports: [
		//
		CommonModule,
		FormsModule,
		RouterModule,
		CoreLibModule,
		MaterializeModule,
		FlexLayoutModule,
	],
})
export class LayoutModule {}
