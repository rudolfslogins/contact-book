<template>
  <div v-on:click="makeGetRequest">
      <md-button v-on:click="makeGetRequest" class="md-raised md-primary">Show all flights</md-button>
      <div 
      v-for="(contact, index) in contacts"
      v-bind:key="index">
      <!-- {{rate.from.city}} - {{rate.to.city}} : {{rate.departureTime}} -->
      {{contact.firstName}} {{contact.lastName}} {{contact.company}} {{contact.notes}} {{contact.birthDate}}  
      <div>
      <span 
      v-for="(adr, index) in contact.addresses"
      v-bind:key="index">
      {{adr.fullAddress}} {{adr.addressType.typeName}}
      </span>
      </div>
      <div>
      <span 
      v-for="(nr, index) in contact.phoneNumbers"
      v-bind:key="index">
      {{nr.prefix}} {{nr.number}} {{nr.phoneNumberType.typeName}}
      </span>
      </div>
      <div>
      <span 
      v-for="(email, index) in contact.emails"
      v-bind:key="index">
      {{email.emailName}} {{email.emailType.typeName}}
      </span>
      </div>
      <div ><br/></div>
      
      
      
  
        <!-- v-for="(flight, index) in flights" -->
        <!-- v-bind:key="index" -->
        <!-- v-bind:class="{error: flight.from.country === 'France'}"> -->
        <!-- {{index}} -->
        <!-- {{flight.from.city}} - {{flight.to.city}} -->
        <!-- <span v-if="flight.from.country === 'France'">:)</span> -->
        <!-- <span v-else-if="flight.from.country === 'Belgium'">@@@</span> -->
        <!-- <span v-else>$$$</span> -->
        <!-- {{rates}} -->
        <!-- <input type="text" v-model="flight.from.city"> -->
        </div>
  </div>
</template>

<script>
export default {
    name: 'FlightList',
    props: {
        flights: {
            type: [Array],
            default: []
        }
    },
    data() {
        return {
            i: 0,
            rates: {},
            username: "codelex-admin",
            password: "Password123",
            contacts: {}
        }

    },
    methods: {
        increment () {
            this.i++;
            console.log(this.i);
            this.$emit('incrementAA', this.i);
        },
        async makeGetRequest() {
            const promiseLike = this.$http.get('http://localhost:8082/api/contacts'
            // ,
            // {
            //     headers: {
            //         Authorization: 'Basic Y29kZWxleC1hZG1pbjpQYXNzd29yZDEyMw=='
            // }}
            );
            console.log(promiseLike);
            let res = await promiseLike;
            console.log(res);
            let json = await res.json()
            this.contacts = json;
        }
    }

}
</script>

<style scoped>

</style>