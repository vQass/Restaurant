import { OrderHistoryElement } from './OrderHistoryElement';

export interface OrderHistoryItem {
  name: string;
  surname: string;
  city: string;
  address: string;
  phoneNumber: string;
  status: string;
  statusTag: string;
  orderDate: Date;
  orderElements: OrderHistoryElement[];
}
