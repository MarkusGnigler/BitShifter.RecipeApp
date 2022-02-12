### Usage

With FROMGROUP map
```html
<pxd-autocomplete-filter
	label="CodeType"
	[parentForm]="entityFormGroup"
	[formControlName]="FORMGROUP_NAMES.NAME"
	[possibleOptions]="possibleEntities"
></pxd-autocomplete-filter>
```

Without FROMGROUP map
```html
<pxd-autocomplete-filter
	label="CodeType"
	[parentForm]="entityFormGroup"
	formControlName="name"
	[possibleOptions]="possibleEntities"
></pxd-autocomplete-filter>
```
