<template>
  <div >
    <label id="labels">Emails</label>
      <md-button v-on:click="addEmailField" class="md-icon-button">
        <md-icon>add</md-icon>
      </md-button>
    <div class="listElements" 
          v-on:clientDataReceived="updateComponent($event)"  
          v-for="(email, index) in emails" 
          v-bind:key="index">
      <md-field class="inputListItem">
        <label>Email</label>
          <md-input id="addresses" 
            v-model="email.emailName" 
            ></md-input>
      </md-field>

      <TypeComponent id="type" 
        v-bind:componentType="email.emailType.typeName"
        v-on:typeUpdated="updateType($event, index)"></TypeComponent>

      <md-button v-on:click="removeEmailField(index)" class="md-icon-button">
        <md-icon>remove_circle_outline</md-icon>
      </md-button>
    </div>
  </div>
</template>

<script>
import ContactPage from "../views/ContactPage"
import TypeComponent from "../components/TypeComponent"
export default {
  name: "EmailComponent",
  components: {
    TypeComponent
  },
  props: {
    emails: {
        type: Array,
        default: []
    }
  },
  methods: {
    addEmailField(){
      let template = {
        emailName: "",
        emailType: {
          typeName: "Home"
                }
      }
      this.emails.push(template);
      this.sendEmailUpdatedEvent();
      
    },
    removeEmailField(index){
      this.emails.splice(index, 1);
      this.sendEmailUpdatedEvent();
    },
    sendEmailUpdatedEvent(){
      this.$emit('emailUpdated', this.emails);
    },
    updateType(event, index){
      this.emails[index].emailType.typeName = event;
    }
  }
}
</script>

<style>

</style>