import { AxiosResponse } from "axios";
import { baseClient } from "./axios";
import { ContactRequest } from "./api/ContactRequest";
import { Contact } from "./api/Contact";


export class ContactApi {
    static async addContact(req: ContactRequest): Promise<AxiosResponse<Contact>>{
        return baseClient.put(`/contacts`, req);
    }


    static async fetchContact(id: number): Promise<AxiosResponse<Contact>>{
        return baseClient.get(`/contacts/${id}`);
    }
    static async searchContacts(search: string): Promise<AxiosResponse<Contact[]>>{
        return baseClient.get(`/contacts`, { params: { search } });
    }
    static async fetchAllContacts(): Promise<AxiosResponse<Contact[]>>{
        return baseClient.get(`/contacts`);
    }



    static async deleteContact(id: number): Promise<AxiosResponse<void>>{
        return baseClient.delete(`/contacts/${id}`);
    }
    static async clearAllContacts(): Promise<AxiosResponse<void>>{
        return baseClient.delete(`/contacts/clearall`);
    }
}

