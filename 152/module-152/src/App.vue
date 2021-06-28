<template>
  <div id="app">
    <Navbar></Navbar>
    <b-container class="mt-5">
      <router-view class="view" :key="$route.fullPath"></router-view>
    </b-container>

    <link href="https://fonts.googleapis.com/css2?family=Staatliches&display=swap" rel="stylesheet">
  </div>
</template>

<script>
import Navbar from './components/Navbar';

export default {
  name: 'app',
  methods: {},
  components: {
    Navbar
  }
}
</script>

<style>
.accent, .navbar-brand {
  color: #BF691F !important;
}

#app {
  margin: 0;
}

h1, .navbar-brand {
  font-family: 'Staatliches', 'sans-serif';
  color: #BF691F 
}
</style>
