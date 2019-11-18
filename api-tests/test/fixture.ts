import { Type } from "../src/api/Type";
import { ContactRequest } from "../src/api/ContactRequest";
import { Email } from "../src/api/Email";
import { Address } from "../src/api/Address";
import { PhoneNumber } from "../src/api/PhoneNumber";

export const TFIRSTNAME = "Aldis";
export const TLASTNAME = "Berzups";

export const HOME_TYPE = new Type("Home");
export const WORK_TYPE = new Type("Work");
export const MOBILE_TYPE = new Type("Mobile");
export const OTHER_TYPE = new Type("Other");

export const TEST_ADDRESS = new Address("Andromedas Gatve 1, Riga", HOME_TYPE);
export const TEST_ADDRESS1 = new Address("Motoru 7, Liepaja", WORK_TYPE);
export const TEST_ADDRESS2 = new Address("Maskavas 250, Riga", OTHER_TYPE);
export const TEST_ADDRESS3 = new Address("Salaspils / Darza 8, Riga", WORK_TYPE);
export const TADDRESSES: Address[] = [TEST_ADDRESS, TEST_ADDRESS1];

export const TEST_PHONE = new PhoneNumber("25354677", "+371", MOBILE_TYPE);
export const TEST_PHONE1 = new PhoneNumber("22336688", "+371", HOME_TYPE);
export const TEST_PHONE2 = new PhoneNumber("62997442", "+371", WORK_TYPE);
export const TEST_PHONE3 = new PhoneNumber("92663377", "+371", OTHER_TYPE);
export const TPHONES: PhoneNumber[] = [TEST_PHONE, TEST_PHONE1];

export const TEST_EMAIL = new Email ("marcs@msn.com", OTHER_TYPE);
export const TEST_EMAIL1 = new Email ("solomon@yahoo.ca", WORK_TYPE);
export const TEST_EMAIL2 = new Email ("empathy@me.com", MOBILE_TYPE);
export const TEST_EMAIL3 = new Email ("yruan@verizon.net", HOME_TYPE);
export const TEMAILS: Email[] = [TEST_EMAIL, TEST_EMAIL1];

export const TCONTACTONE = new ContactRequest(
    "Janis", "Kalnins", "Rimi Latvia", "Neighbour", "1990-02-01", [TEST_ADDRESS], [TEST_PHONE], [TEST_EMAIL]  
);
export const TCONTACTTWO = new ContactRequest(
    "Karlis", "Berzins", "AS Swedbank", "School friend", "1988-01-01", [TEST_ADDRESS1], [TEST_PHONE1], [TEST_EMAIL1]  
);

export const SHORT_CONT = new ContactRequest("Sigurds", "Lapins");
    