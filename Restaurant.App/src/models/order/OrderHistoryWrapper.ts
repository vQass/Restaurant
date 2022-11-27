import { OrderHistoryItem } from './OrderHistoryItem';

export interface OrderHistoryWrapper {
  items: OrderHistoryItem[];
  itemCount: number;
}
