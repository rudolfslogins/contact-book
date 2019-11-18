<template>
  <form>
    <div>
<div class="searchForm">
        <md-field>
        <label>City From</label>
        <md-input v-model="formData.cityFrom"></md-input>
      </md-field>
      <md-field>
        <label>City To</label>
        <md-input v-model="formData.cityTo"></md-input>
      </md-field>
      <md-datepicker
        v-model="formData.fromDate" 
        md-immediately />
      <md-datepicker
        v-model="formData.toDate" 
        md-immediately />
    </div>
</div>
<div class="searchButton">
  <md-button v-on:click="searchFlights" class="md-raised md-primary">Search</md-button>
</div>
      <div 
      v-for="(res, index) in flightResults"
      v-bind:key="index">
      Airport from: {{res.from.airport}} | Airport to: {{res.to.airport}} | 
      Departure time: {{res.departureTime}} | Arrival time: {{res.arrivalTime}} | 
      Carrier: {{res.carrier}}
        </div>
  </form>
</template>

<script>
export default {
    name: 'SearchForm',
    data() {
        return {
            formData: {
                toDate: new Date(),
                fromDate: new Date(),
                cityFrom: 'STD',
                cityTo: 'RIX'
            },
        flightResults: {}
        }
    },
    methods:{
        async searchFlights() {
          let request = {
            from: this.formData.cityFrom,
            to: this.formData.cityTo,
            departureDate: this.formData.fromDate.toISOString().slice(0,10)
          }
          console.log(request);
            // console.log(this.formData);
            this.$emit('searchDataUpdated', this.formData);
            const promiseLike = this.$http.post('http://localhost:8081/api/flights/search', request)
          let res = await promiseLike
          let resJson = await res.json();
          console.log('RES: ', res);
          console.log('RESJSON', resJson.items)
          this.flightResults = resJson.items;

        }
    }
}
</script>

<style>
.searchForm {
  display: flex;
  flex-direction: row !important;
}

.searchButton {
  display: flex;
  flex-direction: row !important;
  justify-content: center;
}
</style>