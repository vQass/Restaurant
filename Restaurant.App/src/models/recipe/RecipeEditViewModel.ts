import { RecipeIngredient } from './RecipeIngredient';

export interface RecipeEditViewModel {
  mealId: number;
  mealName: string;
  ingredients: RecipeIngredient[];
}
