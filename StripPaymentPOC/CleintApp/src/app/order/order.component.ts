import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Stripe, loadStripe } from '@stripe/stripe-js';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  private _baseUrl = "https://localhost:7131";
  private stripePromise?: Promise<Stripe | null>;

  order = {
    ProductApiId: "price_1Nchq2IPIiF5QVRNaJ3CHrRG",
    Quantity: 3
  }

  constructor(private http: HttpClient) {}

  async pay() {
    this.stripePromise = loadStripe(environment.stripe_pk);
    const stripe = await this.stripePromise;
    stripe?.redirectToCheckout({ sessionId: 'cs_test_a15dQmMs8kRvv0Fp6h1UUK4I6dOSqwb20snCQnzQtnL28QatmlUqbCnIac' });
    // this._createOrder(this.order).subscribe((response: string) => {

    // });
  }

  private _createOrder(order: any): Observable<string> {
    return this.http.post<string>(`${this._baseUrl}/api/payment/PlaceOrder`, order)
   }
}
