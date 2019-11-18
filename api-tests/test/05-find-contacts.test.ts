import { TFIRSTNAME, TCONTACTONE } from "./fixture";
import { ContactApi } from "../src/ContactApi";

describe("Finding Contacts", () => {
    beforeEach(() => ContactApi.clearAllContacts());

    it("should return no results when nothing found", async done => {

        const response = await ContactApi.searchContacts(TFIRSTNAME);

        expect(response.status).toBe(200);
        expect(response.data).toEqual([]);

        done()
    });

    it("should be able to find contact by id", async done => {
        const addedContact = (await ContactApi.addContact(TCONTACTONE)).data;

        const response = await ContactApi.fetchContact(addedContact.id);

        expect(response.status).toBe(200)
        expect(response.data.firstName).toEqual(response.data.firstName);
        expect(response.data.lastName).toEqual(response.data.lastName);

        done()
    });

    it("should not find anything when non existing contact id passed", async done => {
        try {
            await ContactApi.fetchContact(666)
            done.fail()
        } catch (e) {
            expect(e.response.status).toBe(404)
            done()
        }
    });
});
