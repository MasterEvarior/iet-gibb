<template>
  <div id="app">
    <v-app>
      <Nav></Nav>
      <v-main>
        <router-view></router-view>
      </v-main>
      <Foot></Foot>
    </v-app>
  </div>
</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}

a a:visited {
  text-decoration: none;
}
</style>

<script>
import Nav from "./components/Nav.vue";
import Foot from "./components/Foot.vue";

export default {
  components: {
    Nav,
    Foot
  },
};
</script>
