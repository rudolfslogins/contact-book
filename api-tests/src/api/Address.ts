import { Type } from "./Type";

export class Address{
    fullAddress: string;
    addressType: Type;

    constructor(address: string, type: Type){
        this.fullAddress = address;
        this.addressType = type;
    }
}