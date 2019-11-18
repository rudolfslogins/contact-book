import _ from 'lodash'
import { ContactApi } from '../src/ContactApi';
import { TCONTACTONE } from './fixture';

describe("Concurrency Handling", () => {

    beforeEach(() => ContactApi.clearAllContacts());

    it
    ("should handle concurrent adding & deleting", async done => {
        await Promise.all(_.range(0, 100).map(async () => {
            const response = await ContactApi.addContact(TCONTACTONE)
            if (response.status === 201) {
                await ContactApi.deleteContact(response.data.id)
            }
        }));
        let resp = await ContactApi.fetchAllContacts();
        expect(resp.data).toEqual([]);

        done()
    }, 60000);
});
