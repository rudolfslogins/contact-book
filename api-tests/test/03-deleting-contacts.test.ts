import { TCONTACTTWO } from "./fixture";
import { ContactApi } from "../src/ContactApi";

describe("Delete Contacts", () => {
  beforeEach(() => ContactApi.clearAllContacts());

  it("should be able to delete contact", async done => {
    const contact = (await ContactApi.addContact(TCONTACTTWO)).data;
    const response = await ContactApi.deleteContact(contact.id);
    expect(response.status).toBe(200);

    try {
      await ContactApi.fetchContact(contact.id);
      done.fail();
    } catch (e) {
      expect(e.response.status).toBe(404);
    }
    done();
  });

  it("should not fail on missing flight", async done => {
    const response = await ContactApi.deleteContact(55);
    expect(response.status).toBe(200);

    done();
  });
});
