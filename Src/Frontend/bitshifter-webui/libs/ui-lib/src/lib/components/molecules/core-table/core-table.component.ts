import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { isObjectEmpty } from '@bitshifter-webui/core-lib';

const COLUMN_TEMPLATE = 'columnTemplate';
// const SELECTED_TEMPLATE = 'selectedTemplate';
const ACTION_TEMPLATE = 'actionTemplate';

export interface TableTemplate {
	header: string;
	template: TemplateRef<HTMLElement>;
}

@Component({
	selector: 'ui-core-table',
	templateUrl: './core-table.component.html',
	styleUrls: ['./core-table.component.scss'],
})
export class CoreTableComponent<T> implements OnInit {
	//
	@Input() items!: T[];
	@Input() displayedColumns: string[] | undefined;

	@Input() beforeTemplates: TableTemplate[] | undefined;
	// @Input() afterTemplates!: TableTemplate[];

	@Input(COLUMN_TEMPLATE)
	columnTemplate?: TemplateRef<HTMLElement>;

	@Input(ACTION_TEMPLATE)
	actionTemplate?: TemplateRef<HTMLElement>;

	@Output() rowClicked = new EventEmitter<T>();

	readonly _displayedColumns: string[] = [];

	ngOnInit(): void {
		this.addBeforeTemplate();
		this.addActionsColumn();
		this.generateDisplayedColumns();
		//For order
		this.displayedColumns?.forEach(item => {
			this._displayedColumns.push(item);
		});
	}

	private addBeforeTemplate() {
		if (!this.beforeTemplates) return;
		this.beforeTemplates.forEach(bf => {
			this._displayedColumns.push(bf.header);
		});
	}

	private addActionsColumn() {
		if (this.actionTemplate) this.displayedColumns?.push('Aktionen');
	}

	private generateDisplayedColumns() {
		if (!isObjectEmpty(this.displayedColumns)) return;
		this.displayedColumns = Object.keys(this.items[0]);
		// this.displayedColumns = Object.keys(this.items.data[0]);
	}

	getTemplate(columnName: string) {
		return this.beforeTemplates?.find(x => x.header == columnName)?.template;
	}
}
