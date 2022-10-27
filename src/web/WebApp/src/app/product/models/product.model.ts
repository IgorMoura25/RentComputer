export interface Product {
    id: string;
    productGuid: string;
    name: string;
    description: string;
    value: number;
    quantity: number;
    imageName: string;
    imageBase64: string;
    isActive: boolean;
    createdAt: Date
}