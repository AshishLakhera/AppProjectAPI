"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var layout_component_1 = require("./layout/layout.component");
var login_component_1 = require("./login/login.component");
exports.appRoutes = [
    { path: '', component: login_component_1.LoginComponent, pathMatch: 'full' },
    { path: 'layout', component: layout_component_1.LayoutComponent }
];
exports.Routing = router_1.RouterModule.forRoot(exports.appRoutes);
//# sourceMappingURL=app.routing.js.map