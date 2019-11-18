<template>
    <div>
        <md-toolbar class="md-primary">
      <md-button class="md-icon-button" @click="showNavigation = true">
        <md-icon>menu</md-icon>
      </md-button>
      <span class="md-title">My Title</span>

      <div class="md-toolbar-section-end">
        <md-button @click="showSidepanel = true">Favorites</md-button>
      </div>
    </md-toolbar>
    </div>
</template>

<script>
// import MenuIcon from 'vue-material-design-icons/Menu.vue'

export default {
    name: 'Header'
}
</script>

<style scoped lang="scss">
</style>