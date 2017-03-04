import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/topromise';

import { Show } from "./show";

@Injectable()
export class ShowService {

    constructor(private http: Http) {}

    searchShows(terms: string): Promise<Show[]> {
        return this.http.get("http://localhost:50699/api/show/search?terms=" + terms)
            .toPromise()
            .then(this.extractData);
    };

    getShow(id: number): Promise<Show> {
        return this.http.get("http://localhost:50699/api/show/" + id)
            .toPromise()
            .then(this.extractData);
    };

    private extractData(res: Response) {
        let body = res.json();
        return body.data || {};
    };

}