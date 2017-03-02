import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

import { AppComponent } from './app.component';
import { ShowDetailComponent } from "./show-detail.component";

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        JsonpModule
    ],
    declarations: [
        AppComponent,
        ShowDetailComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
