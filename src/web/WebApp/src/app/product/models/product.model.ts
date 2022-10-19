export interface Product {
    id: string;
    productGuid: string;
    name: string;
    description: string;
    value: number;
    quantity: number;
    isActive: boolean;
    createdAt: Date
}