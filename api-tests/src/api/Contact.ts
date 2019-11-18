import { Address } from "./Address";
import { PhoneNumber } from "./PhoneNumber";
import { Email } from "./Email";

export interface Contact{
    id: number;
    firstName: string;
    lastName: string;
    company: string;
    notes: string;
    birthDate: string;
    addresses: Address[];
    phoneNumbers: PhoneNumber[];
    emails: Email[];
}