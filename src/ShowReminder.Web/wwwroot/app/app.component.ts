import { Component } from '@angular/core';
import { OnInit } from "@angular/core";

import { Show } from "./show";
import { ShowService } from "./show.service";

@Component({
    selector: 'my-app',
    templateUrl: "app/TestTemplate.html",
    styleUrls: ["app/TestStyles.css"],
    providers: [ShowService]
})
export class AppComponent implements OnInit {

    constructor(private showService: ShowService) {}

    shows: Show[];

    selectedShow: Show;

    onSelect(show: Show): void {
        this.selectedShow = show;
    };

    getShows(): void {
        this.showService.getShows().then(shows => this.shows = shows);
    };

    ngOnInit(): void {
        this.getShows();
    }

}
