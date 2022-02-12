import { Pipe, PipeTransform } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Pipe({
	name: 'toDataSource',
})
export class ToDataSourcePipe<T> implements PipeTransform {
	//
	transform(items: never): MatTableDataSource<T> {
		return new MatTableDataSource<T>(items);
	}
}
