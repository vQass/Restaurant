import { OrderElement } from './OrderElement';

export interface OrderAddRequest {
  userId: number;
  cityId: number;
  name: string;
  surname: string;
  address: string;
  phoneNumber: string;
  orderElements: OrderElement[];
}
