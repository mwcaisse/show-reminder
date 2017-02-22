import { Component, Input } from "@angular/core";
import { Show } from "./show";

@Component({
    selector: "show-detail",
    templateUrl: "app/show-detail.template.html"
})

export class ShowDetailComponent {
    @Input()
    show: Show;  
}