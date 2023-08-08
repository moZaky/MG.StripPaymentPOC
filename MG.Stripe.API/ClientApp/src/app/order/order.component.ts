import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Stripe, loadStripe } from '@stripe/stripe-js';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  private _baseUrl = "";
  private stripePromise?: Promise<Stripe | null>;

  order = {
    ProductApiId: "price_1Nchq2IPIiF5QVRNaJ3CHrRG",
    Quantity: 3
  }

  constructor(private http: HttpClient,@Inject('BASE_URL') baseUrl: string) {
    this._baseUrl=baseUrl;
  }


  async pay() {
    this.stripePromise = loadStripe(environment.stripe_pk);
    debugger
    const stripe = await this.stripePromise;
    this._createOrder(this.order).subscribe(
      {

        next:(response: string) => {
          debugger;
      stripe?.redirectToCheckout({ sessionId: response });
    },
    complete:()=>{
      console.log("complete")
    },
    error:(error:any) => {
      console.error(error);
    }});
  }

  private _createOrder(order: any): Observable<string> {
    return this.http.post<string>(this._baseUrl+"payment/PlaceOrder", order)
   }
}
