import { IngredientViewModel } from '../ingredient/IngredientViewModel';

export interface MealViewModel {
  id: number;
  name: string;
  price: number;
  ingredients: IngredientViewModel[];
}
