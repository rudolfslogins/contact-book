import { Address } from "./Address";
import { PhoneNumber } from "./PhoneNumber";
import { Email } from "./Email";

export class ContactRequest{
    firstName: string;
    lastName: string;
    company?: string;
    notes?: string;
    birthDate?: string;
    addresses?: Address[];
    phoneNumbers?: PhoneNumber[];
    emails?: Email[];


    constructor(
        firstName: string, 
        lastName: string,
        company?: string, 
        notes?: string, 
        birthDate?: string, 
        addresses?: Address[], 
        phoneNumbers?: PhoneNumber[], 
        emails?: Email[]){
            this.firstName = firstName;
            this.lastName = lastName;
            this.company = company;
            this.notes = notes;
            this.birthDate = birthDate;
            this.addresses = addresses;
            this.phoneNumbers = phoneNumbers;
            this.emails = emails;

        }
}