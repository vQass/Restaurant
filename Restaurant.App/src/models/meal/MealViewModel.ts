import { IngredientViewModel } from '../Ingredient/IngredientViewModel';

export interface MealViewModel {
  name: string;
  price: number;
  ingredients: IngredientViewModel[];
}
