import { ContactApi } from "../src/ContactApi";
import { TCONTACTONE } from "./fixture";

describe("Testing", () => {
  it("should clear all contacts", async done => {
    try{
      const req = await ContactApi.addContact(TCONTACTONE);
      expect(req.status).toBe(201);

      const response = await ContactApi.clearAllContacts();
      expect(response.status).toBe(200);
      expect(response.data).toEqual(`1 contacts deleted!`);
      done(); 

    }catch(e){
      console.log(e.response.status, e.response.data);
      expect(e.response.status).toBe(200);
    }
  });
});
