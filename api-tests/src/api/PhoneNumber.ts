import { Type } from "./Type";

export class PhoneNumber{
    number: string;
    prefix: string;
    phoneNumberType: Type;

    constructor(number: string, prefix: string, type: Type){
        this.number = number;
        this.prefix = prefix;
        this.phoneNumberType = type;
    }
}