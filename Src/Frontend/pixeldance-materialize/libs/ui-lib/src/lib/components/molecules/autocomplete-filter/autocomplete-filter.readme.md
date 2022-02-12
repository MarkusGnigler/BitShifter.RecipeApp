### Usage

With FROMGROUP map
```html
<ui-autocomplete-filter
	label="CodeType"
	[parentForm]="entityFormGroup"
	[formControlName]="FORMGROUP_NAMES.NAME"
	[possibleOptions]="possibleEntities"
></ui-autocomplete-filter>
```

Without FROMGROUP map
```html
<ui-autocomplete-filter
	label="CodeType"
	[parentForm]="entityFormGroup"
	formControlName="name"
	[possibleOptions]="possibleEntities"
></ui-autocomplete-filter>
```
