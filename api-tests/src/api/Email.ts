import { Type } from "./Type";

export class Email {
    emailName: string;
    emailType: Type;

constructor(name: string, type: Type){
    this.emailName = name;
    this.emailType = type;
}

}