export interface Product {
    id: string;
    guid: string;
    name: string;
    description: string;
    value: number;
    quantity: number;
    isActive: boolean;
    createdAt: Date
}