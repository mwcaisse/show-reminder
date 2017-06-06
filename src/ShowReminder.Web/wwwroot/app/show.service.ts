import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/topromise';

import { Show } from "./show";
import { TrackedShow } from "./tracked-show";

@Injectable()
export class ShowService {

    private baseUrl: string = "http://localhost:50699/";

    constructor(private http: Http) {}

    searchShows(terms: string): Promise<Show[]> {
        return this.http.get(this.baseUrl + "/show/search?terms=" + terms)
            .toPromise()
            .then(this.extractData);
    };

    getShow(id: number): Promise<Show> {
        return this.http.get(this.baseUrl + "show/" + id)
            .toPromise()
            .then(this.extractData);
    };

    addShow(id: number): Promise<TrackedShow> {
        return this.http.post(this.baseUrl + "show/tracked/add/" + id, {})
            .toPromise()
            .then(this.extractData);
    };

    getAll(): Promise<TrackedShow[]> {
        return this.http.get(this.baseUrl + "show/tracked/")
            .toPromise()
            .then(this.extractData);
    };

    delete(id: number): Promise<boolean> {
        return this.http.delete(this.baseUrl + "show/tracked/" + id)
            .toPromise()
            .then(this.extractData);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body.data || {};
    };

}