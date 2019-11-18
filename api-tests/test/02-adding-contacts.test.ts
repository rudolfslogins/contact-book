import { TCONTACTONE, TCONTACTTWO, TFIRSTNAME, TLASTNAME, OTHER_TYPE } from "./fixture";
import { ContactApi } from "../src/ContactApi";

describe("Adding Contacts", () => {
  beforeEach(() => ContactApi.clearAllContacts());

  it("should be able to add contact", async done => {

    const response = await ContactApi.addContact(TCONTACTONE);
    expect(response.status).toEqual(201);
    
    const contact = response.data;

    expect(contact.id).toBeDefined();
    expect(contact.firstName).toBe(TCONTACTONE.firstName);
    expect(contact.lastName).toBe(TCONTACTONE.lastName);
    expect(contact.company).toBe(TCONTACTONE.company);
    expect(contact.notes).toBe(TCONTACTONE.notes);
    expect(contact.birthDate).toBe(TCONTACTONE.birthDate);
    
    expect(contact.addresses.length).toEqual(TCONTACTONE.addresses!.length);
    expect(contact.phoneNumbers.length).toEqual(TCONTACTONE.phoneNumbers!.length);
    expect(contact.emails.length).toEqual(TCONTACTONE.emails!.length);
    
    done();
  });

  it("should return different ids for each contact", async done => {
    const firstContact = (await ContactApi.addContact(TCONTACTONE)).data;
    const secondContact = (await ContactApi.addContact(TCONTACTTWO)).data;

    expect(firstContact.id).not.toBe(secondContact.id);

    done();
  });

  it("should be able to add same contact twice", async done => {
    const response = await ContactApi.addContact(TCONTACTONE);
    expect(response.status).toBe(201);

    const secondResponse = await ContactApi.addContact(TCONTACTONE);
    expect(secondResponse.status).toBe(201);
    
    done();
  });

  it("should not accept wrong values", async done => {
    const requests = [
      {
        firstName: null,
        lastName: null
      },
      {
        firstName: TFIRSTNAME,
        lastName: null
      },
      {
        firstName: null,
        lastName: TLASTNAME
      },
      {
        firstName: "aLdis",
        lastName: TLASTNAME
      },
      {
        firstName: TFIRSTNAME,
        lastName: "bErzins"
      },
      {
        firstName: "A",
        lastName: TLASTNAME
      },
      {
        firstName: TFIRSTNAME,
        lastName: "B"
      },
      {
        firstName: "AldiS",
        lastName: TLASTNAME
      },
      {
        firstName: TFIRSTNAME,
        lastName: "BerzinS"
      },
      {
        firstName: TFIRSTNAME,
        lastName: TLASTNAME,
        emails: [ {
          emailName: "testtest.com",
          emailType: OTHER_TYPE
        } ]
      },
      {
        firstName: TFIRSTNAME,
        lastName: TLASTNAME,
        emails: [ {
          emailName: "test@testcom",
          emailType: OTHER_TYPE
        } ]
      },
      {
        firstName: TFIRSTNAME,
        lastName: TLASTNAME,
        emails: [ {
          emailName: "test@test.`com",
          emailType: OTHER_TYPE
        } ]
      },
      {
        firstName: TFIRSTNAME,
        lastName: TLASTNAME,
        phoneNumbers: [ {
          number: "2264595",
          prefix: "+371",
          phoneNumberType: OTHER_TYPE
        } ]
      },
      {
        firstName: TFIRSTNAME,
        lastName: TLASTNAME,
        phoneNumbers: [ {
          number: "226459543",
          prefix: "+371",
          phoneNumberType: OTHER_TYPE
        } ]
      },
      {
        firstName: TFIRSTNAME,
        lastName: TLASTNAME,
        phoneNumbers: [ {
          number: "2264595a",
          prefix: "+371",
          phoneNumberType: OTHER_TYPE
        } ]
      },
      {
        firstName: TFIRSTNAME,
        lastName: TLASTNAME,
        phoneNumbers: [ {
          number: "22645954",
          prefix: "+3712",
          phoneNumberType: OTHER_TYPE
        } ]
      },
      {
        firstName: TFIRSTNAME,
        lastName: TLASTNAME,
        phoneNumbers: [ {
          number: "22645954",
          prefix: "*371",
          phoneNumberType: OTHER_TYPE
        } ]
      }
    ];

    await Promise.all(
      requests.map(async it => {
        try {
          await ContactApi.addContact(it as any);
          done.fail(`No error was thrown when adding ${JSON.stringify(it)}`);
        } catch (e) {
          expect(e.response.status).toBe(400);
        }
       })
    );
    done();
  });
});
