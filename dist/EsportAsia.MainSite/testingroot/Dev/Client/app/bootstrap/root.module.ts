import { HomeComponent } from './home.component';
import { RootRoutingModule } from './root-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http'




import { NgModule, Provider, APP_INITIALIZER } from '@angular/core';
import { RootComponent } from "@_bootstrap/root.component";

@NgModule({
    declarations: [
        RootComponent,
        HomeComponent
    ],
    imports: [
         BrowserModule,
         BrowserAnimationsModule,
        RootRoutingModule,
        RouterModule,
        HttpModule
    ],
    providers: [

    ],
    bootstrap: [RootComponent]
})
export class RootModule {

}
