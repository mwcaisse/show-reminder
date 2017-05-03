import { Component, OnInit } from '@angular/core';

import { Show } from "./show";
import { TrackedShow } from "./tracked-show";
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

    trackedShows: TrackedShow[] = [];

    trackedShowsExpanded: TrackedShow[] = [];

    searchTerms: string;

    selectedShow: Show;

    collapsed = false;

    onSelect(show: Show): void {
        this.selectedShow = show;
    };

    searchShows(): void {
        this.showService.searchShows(this.searchTerms).then(shows => this.shows = shows);
    }

    ngOnInit(): void {
        this.refresh();
    }

    toggleCollapsed(): void {
        this.collapsed = !this.collapsed;
    }

    addShow(show: Show): void {
        this.showService.addShow(show.id).then(savedShow => this.trackedShows.push(savedShow));
    }

    getAllShows(): void {
        this.showService.getAll().then(savedShows => this.trackedShows = savedShows);
    }

    refresh(): void {
        this.getAllShows();
    }

    deleteShow(show: TrackedShow): void {
        this.showService.delete(show.id).then(res  => {
            if (res) {
                var index = this.trackedShows.indexOf(show);
                this.trackedShows.splice(index, 1);
            } else {
                alert("Failed to delete show...");
            }
        });
    }

    toggleShowExpanded(show: TrackedShow): void {
        var index = this.trackedShowsExpanded.indexOf(show);
        if (index !== -1) {
            this.trackedShowsExpanded.splice(index, 1);
        } else {
            this.trackedShowsExpanded.push(show);
        }
    }
}
