export interface AuthResult {
  token: string;
  email: string;
  firstName: string;
  lastName: string;
}

export interface UserProfile {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  createdAt: string;
  loyaltyBalance: number;
}

export interface Product {
  id: number;
  name: string;
  categoryId: number;
  categoryName: string;
  price: number;
  pointsAwarded: number;
}

export interface TableInfo {
  id: number;
  tableNumber: string;
  capacity: number;
  hasOutlet: boolean;
  areaId: number;
  areaName: string;
}

export interface Reservation {
  id: string;
  tableId: number;
  tableNumber: string;
  areaName: string;
  startTime: string;
  endTime: string;
  status: string;
}

export interface OrderResult {
  orderId: string;
  totalAmount: number;
  pointsEarned: number;
  createdAt: string;
}

export interface LoyaltyWallet {
  balance: number;
  lastUpdate: string;
}

export interface PointTransaction {
  id: string;
  amount: number;
  transactionType: string;
  createdAt: string;
}

export interface CartItem {
  productId: number;
  name: string;
  price: number;
  quantity: number;
}

export interface User {
  email: string;
  firstName: string;
  lastName: string;
}

export interface Promotion {
  id: number;
  title: string;
  description: string;
  imageUrl: string | null;
  discountCode: string | null;
  startDate: string;
  endDate: string;
  isActive: boolean;
}

export interface AreaInfo {
  id: number;
  name: string;
  isQuietZone: boolean;
  tableCount: number;
  hasWifi: boolean;
  hasOutlets: boolean;
}
