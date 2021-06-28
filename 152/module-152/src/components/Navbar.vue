<template>
  <div>
  <b-navbar class="nav-container" type="light" variant="light" id="navbar-standard">
    <b-container>
    <b-navbar-nav id="bla">
      <b-navbar-brand to="/">Swissbit</b-navbar-brand>
      <b-nav-item to="/">Home</b-nav-item>
      <b-nav-item-dropdown text="Abgabe" right>
        <b-dropdown-item to="/profile/hans">Hans</b-dropdown-item>
        <b-dropdown-item to="/profile/schorsch">Schorsch</b-dropdown-item>
        <b-dropdown-item to="/profile/manfred">Manfred</b-dropdown-item>
        <b-dropdown-item to="/profile/gunter">Gunter</b-dropdown-item>
      </b-nav-item-dropdown>
      <b-nav-item-dropdown text="Portfolios">
        <b-dropdown-item to="/portfolios/hans">Hans</b-dropdown-item>
        <b-dropdown-item to="/portfolios/schorsch">Schorsch</b-dropdown-item>
        <b-dropdown-item to="/portfolios/manfred">Manfred</b-dropdown-item>
        <b-dropdown-item to="/portfolios/gunter">Gunter</b-dropdown-item>
      </b-nav-item-dropdown>
      <b-nav-item to="/video">Video & Livestream</b-nav-item>
    </b-navbar-nav>
    </b-container>
  </b-navbar>
  <b-navbar toggleable type="dark" variant="dark" id="navbar-mobile">
    <b-navbar-brand to="/">Swissbit</b-navbar-brand>

    <b-navbar-toggle target="navbar-toggle-collapse">
      <template v-slot:default="{ expanded }">
        <b-icon v-if="expanded" icon="chevron-bar-up"></b-icon>
        <b-icon v-else icon="chevron-bar-down"></b-icon>
      </template>
    </b-navbar-toggle>

    <b-collapse id="navbar-toggle-collapse" is-nav>
      <b-navbar-nav>
      <b-nav-item to="/">Home</b-nav-item>
      <b-nav-item-dropdown text="Abgabe" right>
        <b-dropdown-item to="/profile/hans">Hans</b-dropdown-item>
        <b-dropdown-item to="/profile/schorsch">Schorsch</b-dropdown-item>
        <b-dropdown-item to="/profile/manfred">Manfred</b-dropdown-item>
        <b-dropdown-item to="/profile/gunter">Gunter</b-dropdown-item>
      </b-nav-item-dropdown>
      <b-nav-item-dropdown text="Portfolios">
        <b-dropdown-item to="/portfolios/hans">Hans</b-dropdown-item>
        <b-dropdown-item to="/portfolios/schorsch">Schorsch</b-dropdown-item>
        <b-dropdown-item to="/portfolios/manfred">Manfred</b-dropdown-item>
        <b-dropdown-item to="/portfolios/gunter">Gunter</b-dropdown-item>
      </b-nav-item-dropdown>
      <b-nav-item to="/video">Video & Livestream</b-nav-item>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</div>
</template>

<script>

export default {
  name: 'navbar',
  methods: {}
}
</script>

<style>
.nav-container {
  background-image: url('../assets/metal_texture.jpg');
  color: black;
}

.navbar-light .navbar-nav .nav-link {
  color: rgba(0, 0, 0, 0.9);
}

@media only screen and (max-width: 600px) {
  #navbar-standard {
    display: none;
  }

  #navbar-mobile {
    display: block;
  }
}

@media only screen and (min-width: 600px) {
  #navbar-standard {
    display: block;
  }

  #navbar-mobile {
    display: none;
  }
}
</style>
