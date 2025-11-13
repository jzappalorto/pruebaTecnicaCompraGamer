import { Routes } from '@angular/router';
import { MicrosComponent } from './micros/micros.component';
import { ChoferesComponent } from './choferes/choferes.component';
import { ChicosComponent } from './chicos/chicos.component';

export const appRoutes: Routes = [
  { path: 'micros', component: MicrosComponent },
  { path: 'choferes', component: ChoferesComponent },
  { path: 'chicos', component: ChicosComponent },
  { path: '', redirectTo: '/micros', pathMatch: 'full' },
  { path: '**', redirectTo: '/micros', pathMatch: 'full' }
];
