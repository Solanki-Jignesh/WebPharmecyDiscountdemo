Explanation of Create Discount Code Functionality
1. Purpose:
I created a form on the admin side to create, update, and delete discount codes with all necessary validation and business logic.

2. Key Features Implemented:

Discount Code Details:
The form captures essential fields like discount code, value, value type (percentage or fixed), minimum cart value, usage limits, applicable customers, start/end dates, and active status.

Usage Limits (Total & Per Customer):
There are two checkboxes for "Unlimited Total Usage" and "Unlimited Per Customer Usage."

When either checkbox is checked, the corresponding input field for usage count is disabled and cleared.

In this case, the backend stores a null value for those fields in the database.

null signifies unlimited usage in the business logic, allowing unlimited discount redemptions.

Applies To Customers:
There is a checkbox for "Applies To All Customers."

When this is checked, the multi-select list of individual customers is disabled and cleared.

In this scenario, an empty array (string[]) is sent to the backend, indicating that the discount applies to all customers.

When unchecked, the admin can select specific customers to apply the discount to.

Date Validation:

The form validates that the end date must be later than the start date using client-side JavaScript.

If validation fails, form submission is prevented and the user is alerted.

3. Benefits of this approach:

User-friendly UI:
Disabling input fields based on checkbox selections prevents invalid or conflicting inputs.

Clear intent in data:
Using null for unlimited usage and empty arrays for "applies to all" simplifies backend logic and makes the data self-explanatory.

Robust validation:
Ensures data integrity before hitting the server, reducing errors.


Perfect 👍
I’ll make one small Angular example project that:

1. Shows Default vs OnPush change detection behavior.


2. Demonstrates loading a component dynamically at runtime using ComponentFactoryResolver + ViewContainerRef.




---

1. Project Structure

We’ll have:

app.component.ts → Main parent, controls everything.

default-cd.component.ts → Uses Default change detection.

onpush-cd.component.ts → Uses OnPush change detection.

dynamic-child.component.ts → Component that we load dynamically.



---

2. Code

default-cd.component.ts

import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-default-cd',
  template: `<p>Default CD: {{ counter }}</p>`,
  changeDetection: ChangeDetectionStrategy.Default
})
export class DefaultCdComponent {
  @Input() counter!: number;
}


---

onpush-cd.component.ts

import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-onpush-cd',
  template: `<p>OnPush CD: {{ counter }}</p>`,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OnPushCdComponent {
  @Input() counter!: number;
}


---

dynamic-child.component.ts

import { Component } from '@angular/core';

@Component({
  selector: 'app-dynamic-child',
  template: `<p>👋 I am a dynamically created component!</p>`
})
export class DynamicChildComponent {}


---

app.component.ts

import { Component, ViewChild, ViewContainerRef, ComponentFactoryResolver } from '@angular/core';
import { DefaultCdComponent } from './default-cd.component';
import { OnpushCdComponent } from './onpush-cd.component';
import { DynamicChildComponent } from './dynamic-child.component';

@Component({
  selector: 'app-root',
  template: `
    <h2>Change Detection Demo</h2>
    <button (click)="increaseCounter()">Increase Counter</button>
    <button (click)="changeObject()">Change Object Ref (For OnPush)</button>

    <app-default-cd [counter]="counter"></app-default-cd>
    <app-onpush-cd [counter]="onPushCounter"></app-onpush-cd>

    <h2>Dynamic Component Demo</h2>
    <button (click)="loadDynamic()">Load Dynamic Component</button>
    <ng-template #dynamicContainer></ng-template>
  `
})
export class AppComponent {
  counter = 0; // for default CD
  onPushCounter = 0; // for OnPush CD

  @ViewChild('dynamicContainer', { read: ViewContainerRef }) container!: ViewContainerRef;

  constructor(private resolver: ComponentFactoryResolver) {}

  increaseCounter() {
    this.counter++; // Will trigger Default CD
    this.onPushCounter++; // Will not trigger OnPush unless reference changes
  }

  changeObject() {
    this.onPushCounter = this.onPushCounter + 1; // OnPush detects this if it's bound to a new value
  }

  loadDynamic() {
    const factory = this.resolver.resolveComponentFactory(DynamicChildComponent);
    this.container.createComponent(factory);
  }
}


---

app.module.ts

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { DefaultCdComponent } from './default-cd.component';
import { OnpushCdComponent } from './onpush-cd.component';
import { DynamicChildComponent } from './dynamic-child.component';

@NgModule({
  declarations: [
    AppComponent,
    DefaultCdComponent,
    OnpushCdComponent,
    DynamicChildComponent
  ],
  imports: [BrowserModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}


---

3. How to Test

1. Run ng new cd-demo and replace the files with above code.


2. Click Increase Counter —

Default CD updates instantly.

OnPush won’t update unless the @Input reference changes.



3. Click Change Object Ref — OnPush now updates.


4. Click Load Dynamic Component — A new component appears without being in the HTML at start.




---

If you want, I can add console logs so you can see exactly when Angular checks each component — that will make the difference between Default & OnPush very clear.

Do you want me to add that debugging part? It will make the example even easier to understand.


