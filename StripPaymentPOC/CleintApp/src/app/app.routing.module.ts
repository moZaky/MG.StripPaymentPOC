import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { OrderComponent } from './order/order.component';
import { OrderSuccessComponent } from './order-success/order-success.component';
import { OrderCancelComponent } from './order-cancel/order-cancel.component';

@NgModule({
  declarations: [ 
    OrderComponent,
OrderSuccessComponent,
OrderCancelComponent,
  ],
  imports: [
    RouterModule.forRoot([
        { path: 'order', component: OrderComponent },
        { path: 'success', component: OrderSuccessComponent },
        { path: 'canceled', component: OrderCancelComponent },
        { path: '**', redirectTo: 'order' }
  ])
  ],
  exports: [
    RouterModule,
  ],
  providers: [],

})
export class AppRoutingModule {}


