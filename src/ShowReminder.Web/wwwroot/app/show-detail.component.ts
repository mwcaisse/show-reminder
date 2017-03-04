import { Component, Input, OnInit } from "@angular/core";
import { Show } from "./show";

import { ShowService } from "./show.service";

@Component({
    selector: "show-detail",
    templateUrl: "app/show-detail.template.html",
    providers: [ShowService]
})

export class ShowDetailComponent {

    constructor(private showService: ShowService) { }

    private _id = -1;

    @Input()
    set id(id: number) {
        this._id = id;
        //fetch the show when a valid id is set
        if (this._id !== -1) {
            this.fetchShow();
        }
    }

    get id(): number {
        return this._id;
    };

    show: Show;

    fetchShow(): void {
        this.showService.getShow(this.id).then(show => this.show = show);
  
    };

}