import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Recipe } from '../recipe.model';

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit {
  @Output() recipeWasSelected = new EventEmitter<Recipe>();

recipes: Recipe[] = [
  new Recipe('Fresh Recipe', 'This is a test recipe',
  'https://img.hellofresh.com/f_auto,fl_lossy,q_auto,w_610/hellofresh_s3/image/5dcc139c96d0db43857c2eb3-a12c2ae7.jpg'),
  new Recipe('Fresh Recipe 1', 'This is a hello fresh recipe',
  'https://img.hellofresh.com/f_auto,fl_lossy,q_auto,w_610/hellofresh_s3/image/5dcc139c96d0db43857c2eb3-a12c2ae7.jpg')
];
  constructor() { }

  ngOnInit(): void {
  }

  onRecipeSelected(recipe: Recipe) {
    this.recipeWasSelected.emit(recipe);
  }

}
