import { Component } from '@angular/core';

export class Show {
    id: number;
    name: string;
}


@Component({
    selector: 'my-app',
    template: `<h1>Hello {{show.name}}</h1>`
})
export class AppComponent {
    show: Show ={
        id: 1,
        name: "Quantico"
    }
}
