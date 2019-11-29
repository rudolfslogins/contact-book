<template>
  <div id="app">
    <Header></Header>
    <transition name="moveInUp">
         <router-view/>
  </transition>
    <div class="footer"></div>
  </div>
</template>

<script>
import Header from "./components/HeaderComponent";
import ContactPage from "./views/ContactPage";
import ContactList from "./views/ContactListPage";

import Vue from 'vue'
import VueMaterial from 'vue-material'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/black-green-light.css' // This line here

Vue.use(VueMaterial)

export default {
  name: 'App',
  components: {
    Header,
    ContactPage,
    ContactList
  }
}
</script>

<style lang="scss">

.md-tabs {
  display: flex;
  flex-direction: row !important;
  .md-tabs-navigation {
    display: flex;
    flex-flow: column ;
  }
}
.moveInUp-enter-active{
  animation: fadeIn 500ms ease-in;
}
@keyframes fadeIn{
  0%{
    opacity: 0;
  }
  50%{
  opacity: 0.5;
  }
  100%{
    opacity: 1;
  }
}

</style>
