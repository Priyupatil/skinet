import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Action } from 'rxjs/internal/scheduler/Action';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { BreadcrumbService } from 'xng-breadcrumb';
//import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute,
    private bcService: BreadcrumbService)
  {  }

  ngOnInit(): void {
    this.loadProduct();
    this.bcService.set('@productDetails',this.product.name);
  }

  loadProduct(){
   this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(product=>
    {this.product=product},error =>{
      console.log(error);
    }
    );
  }

}
