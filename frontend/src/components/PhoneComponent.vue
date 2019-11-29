<template>
  <div>
    <label id="labels">Phones</label>
      <md-button v-on:click="addPhoneField" class="md-icon-button">
        <md-icon>add</md-icon>
      </md-button>
    <div class="listElements" 
          v-for="(phoneNumber, index) in phoneNumbers" 
          v-bind:key="index">
        <md-field id="type">
            <label>Country code</label>
                <md-select id="phonePrefix" v-model="phoneNumber.prefix" md-dense>
                    <md-option v-for="code in countryCodes" :key="code.key" :value="code">{{ code }}</md-option>
                </md-select>
        </md-field>
        <md-field class="inputListItem">
            <label>Phone number</label>
                <md-input id="phoneNr" v-model="phoneNumber.number"></md-input>
        </md-field>

        <TypeComponent id="type" 
          v-bind:componentType="phoneNumber.phoneNumberType.typeName"
          v-on:typeUpdated="updateType($event, index)"></TypeComponent>

        <md-button v-on:click="removePhoneField(index)" class="md-icon-button">
            <md-icon>remove_circle_outline</md-icon>
        </md-button>
    </div>
  </div>
</template>

<script>
import ContactPage from "../views/ContactPage"
import TypeComponent from "../components/TypeComponent"
export default {
    name: "PhoneComponent",
    components: {
      TypeComponent
    },
    props: {
      phoneNumbers:{
        type: [Array],
        default: []
      }
    },
    data(){
        return {
            countryCodes: ["+370", "+371", "+372" ]
        }
    },
    methods:{
      addPhoneField(){
        let template = {
        prefix: "+371",
        number: "",
        phoneNumberType: {
            typeName: "Home"
                }
        }
        this.phoneNumbers.push(template);
        this.sendPhoneUpdatedEvent();
      },
        removePhoneField(index){
            this.phoneNumbers.splice(index, 1);
            this.sendPhoneUpdatedEvent();
        },
        sendPhoneUpdatedEvent(){
            this.$emit('phoneUpdated', this.phoneNumbers);
        },
        updateType(event, index){
            this.phoneNumbers[index].phoneNumberType.typeName = event;
        }
    }
}
</script>

<style>

</style>