<template>
  <div>
    <v-app-bar absolute dark app flat clipped-left>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>EyeCon</v-toolbar-title>

      <v-spacer></v-spacer>

      <v-btn v-if="isAuthenticated" v-on:click="handleLogout" right>
        Logout
      </v-btn>
    </v-app-bar>

    <v-navigation-drawer clipped app dark left v-model="drawer">
      <v-list>
        <nav-entry text='Login' icon='mdi-login' route="/login"/>
        <nav-entry text='Register' icon='mdi-account-plus' route="/register"/>
        <nav-entry text='Command' icon='mdi-lambda' route="/command"/>
        <nav-entry text='About us' icon='mdi-eye-outline' route='/about'/>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>


<script>
import NavEntry from './NavEntry.vue'
import { isAuthenticated, unsetJWT } from '../auth'

export default {
    components: {
        NavEntry
    },
    data: () => ({
      drawer: false,
      isAuthenticated: isAuthenticated()
    }),
    methods: {
      handleLogout() {
        unsetJWT();
        this.isAuthenticated = false;
        this.$router.push('/login');
      }
    }
};
</script>

<style lang="scss">
</style>