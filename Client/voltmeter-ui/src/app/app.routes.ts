import { Routes } from '@angular/router';
import { Layout } from './layout/layout';
import { Home } from './pages/home/home';
import { InvoiceList } from './pages/invoices/invoice-list';
import { MunicipalTaxList } from './pages/municipal-tax-list/municipal-tax-list';
import { NotFound } from './pages/not-found/not-found';

export const routes: Routes = [
    {
        path: "",
        component: Layout,
        children: [
            {
                path:"",
                component: Home
            },
            {
                path:"invoices",
                component: InvoiceList
            },
            {
                path:"municipal-taxes",
                component: MunicipalTaxList
            }
        ]
    },
    {
        path:"**",
        component: NotFound
    }
];