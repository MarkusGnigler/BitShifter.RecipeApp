

### Layout Possibilities
* in file lib/styles/base/_flex.scss
```scss
.fx = display: flex;
.fx-row = flex-direction: row;
.fx-col = flex-direction: column;
.fx-spacer = flex: 1 1 auto;
```

### W - H Possibilities
* in file lib/styles/base/_general_.scss
```scss
.full-width = width: 100;
.full-height = height: 100;
```

### Color Possibilities
* in file lib/styles/base/_colors_.scss
```scss
// Text color
.text-success
.text-info
.text-warning
.text-error

// Background color
.bg-success
.bg-info
.bg-warning
.bg-error

// Border color
.border-success
.border-info
.border-warning
.border-error
```
##### Color PossibilitiesExample
```ts
<mat-card
	[class.border-warning]="true"
	[class.border-success]="false"
	[class.border-error]="false"
>
```
