<template>
  <form>
    <div>
      <div class="addContactForm">
        <md-button v-on:click="reouteToList" class="md-icon-button">
          <md-icon>keyboard_backspace</md-icon>
        </md-button>
        <md-field>
          <label>Name</label>
            <md-input v-model="formData.firstName" required></md-input>
        </md-field>
        <md-field>
          <label>Surname</label>
            <md-input v-model="formData.lastName" required></md-input>
        </md-field>
        <md-field>
          <label>Work</label>
            <md-input v-model="formData.company"></md-input>
        </md-field>
        <md-field>
          <label>Notes</label>
            <md-input v-model="formData.notes"></md-input>
        </md-field>
        <md-datepicker
          v-model="formData.birthDate" 
          md-immediately
          :md-open-on-focus="false">
            <label>Birthdate</label>
        </md-datepicker>
            
        <AddressComponent 
          v-bind:addresses="formData.addresses"
          v-on:addressUpdated="updateAddresses($event)"></AddressComponent>
        <PhoneComponent 
          v-bind:phoneNumbers="formData.phoneNumbers"
          v-on:phoneUpdated="updatePhoneNumbers($event)"></PhoneComponent>
        <EmailComponent
          v-bind:emails="formData.emails"
          v-on:emailUpdated="updateEmails($event)"></EmailComponent>
      </div>

      <div class="addContact">
        <md-button v-on:click="saveContact" class="md-raised md-primary">Save changes</md-button>
        <md-button v-if="this.proId !== undefined" @click="deleteConfirmation = true" class="md-raised md-primary">Delete Contact</md-button>
          <md-dialog-confirm
            :md-active.sync="deleteConfirmation"
            md-title="Delete contact"
            md-content="Are you sure?<br>This action cannot be undone!"
            md-confirm-text="Proceed"
            md-cancel-text="Discard"
            @md-cancel="onCancel"
            @md-confirm="deleteContact" />
      </div>
    </div>
  </form>
</template>

<script>
import ContactList from "./ContactListPage";
import router from "../router/index";
import EmailComponent from "../components/EmailComponent"
import PhoneComponent from "../components/PhoneComponent"
import AddressComponent from "../components/AddressComponent"
export default {
  name: 'ContactPage',
  components: {
    EmailComponent,
    PhoneComponent,
    AddressComponent
  },
  data(){
    return{
      deleteConfirmation: false,
      proId: this.$route.params.Cid,
      formData: {
          firstName: "",
          lastName: "",
          company: "",
          notes: "",
          birthDate: null,
          addresses: [],
          phoneNumbers: [],
          emails: []
        }
    }
  },
  methods: {
    async saveContact(){
      let request = {
        firstName: this.formData.firstName,
        lastName: this.formData.lastName,
        company: this.formData.company,
        notes: this.formData.notes,
        birthDate: this.formData.birthDate !== null ? this.formData.birthDate.toISOString().slice(0,10) : null,
        addresses: this.formData.addresses,
        phoneNumbers: this.formData.phoneNumbers,
        emails: this.formData.emails
      }
      const promiseLike = 
      (this.proId === undefined ? this.$http.put(`http://localhost:8082/api/contacts`, request) 
      : this.$http.patch(`http://localhost:8082/api/contacts/${this.proId}`, request))
        .then(response => {
        // success
          if(response.status === 201 || response.status === 200){
            //console.log(response.data)
            window.alert(`Contact ${this.formData.firstName} ${this.formData.lastName} saved!`);
            this.reouteToList();
          }
          }, response => {
          //error
          if(response.status === 400) {
            //console.log(response.data)
            window.alert(this.formatMsg(response.data.message));
          }else{
            //console.log(response.data)
            window.alert(this.formatMsg(response.data.message));
          }
          })
      let result = await promiseLike;
      },
      reouteToList(){
        this.$router.push({ name: "list" });
      },
      formatMsg(msg){
        return msg
          .replace(/First Name/g, 'Name')
          .replace(/Last Name/g, 'Surname')
          .replace(/Email Name/g, 'Email')
          .replace(/Number/g, 'Phone Number')
          .replace(/ \| /g, '\n');
      },
      async getClientById(id){
        const promiseLike = this.$http.get(`http://localhost:8082/api/contacts/${this.proId}`);
        let result = await promiseLike;
        this.formData.firstName = result.data.firstName;
        this.formData.lastName = result.data.lastName;
        this.formData.company = result.data.company;
        this.formData.notes = result.data.notes;
        this.formData.birthDate = new Date(result.data.birthDate);
        this.formData.addresses = result.data.addresses;
        this.formData.phoneNumbers = result.data.phoneNumbers;
        this.formData.emails = result.data.emails;
        this.$emit('clientDataReceived', this.formData);
      },
      async deleteContact() {
        if(this.proId === undefined)
          return;
        const promiseLike = this.$http.delete(`http://localhost:8082/api/contacts/${this.proId}`);
        let res = await promiseLike;
        window.alert("Contact has been deleted!");
        this.reouteToList();
      },
      onCancel(){
      },
      updateEmails(event){
        this.formData.emails = event;
      },
      updatePhoneNumbers(event){
        this.formData.phoneNumbers = event;
      },
      updateAddresses(event){
        this.formData.addresses = event;
      }
    },
    mounted(){
      if(this.proId > 0){
        this.getClientById(this.proId)
      }
    }
}
</script>

<style lang="scss">
.listElements {
  background-color: gainsboro;
  display: flex;
  flex-direction: row !important;
  margin-bottom: 30px;
}
#type {
  max-width: 150px;
  margin-left: 10px;
}
.md-field.inputListItem {
  margin-left: 10px;
}
.addContactForm{
  margin: 30px;
}
.addContact{
  margin-left: 50px;
  margin-top: 20px;
}
.addContactForm{
  max-width: 900px;
}
small {
  display: block;
}
  
</style>