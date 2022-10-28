import { ProductImage } from "./product-image.model";

export interface Product {
    id: string;
    productGuid: string;
    name: string;
    description: string;
    value: number;
    quantity: number;
    images: ProductImage[];
    isActive: boolean;
    createdAt: Date
}