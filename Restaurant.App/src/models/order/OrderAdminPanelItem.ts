import { OrderAdminPanelElement } from './OrderAdminPanelElement';

export interface OrderAdminPanelItem {
  id: number;
  email: string;
  name: string;
  surname: string;
  city: string;
  address: string;
  phoneNumber: string;
  status: string;
  statusTag: string;
  orderDate: Date;
  orderElements: OrderAdminPanelElement[];
}
