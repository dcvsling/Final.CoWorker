import { HomeComponent } from './home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule, CanActivateChild } from '@angular/router';





let routes: Routes = [
    {
		path: '',
        data: {
            title: 'Home'
        },
        children: [
            {
                path: '',component:HomeComponent
             //   loadChildren: '../models/home/home.module#HomeModule'
            },
            { path: 'home', redirectTo: '', pathMatch: 'full' },
        ]
    },

    { path: '**', redirectTo: 'error_page', pathMatch: 'full' }//暫時用不到
];
/*
Object.getOwnPropertyNames(routing).forEach((val, idx, array) => {
    let a = new routing[val];
    (!a.constructor.routePath && !a.constructor.parentRoute) || (routes1.unshift(a.constructor.routePath), a.done = true);
    //console.log(a);
});
let routes: Routes = routes1*/

/*
//error https://github.com/angular/angular-cli/issues/3674
import * as component from '#models/head/index';
import { RouteJson }  from '#routing/route.sevice';
let routes1: Routes = [
        { path: '**', redirectTo: '', pathMatch: 'full' }
    ];

let routes=new RouteJson().getRouteJson(routes1);
*/
@NgModule({
    imports: [
         RouterModule.forRoot(routes, { useHash: true }),
        //RouterModule.forRoot(routes),
    
    ],
    exports: [
        RouterModule,
    ],
    providers: []
})
export class RootRoutingModule { }
