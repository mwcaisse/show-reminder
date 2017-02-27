import { Injectable } from "@angular/core";

import { Show } from "./show";
import { SHOWS } from "./mock-shows";

@Injectable()
export class ShowService {

    getShows(): Promise<Show[]> {
        //we have the Shows in an array, so instantly resolve the promise
        return Promise.resolve(SHOWS);
    };

}