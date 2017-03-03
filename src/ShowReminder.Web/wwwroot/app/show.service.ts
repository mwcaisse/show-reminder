import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/topromise';

import { Show } from "./show";

@Injectable()
export class ShowService {

    constructor(private http: Http) {}

    getShows(): Promise<Show[]> {
        //we have the Shows in an array, so instantly resolve the promise
        //return Promise.resolve(SHOWS);

        return this.http.get("http://localhost:50699/api/show/search?terms=hawaii")
            .toPromise()
            .then(this.extractData);
    };

    private extractData(res: Response) {
        let body = res.json();
        return body.data || {};
    }

}