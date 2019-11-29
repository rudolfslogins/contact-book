<template>
  <div>
    <md-table class="contact-table" v-on:contactAdded="makeGetRequest" v-model="searched" md-sort="name" md-sort-order="asc" md-card md-fixed-header @md-selected="onSelect">
      <md-table-toolbar>
        <div class="md-toolbar-section-start">
          
        </div>
        <md-button v-on:click="addContact" id="addContactBtn" class="md-raised md-primary">Add Contact</md-button>

        <md-field md-clearable class="md-toolbar-section-end">
          <md-input placeholder="Search by name, work, phone or email..." v-model="search" @input="searchOnTable" />
        </md-field>
      </md-table-toolbar>

      <div>
      <md-table-empty-state
        md-label="No users found"
        :md-description="`No user found for this '${search}' query. Try a different search term or create a new user.`">
        <md-button class="md-primary md-raised" @click="addContact">Create New Contact</md-button>
      </md-table-empty-state>
      </div>

      <md-table-row slot="md-table-row" slot-scope="{ item }" md-selectable="single">
        <md-table-cell md-label="Name" md-sort-by="firstName"><md-highlight-text :md-term="search">{{ item.firstName }}</md-highlight-text></md-table-cell>
        <md-table-cell md-label="Surname" md-sort-by="lastName"><md-highlight-text :md-term="search">{{ item.lastName }}</md-highlight-text></md-table-cell>
        <md-table-cell md-label="Work" md-sort-by="company"><md-highlight-text :md-term="search">{{ item.company }}</md-highlight-text></md-table-cell>
        <md-table-cell md-label="Date of birth" md-sort-by="bithDate">{{ item.birthDate }}</md-table-cell>
        <md-table-cell md-label="Notes" >{{ item.notes }}</md-table-cell>
        <md-table-cell md-label="Emails" ><div v-for="(email, index) in item.emails" v-bind:key="index"><md-highlight-text :md-term="search">{{email.emailName}}</md-highlight-text></div></md-table-cell>
        <md-table-cell md-label="Phone Numbers" ><div v-for="(phone, index) in item.phoneNumbers" v-bind:key="index"><md-highlight-text :md-term="search">{{phone.prefix}}-{{phone.number}}</md-highlight-text></div></md-table-cell>
        <md-table-cell md-label="Addresses" ><div v-for="(address, index) in item.addresses" v-bind:key="index">{{address.fullAddress}}</div></md-table-cell>
      </md-table-row>
    </md-table>
  </div>
</template>

<script>
  const toLower = text => {
    return text.toString().toLowerCase()
  }

  const searchByName = (items, term) => {
    if (term) {
      return items.filter(item => 
        toLower(item.firstName).includes(toLower(term)) || 
        toLower(item.lastName).includes(toLower(term)) ||
        toLower(item.company).includes(toLower(term)) || 
        item.phoneNumbers.some(y => y.number.includes(term)) ||
        item.emails.some(e => toLower(e.emailName).includes(toLower(term)))
        )
    }
    return items
  }
import ContactPage from "./ContactPage";
import router from "../router/index";
  
  export default {
    name: 'ContactList',
    data: () => ({
      search: null,
      searched: [],
      contacts: []
    }),
    methods: {
      searchOnTable () {
        this.searched = searchByName(this.contacts, this.search)
      },
      async makeGetRequest() {
        const promiseLike = this.$http.get('http://localhost:8082/api/contacts');
        let res = await promiseLike;
        let json = await res.json()
        this.contacts = json;
        this.searched = this.contacts
      },
      addContact(){
        this.$router.push({ name: "add"});
      },
      editContact(id){
        this.$router.push({ name: "edit", params:{Cid:id} });
      },
      onSelect (item) {
        this.editContact(item.id)
      }
    },
    created () {
      this.makeGetRequest();
      this.searched = this.contacts
    }
  }
</script>

<style lang="scss" scoped>
  .md-field {
    max-width: 300px;
  }
.my-table {
height: 5000px !important;
}
#addContactBtn{
  display: flex;
  justify-content: left;
}
body {
    min-height: 100vh;
}
.md-highlight-text > .md-highlight-text-match {
  font-weight: bolder !important;
}

</style>
