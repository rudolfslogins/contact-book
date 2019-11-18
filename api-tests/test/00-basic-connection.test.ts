import { ContactApi } from "../src/ContactApi";

describe("Basic access", () => {

  it("should be able to access all contacts", async done => {

      let req = await ContactApi.fetchAllContacts();
      expect(req.status).toBe(200);
      done();
  });

});
