import { Ingredient } from '../ingredient/Ingredient';

export interface MealViewModel {
  id: number;
  name: string;
  price: number;
  ingredients: Ingredient[];
}
