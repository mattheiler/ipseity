import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard } from "./auth"

import { UnauthorizedComponent } from "./unauthorized/unauthorized.component";
import { ForbiddenComponent } from "./forbidden/forbidden.component";

const routes: Routes = [
    {
        path: "",
        pathMatch: "full",
        redirectTo: "home"
    },
    {
        path: "home",
        loadChildren: () => import("./home").then(module => module.HomeModule),
        canLoad: [AuthGuard],
        data: { hasLayout: true }
    },
    {
        path: "forbidden",
        component: ForbiddenComponent,
        canActivate: [AuthGuard]
    },
    {
        path: "unauthorized",
        component: UnauthorizedComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { paramsInheritanceStrategy: "always" })],
    exports: [RouterModule]
})
export class AppRoutingModule {
}