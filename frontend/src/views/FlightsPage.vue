<template>
  <div class="flights">
    <FlightSearchForm v-on:searchDataUpdated="handleSearchForm($event)"></FlightSearchForm> 
    <div class="filtered">
      <div class="list">
        <FlightList
        v-bind:flights="flights"
        v-on:incrementAA="incrementAAHandler($event)"></FlightList>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
// @ is an alias to /src
import { Flight, RIX } from '../models/flight.model';
import FlightList from '../components/FlightList.vue';
import FlightSearchForm from '../components/FlightSearchForm.vue';

export default {
  name: 'FlightsPage',
  components:{
    FlightList,
    FlightSearchForm
  },
  data () {
    return {
      flights: [{
        from: {
          airportCode: 'MRS',
          city: 'Marsielle',
          country: 'France'
        },
        to: RIX,
        date: new Date()
      },
      {
        from: {
          airportCode: 'BEL',
          city: 'Brussels',
          country: 'Belgium'
        },
        to: RIX,
        date: new Date()
      },
      {
        from: {
          airportCode: 'RIX',
          city: 'Riga',
          country: 'Latvia'
        },
        to: RIX,
        date: new Date()
      }] as Flight[]
    }
  },
  methods: {
    incrementAAHandler(event: any){
      console.log('incrementAAHandler got ', event)
    },
    handleSearchForm(ev: any){
      console.log('FlightPage got ', ev)
    }
  }
}
</script>
<style scoped lang="scss">
.error{
  font-weight: 700;
  color: orange;
}
.flights{
  display: flex;
  flex-flow: column;
  form{
    // height: 100px;
    width: 100%;
    background-color:palegoldenrod;
  }
  .filtered{
    display: flex;
    justify-content: space-between;
    .map, .list{
      // height: 100px;
      background-color: bisque;
    }
  }
}
</style>
