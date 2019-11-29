<template>
  <div>
    <label id="labels">Addresses</label>
      <md-button v-on:click="addAddressField" class="md-icon-button">
        <md-icon>add</md-icon>
      </md-button>
    <div class="listElements" 
            v-for="(address, index) in addresses" 
            v-bind:key="index">

        <md-autocomplete class="inputListItem"
            v-model="address.fullAddress" 
            @md-selected="handleAddressSelection($event, index)"
            :md-options="googleAddresses.map(x=>({
            'formatted_address': x.formatted_address,
            'toLowerCase':()=>x.formatted_address.toLowerCase(),
            'toString':()=>x.formatted_address
            }))"  
            @md-changed="getGoogleAddresses" 
            @md-opened="getGoogleAddresses" 
            md-dense required>
                <label>Full address</label>
                    <template slot="md-autocomplete-item" slot-scope="{ item, term }">
                        <md-highlight-text :md-term="term">{{ item.formatted_address }}</md-highlight-text>
                    </template>
        </md-autocomplete>

        <TypeComponent id="type"
          v-bind:componentType="address.addressType.typeName"
          v-on:typeUpdated="updateType($event, index)"></TypeComponent>

        <md-button v-on:click="removeAddressField(index)" class="md-icon-button">
            <md-icon>remove_circle_outline</md-icon>
        </md-button>
    </div>
  </div>
</template>

<script>
import ContactPage from "../views/ContactPage"
import TypeComponent from "../components/TypeComponent"
export default {
    name: "AddressComponent",
    components: {
        TypeComponent
    },
    props: {
        addresses:{
            type: [Array],
            default: []
        }
    },
    data(){
        return {
            selectedAddress: null,
            googleAddresses: []
        }
    },
    methods:{
        addAddressField(){
            let template = {
            fullAddress: "",
            addressType: {
                typeName: "Home"
            }
            }
            this.addresses.push(template);
            this.sendAddressUpdatedEvent();
        },
        removeAddressField(index){
            this.addresses.splice(index, 1);
            this.sendAddressUpdatedEvent();
        },
        sendAddressUpdatedEvent(){
            this.$emit('addressUpdated', this.addresses);
        },
        updateType(event, index){
            this.addresses[index].addressType.typeName = event;
        },
        handleAddressSelection(event, index){
            this.addresses[index].fullAddress = event.formatted_address;
        },
        async getGoogleAddresses(searchTerm){
            if(searchTerm === undefined || searchTerm.length < 3)
            return;
            let term = searchTerm.replace(' ', '+');
            let searchRes = await this.$http.get(`https://maps.googleapis.com/maps/api/geocode/json?address=${term}&key=AIzaSyAN1xdzrFqNyhxrAstsTtrIHUdVqwx3pd0`);
            this.googleAddresses = await searchRes.data.results;
        }
    }
}
</script>

<style>

</style>