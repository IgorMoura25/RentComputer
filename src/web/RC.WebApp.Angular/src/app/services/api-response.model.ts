import { Product } from "./catalog/product.model"

export class ApiResponse {
    public title : string
    public status : number
    public data : any
    public errors : string[]
}