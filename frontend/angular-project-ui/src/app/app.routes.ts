import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ContactsComponent } from './pages/contacts/contacts.component';

import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [

  { path: '', component: HomeComponent },

  { path: 'login', component: LoginComponent },

  {
    path: 'contacts',
    component: ContactsComponent,
    canActivate: [AuthGuard]
  },

  { path: '**', redirectTo: '' }
];