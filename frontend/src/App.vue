<template>
  <div id="app">
    <Header></Header>
    <div class="content">
      <md-tabs>
        <md-tab id="tab-home" md-label="Home" md-icon="home">
          <FlightPage></FlightPage>
        </md-tab>
        <md-tab id="tab-pages" md-label="Pages" md-icon="pages">
          BBB
        </md-tab>
        <md-tab id="tab-posts" md-label="Posts" md-icon="android">
          CCC
        </md-tab>
        <md-tab id="tab-favorites" md-label="Favorites" md-icon="favorite">
          DDD
        </md-tab>
      </md-tabs>
    </div>
    <div class="footer"></div>
  </div>
</template>

<script>
import FlightPage from './views/FlightsPage';
import Header from "./components/HeaderComponent";
export default {
  name: 'App',
  components: {
    FlightPage,
    Header
  }
}
</script>

<style lang="scss">

/* #app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  display: flex;
  flex-flow: column;
} */
.md-tabs {
  display: flex;
  flex-direction: row !important;
  .md-tabs-navigation {
    display: flex;
    flex-flow: column ;
  }
}

/* div[id^="tab-"] {
 // cool CSS stuffs
 background-color: #42b983;
} */
/* #nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
} */
</style>
