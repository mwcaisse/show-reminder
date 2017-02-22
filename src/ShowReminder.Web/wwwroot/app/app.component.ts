import { Component } from '@angular/core';
import { Show } from "./show";


const SHOWS: Show[] = [
    {id: 1, name: "Brooklyn Nine Nine"},
    {id: 2, name: "Designated Survivor"},
    {id: 3, name: "Agents of Shield"},
    {id: 4, name: "Hawaii Five O"},
    {id: 5, name: "Lucifer"},
    {id: 6, name: "Quantico"}
];

@Component({
    selector: 'my-app',
    templateUrl: "app/TestTemplate.html",
    styleUrls: ["app/TestStyles.css"]
})
export class AppComponent {
    shows = SHOWS;

    selectedShow: Show;

    onSelect(show: Show): void {
        this.selectedShow = show;
    };
}
