import { TCONTACTTWO, SHORT_CONT } from "./fixture";
import { ContactApi } from "../src/ContactApi";

describe("Airport Typeahead", () => {
    beforeEach(() => ContactApi.clearAllContacts());

    it("should search by incomplete phrases", async done => {

        const phrases = [
            "KARLIS",
            "karlis",
            " KArlis",
            "KAR ",
            "Berz",
            " beRz",
            "Berzin",
            "swed",
            " WEDBAnk",
            "223366",
            "solomon"

        ];

        await Promise.all([
            await ContactApi.addContact(TCONTACTTWO),
            await ContactApi.addContact(TCONTACTTWO),
            await ContactApi.addContact(SHORT_CONT)
        ])

        const responses = await Promise.all(phrases.map(it => ContactApi.searchContacts(it)))

        responses.forEach(res => {
            expect(res.status).toBe(200);
            res.data.forEach(element => {
                expect(element.firstName).toEqual(TCONTACTTWO.firstName);
                expect(element.lastName).toEqual(TCONTACTTWO.lastName);
                expect(element.company).toEqual(TCONTACTTWO.company);
                expect(element.notes).toEqual(TCONTACTTWO.notes);
                expect(element.birthDate).toEqual(TCONTACTTWO.birthDate);
            });
        })
        done()
    });
});
