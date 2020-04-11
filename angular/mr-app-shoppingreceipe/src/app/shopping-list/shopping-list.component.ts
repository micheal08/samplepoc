import { Component, OnInit } from '@angular/core';
import { Ingredient } from '../shared/ingredient.model';

@Component({
  selector: 'app-shopping-list',
  templateUrl: './shopping-list.component.html',
  styleUrls: ['./shopping-list.component.css']
})
export class ShoppingListComponent implements OnInit {
ingredients: Ingredient[] = [
  new Ingredient('Onion', 14),
  new Ingredient('Tomato', 5),
  new Ingredient('Potato', 8),
  new Ingredient('Carrot', 6),
  new Ingredient('Beans', 4)
];
  constructor() { }

  ngOnInit(): void {
  }

}
